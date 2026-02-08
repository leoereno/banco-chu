using BancoChu.Data;
using BancoChu.Models;
using BancoChu.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Net.Security;

namespace Tests
{
    public class TransferenciaServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task VerificaDisponibilidade_DeveRetornarFalso_QuandoFeriado()
        {
            var db = GetDbContext();
            var mockDisponibilidadeSvc = new Mock<IDisponibilidadeService>();
            var mockUserSvc = new Mock<IUserService>();
            var mockCache = new Mock<IDistributedCache>();

            mockDisponibilidadeSvc.Setup(s => s.VerificaDisponibilidadeTransferencia(It.IsAny<string>())).ReturnsAsync(false);

            var service = new TransferenciaService(mockUserSvc.Object, db, mockCache.Object, mockDisponibilidadeSvc.Object);

            var result = await service.ProcessarTransferencia("11111111111", "22222222222", 10);

            Assert.False(result.Success);
            Assert.Equal("Operação não permitida fora de dia útil.", result.Message);
        }

        [Fact]
        public async Task Transferir_ShouldNotSucceed_WhenSaldoInsuficiente()
        {
            var db = GetDbContext();
            var mockCache = new Mock<IDistributedCache>();
            var mockUserSvc = new Mock<IUserService>();
            var disponibilidadeSvc = new Mock<IDisponibilidadeService>();

            string cpf1 = "11111111111";
            string cpf2 = "22222222222";

            var origem = new Conta { Id = new Guid(), Cpf = cpf1, Saldo = 10m};
            var destino = new Conta { Id = new Guid(), Cpf = cpf2, Saldo = 0m };

            db.Contas.AddRange(origem, destino);

            await db.SaveChangesAsync();

            mockUserSvc.Setup(s => s.GetContaByCpf(cpf1)).ReturnsAsync(origem);
            mockUserSvc.Setup(s => s.GetContaByCpf(cpf2)).ReturnsAsync(destino);

            disponibilidadeSvc.Setup(d => d.VerificaDisponibilidadeTransferencia(DateTime.Now.ToString("yyyy-MM-dd"))).ReturnsAsync(true);

            var transferSvc = new TransferenciaService(mockUserSvc.Object, db, mockCache.Object, disponibilidadeSvc.Object);

            var result = await transferSvc.ProcessarTransferencia(origem.Cpf, destino.Cpf, 50m);

            Assert.False(result.Success);
            Assert.Equal("Conta com saldo insuficiente para fazer a transferencia.", result.Message);
        }

        [Fact]
        public async Task Transferir_ShouldReturnOk_WhenTudoCerto()
        {
            var db = GetDbContext();
            var mockCache = new Mock<IDistributedCache>();
            var mockUserSvc = new Mock<IUserService>();
            var disponibilidadeSvc = new Mock<IDisponibilidadeService>();

            string cpf1 = "11111111111";
            var id1 = new Guid();

            string cpf2 = "22222222222";
            var id2 = new Guid();

            var origem = new Conta { Id = id1, Cpf = cpf1, Saldo = 50m };
            var destino = new Conta { Id = id2, Cpf = cpf2, Saldo = 0m };

            db.Contas.AddRange(origem, destino);

            await db.SaveChangesAsync();

            disponibilidadeSvc.Setup(d => d.VerificaDisponibilidadeTransferencia(It.IsAny<string>())).ReturnsAsync(true);

            mockUserSvc.Setup(s => s.GetContaByCpf(cpf1)).ReturnsAsync(origem);
            mockUserSvc.Setup(s => s.GetContaByCpf(cpf2)).ReturnsAsync(destino);

            var transferSvc = new TransferenciaService(mockUserSvc.Object, db, mockCache.Object, disponibilidadeSvc.Object);

            var result = await transferSvc.ProcessarTransferencia(origem.Cpf, destino.Cpf, 50m);

            Assert.True(result.Success);

            var origemNoDb = await db.Contas.FirstOrDefaultAsync(x => x.Cpf == cpf1);
            var destinoNoDb = await db.Contas.FirstOrDefaultAsync(x => x.Cpf == cpf2);


            Assert.Equal(0m, origemNoDb.Saldo);
            Assert.Equal(50m, destinoNoDb.Saldo);

        }
    }
}

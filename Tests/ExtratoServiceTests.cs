using BancoChu.Data;
using BancoChu.Models;
using BancoChu.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ExtratoServiceTests
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
        public async Task ExtratoDeveSerGerado()
        {
            var db = GetDbContext();
            var mockFeriadoSvc = new Mock<IDisponibilidadeService>();
            var mockUserSvc = new Mock<IUserService>();
            var mockCache = new Mock<IDistributedCache>();
            var mockTransferencia = new Mock<ITransferenciaService>();

            string cpf1 = "11111111111";
            string cpf2 = "22222222222";

            mockUserSvc.Setup(s => s.GetContaByCpf(cpf1))
               .ReturnsAsync(new Conta { Cpf = cpf1, Nome = "Leo" });

            db.Transferencias.AddRange(new List<Transferencia>
            {
                new () { CpfOrigem = cpf1, CpfDestino = cpf2, Valor = 100, Data = DateTime.Parse("2026-02-01") },
                new () { CpfOrigem = cpf1, CpfDestino = cpf2, Valor = 230, Data = DateTime.Parse("2026-02-07") },
                new () { CpfOrigem = cpf2, CpfDestino = cpf1, Valor = 421, Data = DateTime.Parse("2026-01-07") },
                new () { CpfOrigem = "55555555555", CpfDestino = cpf2, Valor = 230, Data = DateTime.Parse("2026-02-07") },
            });

            await db.SaveChangesAsync();

            var service = new ExtratoService(mockTransferencia.Object, mockUserSvc.Object, db);

            var result = await service.GerarExtrato(cpf1, "2026-01-01", "2026-02-09");

            Assert.Equal(3, result.Count);
            Assert.All(result, t => Assert.True(t.CpfOrigem == cpf1 || t.CpfDestino == cpf1));
            Assert.Equal(100, result[1].Valor);


        }
    }
}

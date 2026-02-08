using BancoChu.Data;
using BancoChu.Models.DTO;
using BancoChu.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Moq;

namespace Tests
{
    public class UserServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateConta_ShouldReturnCreatedConta_WhenValid()
        {
            var db = GetDbContext();
            var mockCache = new Mock<IDistributedCache>();
            var service = new UserService(db, mockCache.Object);

            var dto = new CreateAccountDto
            {
                Nome = "Lucas",
                Sobrenome = "Santos",
                Email = "lucas@mail.com",
                Cpf = "12345678908",
                Senha = "12345678"
            };

            var result = await service.CreateUser(dto);

            Assert.NotNull(result);
            Assert.Equal("Lucas", result.Nome);
            Assert.Equal(dto.Cpf, result.Cpf);

            var contaInDb = await db.Contas.FirstOrDefaultAsync(c => c.Email == dto.Email);
            Assert.NotNull(contaInDb);
        }
    }
}

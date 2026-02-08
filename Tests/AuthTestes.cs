using BancoChu.Data;
using BancoChu.Models;
using BancoChu.Services;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class AuthTestes
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
        public void BCrypt_Should_Verify_Correct_Password()
        {
            string senha = "12345678";

            string hash = BCrypt.Net.BCrypt.HashPassword(senha);
            bool isValid = BCrypt.Net.BCrypt.Verify(senha, hash);

            Assert.True(isValid);
        }

        [Fact]
        public async Task LoginShould_ReturnValidToken()
        {
            var context = GetDbContext();

            var mockConfig = new Mock<IConfiguration>();
            var mockUser = new Mock<IUserService>();

            mockConfig.Setup(c => c["Jwt:Key"]).Returns("d4S6yD5AsaTUFBKKq9fOlMSVi92TBxPd");
            mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("BancoChuApi");
            mockConfig.Setup(c => c["Jwt:Audience"]).Returns("BancoChuApp");

            var testCpf = "11111111111";
            var testPassword = "12345678";
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(testPassword);

            var contaExistente = new Conta
            {
                Cpf = testCpf,
                SenhaHash = passwordHash,
                Nome = "Leo",
                Sobrenome = "Ereno"
            };

            mockUser.Setup(s => s.GetContaByCpf(testCpf)).ReturnsAsync(contaExistente);

            var service = new AuthService(mockConfig.Object, mockUser.Object);

            var token = service.GenerateToken(contaExistente);

            Assert.NotNull(token);
            Assert.NotEmpty(token);

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(testPassword, contaExistente.SenhaHash);
            Assert.True(isPasswordValid);
        }
    }
}

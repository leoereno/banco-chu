using BancoChu.Models.DTO;
using BancoChu.Services;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace BancoChu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetContaByCpf(loginDto.Cpf);

            if (user == null || BCrypt.Net.BCrypt.Verify(loginDto.Senha, user.SenhaHash))
            {
                return Unauthorized("CPF ou senha inválidos.");
            }

            var token =_authService.GenerateToken(user);

            string nomeCompleto = user.Nome + " " + user.Sobrenome;

            return Ok(new
            {
                Token = token,
                User = new { nomeCompleto, user.Cpf }
            });
        }
    }
}

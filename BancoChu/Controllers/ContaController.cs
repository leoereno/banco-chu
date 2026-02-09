using Asp.Versioning;
using BancoChu.Models;
using BancoChu.Models.DTO;
using BancoChu.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BancoChu.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/conta")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly ITransferenciaService _transferenciaService;
        private readonly IUserService _userService;
        private readonly IExtratoService _extratoService;

        public ContaController(ITransferenciaService transferenciaService, IUserService userService, IExtratoService extratoService)
        {
            this._transferenciaService = transferenciaService;
            this._userService = userService;
            _extratoService = extratoService;
        }



        // GET: api/<ContaController>
        [HttpGet]
        public async Task<List<Conta>> Get()
        {
            return await this._userService.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conta>> GetConta(Guid id)
        {
            var conta = await _userService.GetContaById(id);
            if(conta == null)
            {
                return NotFound(new { message = "Conta não encontrada." });
            }

            return Ok(conta);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarConta([FromBody] CreateAccountDto createAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _userService.CreateUser(createAccountDto);
                return StatusCode(201, createdUser);
                //return CreatedAtAction("", new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno. {ex.Message}");
            }

        }

        [HttpPost("extrato")]
        public async Task<IActionResult> GerarExtrato([FromBody] ExtratoDto extratoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var extrato = await _extratoService.GerarExtrato(extratoDto.Cpf, extratoDto.DataInicial, extratoDto.DataFinal);

            return Ok(extrato);
        }





    }
}

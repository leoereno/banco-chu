using BancoChu.Models.DTO;
using BancoChu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoChu.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private readonly ITransferenciaService _transferenciaService;

        public TransferenciaController(ITransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }


        [HttpPost]
        public async Task<IActionResult> RealizarTransferencia([FromBody] TransferenciaDto transferenciaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cpfUsuarioLogado = User.FindFirst("Cpf")?.Value;

            if (string.IsNullOrEmpty(cpfUsuarioLogado))
                return Unauthorized();

            var result = await _transferenciaService.ProcessarTransferencia(transferenciaDto.CpfOrigem, transferenciaDto.CpfDestino, transferenciaDto.Valor);
            if (!result.Success)
                return BadRequest(new { message = result.Message });
            return Ok();
        }
    }
}

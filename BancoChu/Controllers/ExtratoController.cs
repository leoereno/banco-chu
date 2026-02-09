using Asp.Versioning;
using BancoChu.Models.DTO;
using BancoChu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoChu.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [ApiController]
    public class ExtratoController : ControllerBase
    {
        private readonly IExtratoService _extratoService;

        public ExtratoController(IExtratoService extratoService)
        {
            _extratoService = extratoService;
        }

        [HttpPost]
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

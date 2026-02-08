using System.ComponentModel.DataAnnotations;

namespace BancoChu.Models.DTO
{
    public class TransferenciaDto
    {
        [Required]
        public string CpfOrigem { get; set; }
        [Required]
        public string CpfDestino { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }
}

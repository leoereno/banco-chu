using System.ComponentModel.DataAnnotations;

namespace BancoChu.Models.DTO
{
    public class CreateAccountDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Sobrenome { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

    }
}

using System.ComponentModel.DataAnnotations;

namespace BancoChu.Models.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "CPF é obrigatório")]
        public string Cpf { get; set; } = String.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; } = String.Empty;
     }
}

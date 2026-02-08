using System.ComponentModel.DataAnnotations;

namespace BancoChu.Models.DTO
{
    public class CreateAccountDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(8, ErrorMessage = "Senha deve ter pelo menos 8 caracteres.")]
        public string Senha {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cpfs é obrigatório")]
        public string Cpf { get; set; } = string.Empty;

        public decimal Saldo { get; set; } = 0;

    }
}

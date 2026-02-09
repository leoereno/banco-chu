using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BancoChu.Models.DTO
{
    public class LoginDto
    {
        public string Cpf { get; set; } = String.Empty;

        public string Senha { get; set; } = String.Empty;
     }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Cpf).NotEmpty().Length(11).WithMessage("CPF Inválido");
            RuleFor(x => x.Senha).NotEmpty().MinimumLength(8).WithMessage("Senha não pode ser nula e deve conter 8 ou mais caracteres");
        }
    }
}

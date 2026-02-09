using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BancoChu.Models.DTO
{
    public class CreateAccountDto
    {
        public string Nome { get; set; } = string.Empty;

        public string Sobrenome { get; set; } = string.Empty;

        public string Senha {  get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;

        public decimal Saldo { get; set; } = 0;

    }

    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.Cpf).NotEmpty().Length(11).WithMessage("CPF Inválido");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome não pode ser nulo");
            RuleFor(x => x.Sobrenome).NotEmpty().WithMessage("Sobrenome não pode ser nulo");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email não pode ser nulo");
            RuleFor(x => x.Senha).NotEmpty().MinimumLength(8).WithMessage("Senha não pode ser nula e deve conter 8 ou mais caracteres");
        }
    }
}

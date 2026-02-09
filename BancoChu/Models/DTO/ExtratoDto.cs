using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BancoChu.Models.DTO
{
    public class ExtratoDto
    {
        [Required(ErrorMessage = "CPF da conta é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Data inicial em formato 'yyy-MM-dd' é obrigatória")]
        public string DataInicial { get; set; }

        [Required(ErrorMessage = "Data final em formato 'yyy-MM-dd' é obrigatória")]
        public string DataFinal { get; set; }
    }

    public class ExtratoValidator : AbstractValidator<ExtratoDto>
    {
        public ExtratoValidator()
        {
            RuleFor(x => x.DataInicial).NotEmpty().WithMessage("Data inicial deve ser no formato AAAA-MM-DD");
            RuleFor(x => x.Cpf).NotEmpty().Length(11).WithMessage("CPF inválido.");
            RuleFor(x => x.DataFinal).NotEmpty().WithMessage("Data final deve ser no formato AAAA-MM-DD");
        }
    }
}

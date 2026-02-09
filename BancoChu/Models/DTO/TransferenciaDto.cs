using FluentValidation;
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

    public class TransferenciaDtoValidator : AbstractValidator<TransferenciaDto>
    {
        public TransferenciaDtoValidator()
        {
            RuleFor(x => x.CpfOrigem).NotEmpty().Length(11).WithMessage("CPF de origem inválido.");
            RuleFor(x => x.CpfDestino).NotEmpty().Length(11).WithMessage("CPF de destino inválido.");
            RuleFor(x => x.Valor).NotEmpty().GreaterThan(0).WithMessage("Valor deve ser maior que zero.");
        }
    }
}

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
}

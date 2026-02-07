using System.ComponentModel.DataAnnotations;
using UUIDNext;

namespace BancoChu.Models
{
    public class Conta
    {
        [Key]
        public Guid Id { get; set; } = UUIDNext.Uuid.NewDatabaseFriendly(Database.Other);
        public string Nome { get; set; } = String.Empty;
        public string Sobrenome { get; set; } = String.Empty;
        public string Cpf { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public decimal Saldo { get; set; } = 0;

    }
}

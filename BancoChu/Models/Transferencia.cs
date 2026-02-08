using System.ComponentModel.DataAnnotations;
using UUIDNext;

namespace BancoChu.Models
{
    public class Transferencia
    {
        [Key]
        public Guid Id { get; set; } = UUIDNext.Uuid.NewDatabaseFriendly(Database.Other);
        public string CpfOrigem { get; set; }
        public string CpfDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

    }
}

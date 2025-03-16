using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApiPayphone.Domain.Models
{
    public enum TransactionType
    {
        Credit,
        Debit
    }

    public class Transaction
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }

        [Required]
        [EnumDataType(typeof(TransactionType), ErrorMessage = "El tipo debe ser 'Credit' o 'Debit'.")]
        public TransactionType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}

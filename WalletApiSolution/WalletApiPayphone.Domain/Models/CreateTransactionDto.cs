using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApiPayphone.Domain.Models
{
    
    public class CreateTransactionDto
    {       
        public decimal Amount { get; set; }

        [Required]
        [RegularExpression("^(Credit|Debit)$", ErrorMessage = "El tipo debe ser 'Credit' o 'Debit'.")]
        public string Type { get; set; }
    }
}

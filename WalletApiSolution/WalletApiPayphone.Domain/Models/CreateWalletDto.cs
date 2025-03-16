using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApiPayphone.Domain.Models
{
    public class CreateWalletDto
    {
        public string DocumentId { get; set; }
        public string? Name { get; set; } = null;
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

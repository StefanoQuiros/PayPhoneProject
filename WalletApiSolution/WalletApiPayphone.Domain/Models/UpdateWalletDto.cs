using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApiPayphone.Domain.Models
{
    public class UpdateWalletDto
    {
        public string? Name { get; set; }
        public decimal? Balance { get; set; }
        public string DocumentId { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool? Deleted { get; set; } = false;
    }
}

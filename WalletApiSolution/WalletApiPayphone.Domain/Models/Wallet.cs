﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApiPayphone.Domain.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string DocumentId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
        public int UserId { get; set; }
        public bool? Deleted { get; set; } = false;
    }
}

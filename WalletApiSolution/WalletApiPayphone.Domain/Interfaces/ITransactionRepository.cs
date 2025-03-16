using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApiPayphone.Domain.Models;

namespace WalletApiPayphone.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetByWalletIdAsync(int walletId);
        Task AddAsync(Transaction transaction);
    }
}

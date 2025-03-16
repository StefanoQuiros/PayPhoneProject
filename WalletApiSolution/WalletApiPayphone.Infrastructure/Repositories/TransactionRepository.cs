using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApiPayphone.Domain.Interfaces;
using WalletApiPayphone.Domain.Models;

namespace WalletApiPayphone.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private static List<Transaction> _transactions = new();

        public async Task<List<Transaction>> GetByWalletIdAsync(int walletId) =>
            _transactions.Where(t => t.WalletId == walletId).ToList();

        public async Task AddAsync(Transaction transaction) =>
            _transactions.Add(transaction);
    }
}

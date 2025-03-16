using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApiPayphone.Domain.Interfaces;
using WalletApiPayphone.Domain.Models;

namespace WalletApiPayphone.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private static List<Wallet> _wallets = new();

        public async Task<Wallet?> GetByIdAsync(int id) =>
            _wallets.FirstOrDefault(w => w.Id == id);

        public async Task<List<Wallet>> GetAllAsync() => _wallets;

        public async Task AddAsync(Wallet wallet) => _wallets.Add(wallet);

        public async Task UpdateAsync(Wallet wallet)
        {
            var index = _wallets.FindIndex(w => w.Id == wallet.Id);
            if (index >= 0) _wallets[index] = wallet;
        }

        public async Task DeleteAsync(int id) =>
            _wallets.RemoveAll(w => w.Id == id);
    }
}

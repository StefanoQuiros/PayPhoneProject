using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApiPayphone.Domain.Interfaces;
using WalletApiPayphone.Domain.Models;

namespace WalletApiPayphone.Application.Services
{
    public class WalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<List<Wallet>> GetWalletsAsync() =>
            await _walletRepository.GetAllAsync();

        public async Task<Wallet?> GetWalletByIdAsync(int id) =>
            await _walletRepository.GetByIdAsync(id);

        public async Task CreateWalletAsync(Wallet wallet) =>
            await _walletRepository.AddAsync(wallet);

        public async Task UpdateWalletAsync(Wallet wallet) =>
            await _walletRepository.UpdateAsync(wallet);

        public async Task DeleteWalletAsync(int id) =>
            await _walletRepository.DeleteAsync(id);
    }
}

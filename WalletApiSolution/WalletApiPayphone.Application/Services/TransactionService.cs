using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApiPayphone.Domain.Interfaces;
using WalletApiPayphone.Domain.Models;

namespace WalletApiPayphone.Application.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public async Task<List<Transaction>> GetTransactionsByWalletIdAsync(int walletId) =>
            await _transactionRepository.GetByWalletIdAsync(walletId);

        public async Task<bool> CreateTransactionAsync(int walletId, decimal amount, string type)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletId);

            if (wallet == null)
                throw new Exception("Wallet not found");

            if (amount <= 0)
                throw new Exception("Amount must be greater than zero");

            if (type == "Debit" && wallet.Balance < amount)
                throw new Exception("Insufficient funds");

            var transaction = new Transaction
            {
                WalletId = walletId,
                Amount = amount,
                Type = type
            };

            await _transactionRepository.AddAsync(transaction);

            wallet.Balance += type == "Credit" ? amount : -amount;
            wallet.UpdatedAt = DateTime.UtcNow;

            await _walletRepository.UpdateAsync(wallet);

            return true;
        }
    }
}

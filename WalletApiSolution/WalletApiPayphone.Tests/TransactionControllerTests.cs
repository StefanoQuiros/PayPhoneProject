using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApiPayphone.Controllers;
using WalletApiPayphone.Domain.Models;
using WalletApiPayphone.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WalletApiPayphone.Tests
{
    public class TransactionControllerTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly TransactionController _controller;

        public TransactionControllerTests()
        {
            _mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            _controller = new TransactionController(_mockContext.Object);
        }

      

        [Fact]
        public async Task GetTransactions_ReturnsNotFound_WhenWalletDoesNotExist()
        {
            // Arrange
            var walletId = 1;
            _mockContext.Setup(db => db.Wallets.FindAsync(walletId)).ReturnsAsync((Wallet)null);

            // Act
            var result = await _controller.GetTransactions(walletId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateTransaction_ReturnsNotFound_WhenWalletDoesNotExist()
        {
            // Arrange
            var walletId = 1;
            var createTransactionDto = new CreateTransactionDto { Amount = 50, Type = "Credit" };
            _mockContext.Setup(db => db.Wallets.FindAsync(walletId)).ReturnsAsync((Wallet)null);

            // Act
            var result = await _controller.CreateTransaction(walletId, createTransactionDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateTransaction_ReturnsBadRequest_WhenAmountIsInvalid()
        {
            // Arrange
            var walletId = 1;
            var wallet = new Wallet { Id = walletId, Balance = 100 };
            var createTransactionDto = new CreateTransactionDto { Amount = -10, Type = "Credit" };
            _mockContext.Setup(db => db.Wallets.FindAsync(walletId)).ReturnsAsync(wallet);

            // Act
            var result = await _controller.CreateTransaction(walletId, createTransactionDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateTransaction_ReturnsBadRequest_WhenInsufficientBalanceForDebit()
        {
            // Arrange
            var walletId = 1;
            var wallet = new Wallet { Id = walletId, Balance = 50 };
            var createTransactionDto = new CreateTransactionDto { Amount = 100, Type = "Debit" };
            _mockContext.Setup(db => db.Wallets.FindAsync(walletId)).ReturnsAsync(wallet);

            // Act
            var result = await _controller.CreateTransaction(walletId, createTransactionDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateTransaction_ReturnsCreated_WhenTransactionIsValid()
        {
            // Arrange
            var walletId = 1;
            var wallet = new Wallet { Id = walletId, Balance = 100 };
            var createTransactionDto = new CreateTransactionDto { Amount = 50, Type = "Credit" };
            _mockContext.Setup(db => db.Wallets.FindAsync(walletId)).ReturnsAsync(wallet);

            // Act
            var result = await _controller.CreateTransaction(walletId, createTransactionDto);

            // Assert
            var createdResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, createdResult.StatusCode);  // Espera que sea 201 Created
        }
    }
}
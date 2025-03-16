using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApiPayphone.Controllers;
using WalletApiPayphone.Domain.Models;
using WalletApiPayphone.Infrastructure.Data;


namespace WalletApiPayphone.Tests
{
    public class WalletControllerTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly WalletController _controller;

        public WalletControllerTests()
        {
            _mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            _controller = new WalletController(_mockContext.Object);
        }

        [Fact]
        public async Task GetWalletById_ReturnsOk_WhenWalletExists()
        {
            // Arrange
            var wallet = new Wallet { Id = 1, Name = "Test Wallet", Balance = 100 };
            _mockContext.Setup(db => db.Wallets.FindAsync(1)).ReturnsAsync(wallet);

            // Act
            var result = await _controller.GetWalletById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(wallet, okResult.Value);
        }

        [Fact]
        public async Task GetWalletById_ReturnsNotFound_WhenWalletDoesNotExist()
        {
            // Arrange
            _mockContext.Setup(db => db.Wallets.FindAsync(1)).ReturnsAsync((Wallet)null);

            // Act
            var result = await _controller.GetWalletById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateWallet_ReturnsBadRequest_WhenInvalidData()
        {
            // Act
            var result = await _controller.Create(new CreateWalletDto { DocumentId = "string", Name = "string", Balance = -10 });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateWallet_ReturnsCreated_WhenValidData()
        {
            // Arrange
            var walletDto = new CreateWalletDto { DocumentId = "123456", Name = "John Doe", Balance = 100 };
            _mockContext.Setup(db => db.Wallets.Add(It.IsAny<Wallet>()));

            // Act
            var result = await _controller.Create(walletDto);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, ((StatusCodeResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateWallet_ReturnsNotFound_WhenWalletDoesNotExist()
        {
            // Arrange
            _mockContext.Setup(db => db.Wallets.FindAsync(1)).ReturnsAsync((Wallet)null);

            // Act
            var result = await _controller.UpdateWallet(1, new UpdateWalletDto { Name = "Updated Name" });

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateWallet_ReturnsNoContent_WhenValidUpdate()
        {
            // Arrange
            var wallet = new Wallet { Id = 1, Name = "Old Name", Balance = 100 };
            _mockContext.Setup(db => db.Wallets.FindAsync(1)).ReturnsAsync(wallet);

            var updateDto = new UpdateWalletDto { Name = "New Name", Balance = 200 };

            // Act
            var result = await _controller.UpdateWallet(1, updateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal("New Name", wallet.Name);
            Assert.Equal(200, wallet.Balance);
        }
    }
}

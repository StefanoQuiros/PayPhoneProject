using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletApiPayphone.Controllers;
using WalletApiPayphone.Domain.Models;
using WalletApiPayphone.Infrastructure.Data;
namespace WalletApiPayphone.Tests
{
    public class WalletControllerIntegrationTests
    {
        private readonly AppDbContext _context;
        private readonly WalletController _controller;

        public WalletControllerIntegrationTests()
        {
            // Configuración de la base de datos en memoria
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "WalletTestDb") // Se puede usar un nombre único para la base de datos en memoria
                .Options;

            _context = new AppDbContext(options);

            // Crear la instancia del controlador con el contexto en memoria
            _controller = new WalletController(_context);
        }

        [Fact]
        public async Task CreateWallet_ReturnsCreated_WhenValidData()
        {
            // Arrange
            var walletDto = new CreateWalletDto { DocumentId = "123456", Name = "John Doe", Balance = 100 };

            // Act
            var result = await _controller.Create(walletDto);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, statusCodeResult.StatusCode);

            // Verificar que la billetera fue efectivamente guardada en la base de datos
            var wallet = await _context.Wallets.FindAsync(1);
            Assert.NotNull(wallet);
            Assert.Equal("John Doe", wallet.Name);
            Assert.Equal(100, wallet.Balance);
        }

        [Fact]
        public async Task GetWalletById_ReturnsOk_WhenWalletExists()
        {
            // Arrange
            var wallet = new Wallet { DocumentId = "123456", Id = 1, Name = "Test Wallet", Balance = 100 };
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetWalletById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedWallet = Assert.IsType<Wallet>(okResult.Value);
            Assert.Equal(wallet.Id, returnedWallet.Id);
            Assert.Equal(wallet.Name, returnedWallet.Name);
            Assert.Equal(wallet.Balance, returnedWallet.Balance);
        }

        [Fact]
        public async Task GetWalletById_ReturnsNotFound_WhenWalletDoesNotExist()
        {
            // Act
            var result = await _controller.GetWalletById(999); // ID que no existe

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

  

        [Fact]
        public async Task UpdateWallet_ReturnsNotFound_WhenWalletDoesNotExist()
        {
            // Arrange
            var updateDto = new UpdateWalletDto { Name = "New Name", Balance = 200 };

            // Act
            var result = await _controller.UpdateWallet(999, updateDto); // ID que no existe

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateWallet_ReturnsBadRequest_WhenInvalidData()
        {
            // Arrange
            var walletDto = new CreateWalletDto { DocumentId = "123456", Name = "John Doe", Balance = -10 }; // Balance inválido

            // Act
            var result = await _controller.Create(walletDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Balance", badRequestResult.Value.ToString());
        }
    }
}

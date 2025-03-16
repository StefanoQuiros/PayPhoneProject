using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WalletApiPayphone.Domain.Models;
using WalletApiPayphone.Infrastructure.Data;

namespace WalletApiPayphone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene el historial de movimientos por walletId.
        /// </summary>
        /// <param name="walletId">ID de la billetera</param>
        /// <returns>Lista de transacciones</returns>
        [HttpGet("{walletId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Transaction>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransactions(int walletId)
        {
            var wallet = await _context.Wallets.FindAsync(walletId);
            if (wallet == null)
                return NotFound("Billetera no encontrada");

            var transactions = await _context.Transactions.FindAsync(walletId);

            if (transactions == null)
                return NotFound("No hay transacciones con esta billetera");

            return Ok(transactions);
        }

        /// <summary>
        /// Crea una nueva transacción (Crédito o Débito).
        /// </summary>
        /// <param name="walletId">ID de la billetera</param>
        /// <param name="amount">Monto de la transacción</param>
        /// <param name="type">Tipo de transacción (Credit/Debit)</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateTransaction(int walletId, [FromBody] CreateTransactionDto createTransactionDto)
        {
            try
            {
                var wallet = await _context.Wallets.FindAsync(walletId);
                if (wallet == null)
                    return NotFound("Billetera no encontrada");

                var transaction = new Transaction();
                transaction.Type = Enum.Parse<TransactionType>(createTransactionDto.Type, true); // 'true' permite mayúsculas/minúsculas 

                if (createTransactionDto.Amount <= 0 || (transaction.Type == TransactionType.Debit && wallet.Balance < createTransactionDto.Amount))
                    return BadRequest("El monto de la transaccion no puede ser menor o igual a cero y el balance debe ser mayor a la transaccion que vas a realizar");
                
                transaction.Amount = createTransactionDto.Amount;                           
                transaction.WalletId = walletId;

                wallet.Balance += transaction.Type == TransactionType.Credit ? createTransactionDto.Amount : -createTransactionDto.Amount;

                _context.Transactions.Add(transaction);
                _context.Wallets.Update(wallet);

                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

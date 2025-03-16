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
    public class WalletController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WalletController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateWalletDto createWalletDto)
        {
            if (createWalletDto == null || createWalletDto.DocumentId == null || createWalletDto.Name == null || createWalletDto.DocumentId == "string" || createWalletDto.Name == "string" || createWalletDto.Balance < 0 )
                return BadRequest("Debes rellenar todos los campos y el Balance no puede ser menor a 0");

            var wallet = new Wallet();

            wallet.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            wallet.Name = createWalletDto.Name;
            wallet.Balance = createWalletDto.Balance;
            wallet.DocumentId = createWalletDto.DocumentId;

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, wallet);
        }


        // Obtener una billetera por ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalletById(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);

            if (wallet == null)
                return NotFound();

            return Ok(wallet);
        }

        // Actualizar una billetera
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWallet(int id, [FromBody] UpdateWalletDto updateWalletDto)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
                return NotFound("Billetera no encontrada");

            if (updateWalletDto.Name == "string" || updateWalletDto.DocumentId == "string" || updateWalletDto.Balance < 0)
            {
                return BadRequest("Uno o mas valores vienen incorrectos, verifica el nombre, el documentId y el balance no puede ser menor a 0");
            }

            if (updateWalletDto.Name == null && updateWalletDto.Balance == null && updateWalletDto.Deleted == null)
            {
                return BadRequest("No hay datos que actualizar");
            }            

            // Solo actualiza los campos que vienen en el DTO
            wallet.Name = updateWalletDto.Name ?? wallet.Name;
            wallet.Balance = updateWalletDto.Balance ?? wallet.Balance;
            wallet.Deleted = updateWalletDto.Deleted ?? wallet.Deleted;
            wallet.DocumentId = updateWalletDto.DocumentId ?? wallet.DocumentId;
            wallet.UpdatedAt = updateWalletDto.UpdatedAt;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
<<<<<<< HEAD
=======
using ProjetoPokeShop.Models;
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
using ProjetoPokeShop.Services;

namespace ProjetoPokeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StorageController : ControllerBase
    {
        readonly IStorageService _storageService;

        public StorageController(IStorageService storageService) => _storageService = storageService;

        [HttpGet("inventory/{id}")]
        public async Task<IActionResult> Inventory([FromRoute] int id)
        {
            try
            {
                var r = await _storageService.GetInventoryAsync(id);

                return Ok(r);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

<<<<<<< HEAD
=======
        [HttpGet("transactions/{id}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsAsync([FromRoute] int id)
        {
            try
            {
                var r = await _storageService.GetTransactionsAsync(id);

                return Ok(r);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        [HttpPost("sell-pokemon")]
        public async Task<ActionResult<SellResultDto>> SellPokemon([FromBody] SellRequestDto dto)
        {
            try
            {
<<<<<<< HEAD
                var r = await _storageService.SellPokemonAsync(dto.UserId, dto.UserPokemonId);
=======
                var r = await _storageService.SellPokemonAsync(dto.UserId, dto.PokemonId);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379

                return Ok(r);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
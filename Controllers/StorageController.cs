using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;
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
        public async Task<ActionResult<List<PokemonDto>>> Inventory([FromRoute] int id)
        {
            try
            {
                var r = await _storageService.Inventory(id);

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

        [HttpPost("sell-pokemon")]
        public async Task<ActionResult<SellResultDto>> SellPokemon([FromBody] SellRequestDto dto)
        {
            try
            {
                var r = await _storageService.SellPokemon(dto.UserId, dto.UserPokemonId);

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
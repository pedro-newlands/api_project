using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Services;

namespace ProjetoPokeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CenterController : ControllerBase
    {
        readonly ICenterService _centerService;

        public CenterController(ICenterService centerService) => _centerService = centerService;

        [HttpGet("center")]
        public async Task<IActionResult> GetAvailablePokemonsAsync()
        {
            var pokemonsForSale = await _centerService.GetAvailablePokemonsAsync();

            return Ok(pokemonsForSale);
        }

        [HttpPost("buy-pokemon")]
        public async Task<ActionResult<BuyResultDto>> BuyPokemon([FromBody] BuyRequestDto dto)
        {
            try
            {
                var r = await _centerService.BuyPokemonAsync(dto.PokemonCenterId, dto.UserId);

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
        
        [HttpPost("buy-pokeball")]
        public async Task<ActionResult<PokeballDto>> Pokeball([FromQuery]int userId)
        {
            try
            {
                var r = await _centerService.BuyPokeballAsync(userId);

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
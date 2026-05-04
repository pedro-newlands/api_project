using PokeShop.Application.DTOs.Storage;

namespace PokeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StorageController : ControllerBase
    {
        readonly IStorageService _storageService;

        public StorageController(IStorageService storageService) => _storageService = storageService;

        [HttpGet("inventory/{id}")]
        public async Task<ActionResult<IEnumerable<EngagedPokemonDto>>> Inventory([FromRoute] int id)
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

        [HttpGet("transactions/{id}")]
        public async Task<ActionResult<IEnumerable<TransactionSummaryDto>>> GetTransactionsAsync([FromRoute] int id)
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

        [HttpPost("sell-pokemon")]
        public async Task<ActionResult<SellResultDto>> SellPokemon([FromBody] SellRequestDto dto)
        {
            try
            {
                var r = await _storageService.SellPokemonAsync(dto.UserId, dto.PokemonId);

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
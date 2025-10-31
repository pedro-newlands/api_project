using api_project.Services;
using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;

namespace api_project.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        readonly IManagementService _managementService;

        public AdminController(IManagementService managementService) => _managementService = managementService;

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromHeader(Name = "X-Super-Password")] string superPassword)
        {
            var r = await _managementService.GetUsers(superPassword);

            return Ok(r);
        }

        [HttpGet("pokemons")]
        public async Task<IActionResult> GetPokemons([FromHeader(Name = "X-Super-Password")] string superPassword)
        {
            var r = await _managementService.GetPokemons(superPassword);

            return Ok(r);
        }

        [HttpGet("pokemon/{id}")]
        public async Task<ActionResult<ResultDto<Pokemon>>> GetPokemonById([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            var pokemon = await _managementService.GetPokemonById(superPassword, id);
            if (pokemon == null)
                return NotFound();

            return Ok(pokemon);
        }

        [HttpPost("pokemon")]
        public async Task<ActionResult<ResultDto<Pokemon>>> CreatePokemon([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] PokemonDto dto)
        {
            try
            {
                var pokemon = await _managementService.CreatePokemon(superPassword, dto);

                return CreatedAtAction
                    (nameof(GetPokemonById), new { id = pokemon.TargetEntity.Id }, pokemon);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
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

        [HttpPost("pokemoncenter/restore")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> RestorePokemonCenter([FromHeader(Name = "X-Super-Password")] string superPassword, [FromQuery] int targetId)
        {
            try
            {
                var r = await _managementService.RestorePokemonCenter(superPassword, targetId);

                return Ok(r);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
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

        [HttpPatch("user/{id}")]
        public async Task<ActionResult<ResultDto<User>>> UpdateUser([FromHeader(Name = "X-Super-Password")] string superPassword, int id, [FromBody] UserDto dto)
        {
            try
            {
                var r = await _managementService.UpdateUser(superPassword, id, dto);

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

        [HttpPatch("pokemon/{id}")]
        public async Task<ActionResult<ResultDto<Pokemon>>> UpdatePokemon([FromHeader(Name = "X-Super-Password")] string superPassword, int id, [FromBody] UpdatePokemonDto dto)
        {
            try
            {
                var updated = await _managementService.UpdatePokemon(superPassword, id, dto);
                return Ok(updated);
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

        [HttpDelete("user/{id}")]
        public async Task<ActionResult<ResultDto<User>>> DeleteUser([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deleted = await _managementService.DeleteUser(superPassword, id);

                return Ok(deleted);
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

        [HttpDelete("pokemon/{id}")]
        public async Task<ActionResult<ResultDto<Pokemon>>> DeletePokemon([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deleted = await _managementService.DeletePokemon(superPassword, id);

                return Ok(deleted);
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

        [HttpDelete("pokemoncenter/{id}")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> DeletePokemonCenter([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deleted = await _managementService.DeletePokemonCenter(superPassword, id);

                return Ok(deleted);
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
    }
}
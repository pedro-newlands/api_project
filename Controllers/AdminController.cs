using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Models;
using ProjetoPokeShop.Services;

namespace api_project.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        readonly IAdminService _adminService;

        public AdminController(IAdminService managementService) => _adminService = managementService;

        //User management
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersAsync([FromHeader(Name = "X-Super-Password")] string superPassword)
        {
            var users = await _adminService.GetAllUsersAsync(superPassword);

            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResultDto<User>>> GetUserByIdAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var pokemon = await _adminService.GetUserByIdAsync(superPassword, id);

                return Ok(pokemon);
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

        [HttpPost("user/create")]

        public async Task<ActionResult<ResultDto<User>>> CreateUserAsync([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] UserDto dto)
        {
            try
            {
                var newUser = await _adminService.CreateUserAsync(superPassword, dto);

                return Ok(newUser);
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

        [HttpPatch("user/update/{id}")]
        public async Task<ActionResult<ResultDto<User>>> UpdateUserAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id, [FromBody] UpdateUserDto dto)
        {
            try
            {
                var updatedUser = await _adminService.UpdateUserAsync(superPassword, id, dto);

                return Ok(updatedUser);
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

        [HttpDelete("user/delete/{id}")]
        public async Task<ActionResult<ResultDto<User>>> DeleteUserAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deletedUser = await _adminService.DeleteUserAsync(superPassword, id);

                return Ok(deletedUser);
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

        //Pokemon management
        [HttpGet("pokemons")]
        public async Task<IActionResult> GetAllPokemonsAsync([FromHeader(Name = "X-Super-Password")] string superPassword)
        {
            var pokemons = await _adminService.GetAllPokemonsAsync(superPassword);

            return Ok(pokemons);
        }

        [HttpGet("pokemon/{id}")]
        public async Task<ActionResult<ResultDto<Pokemon>>> GetPokemonByIdAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var pokemon = await _adminService.GetPokemonByIdAsync(superPassword, id);

                return Ok(pokemon);
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

        [HttpPost("pokemon/create")]
        public async Task<ActionResult<ResultDto<Pokemon>>> CreatePokemonAsync([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] PokemonDto dto)
        {
            try
            {
                var newPokemon = await _adminService.CreatePokemonAsync(superPassword, dto);

                return Ok (newPokemon);
                    
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

        [HttpPatch("pokemon/update/{id}")]
        public async Task<ActionResult<ResultDto<Pokemon>>> UpdatePokemonAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id, [FromBody] UpdatePokemonDto dto)
        {
            try
            {
                var updatedPokemon = await _adminService.UpdatePokemonAsync(superPassword, id, dto);
                return Ok(updatedPokemon);
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

        [HttpDelete("pokemon/delete/{id}")]
        public async Task<ActionResult<ResultDto<Pokemon>>> DeletePokemonAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deletedPokemon = await _adminService.DeletePokemonAsync(superPassword, id);

                return Ok(deletedPokemon);
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

        //Pokemon center management
        [HttpPost("pokemoncenter/restore")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> CreatePokemonCenterAsync([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] PokemonCenter pokemonCenter)
        {
            try
            {
                var oldPokemonCenter = await _adminService.CreatePokemonCenterAsync(superPassword, pokemonCenter);

                return Ok(oldPokemonCenter);
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

        [HttpDelete("pokemoncenter/delete/{id}")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> DeletePokemonCenterasync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deletedPokemonCenter = await _adminService.DeletePokemonCenterAsync(superPassword, id);

                return Ok(deletedPokemonCenter);
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
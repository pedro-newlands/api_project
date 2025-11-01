using api_project.Services;
using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Models;

namespace api_project.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        readonly IManagementService _managementService;

        public AdminController(IManagementService managementService) => _managementService = managementService;

        //User management
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromHeader(Name = "X-Super-Password")] string superPassword)
        {
            var users = await _managementService.GetUsers(superPassword);

            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResultDto<User>>> GetUserById([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var pokemon = await _managementService.GetUserById(superPassword, id);

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

        public async Task<ActionResult<ResultDto<User>>> CreateUser([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] UserDto dto)
        {
            try
            {
                var newUser = await _managementService.CreateUser(superPassword, dto);

                return CreatedAtAction(nameof(GetUserById), new { id = newUser.TargetEntity }, newUser);
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
        public async Task<ActionResult<ResultDto<User>>> UpdateUser([FromHeader(Name = "X-Super-Password")] string superPassword, int id, [FromBody] UpdateUserDto dto)
        {
            try
            {
                var updatedUser = await _managementService.UpdateUser(superPassword, id, dto);

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
        public async Task<ActionResult<ResultDto<User>>> DeleteUser([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deletedUser = await _managementService.DeleteUser(superPassword, id);

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
        public async Task<IActionResult> GetPokemons([FromHeader(Name = "X-Super-Password")] string superPassword)
        {
            var pokemons = await _managementService.GetPokemons(superPassword);

            return Ok(pokemons);
        }

        [HttpGet("pokemon/{id}")]
        public async Task<ActionResult<ResultDto<Pokemon>>> GetPokemonById([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var pokemon = await _managementService.GetPokemonById(superPassword, id);

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
        public async Task<ActionResult<ResultDto<Pokemon>>> CreatePokemon([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] PokemonDto dto)
        {
            try
            {
                var newPokemon = await _managementService.CreatePokemon(superPassword, dto);

                return CreatedAtAction
                    (nameof(GetPokemonById), new { id = newPokemon.TargetEntity.Id }, newPokemon);
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
        public async Task<ActionResult<ResultDto<Pokemon>>> UpdatePokemon([FromHeader(Name = "X-Super-Password")] string superPassword, int id, [FromBody] UpdatePokemonDto dto)
        {
            try
            {
                var updatedPokemon = await _managementService.UpdatePokemon(superPassword, id, dto);
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
        public async Task<ActionResult<ResultDto<Pokemon>>> DeletePokemon([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deletedPokemon = await _managementService.DeletePokemon(superPassword, id);

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
        public async Task<ActionResult<ResultDto<PokemonCenter>>> RestorePokemonCenter([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] PokemonCenterDto dto)
        {
            try
            {
                var oldPokemonCenter = await _managementService.RestorePokemonCenter(superPassword, dto);

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
        public async Task<ActionResult<ResultDto<PokemonCenter>>> DeletePokemonCenter([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var deletedPokemonCenter = await _managementService.DeletePokemonCenter(superPassword, id);

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
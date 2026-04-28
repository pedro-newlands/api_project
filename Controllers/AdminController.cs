<<<<<<< HEAD
=======
using api_project.DTOs.Management;
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Models;
using ProjetoPokeShop.Services;

<<<<<<< HEAD
namespace api_project.Controllers
=======
namespace ProjetoPokeShop.Controllers
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379

{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        readonly IAdminService _adminService;

        public AdminController(IAdminService managementService) => _adminService = managementService;

        //User management
        [HttpGet("users")]
<<<<<<< HEAD
        public async Task<IActionResult> GetAllUsersAsync([FromHeader(Name = "X-Super-Password")] string superPassword)
=======
        public async Task<ActionResult<ResultDto<IEnumerable<User>>>> GetAllUsersAsync([FromHeader(Name = "X-Super-Password")] string superPassword)
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        {
            var users = await _adminService.GetAllUsersAsync(superPassword);

            return Ok(users);
        }

<<<<<<< HEAD
        [HttpGet("user/{id}")]
=======
        [HttpGet("user/{id}", Name = "GetUserById")]
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        public async Task<ActionResult<ResultDto<User>>> GetUserByIdAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
<<<<<<< HEAD
                var pokemon = await _adminService.GetUserByIdAsync(superPassword, id);

                return Ok(pokemon);
=======
                var user = await _adminService.GetUserByIdAsync(superPassword, id);

                return Ok(user);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
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

<<<<<<< HEAD
                return Ok(newUser);
=======
                return CreatedAtRoute("GetUserById", new {id = newUser.TargetEntity.Id}, newUser);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
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
<<<<<<< HEAD
        public async Task<IActionResult> GetAllPokemonsAsync([FromHeader(Name = "X-Super-Password")] string superPassword)
=======
        public async Task<ActionResult<ResultDto<IEnumerable<Pokemon>>>> GetAllPokemonsAsync([FromHeader(Name = "X-Super-Password")] string superPassword)
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        {
            var pokemons = await _adminService.GetAllPokemonsAsync(superPassword);

            return Ok(pokemons);
        }

<<<<<<< HEAD
        [HttpGet("pokemon/{id}")]
=======
        [HttpGet("pokemon/{id}", Name = "GetPokemonById")]
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
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

<<<<<<< HEAD
                return Ok (newPokemon);
=======
                return CreatedAtRoute ("GetPokemonById", new { id = newPokemon.TargetEntity.Id}, newPokemon);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
                    
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
<<<<<<< HEAD
        [HttpPost("pokemoncenter/restore")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> CreatePokemonCenterAsync([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] PokemonCenter pokemonCenter)
        {
            try
            {
                var oldPokemonCenter = await _adminService.CreatePokemonCenterAsync(superPassword, pokemonCenter);
=======
        [HttpPost("pokemoncenter/create")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> CreatePokemonCenterAsync([FromHeader(Name = "X-Super-Password")] string superPassword, [FromBody] PokemonCenterDto dto)
        {
            try
            {
                var oldPokemonCenter = await _adminService.CreatePokemonCenterAsync(superPassword, dto);
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379

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

<<<<<<< HEAD
        [HttpDelete("pokemoncenter/delete/{id}")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> DeletePokemonCenterasync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
=======
        [HttpPatch("pokemoncenter/update/{id}")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> UpdatePokemonCenterMarketPriceAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id, [FromBody] UpdatePriceDto dto)
        {
            try
            {
                var updatedPokemonCenter = await _adminService.UpdatePokemonCenterMarketPriceAsync(superPassword, id, dto);
                return Ok(updatedPokemonCenter);
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
        public async Task<ActionResult<ResultDto<PokemonCenter>>> DeletePokemonCenterAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
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
<<<<<<< HEAD
=======

        //Transaction management
        [HttpGet("transactions")]
        public async Task<ActionResult<ResultDto<IEnumerable<Transaction>>>> GetAllTransactionsAsync([FromHeader(Name = "X-Super-Password")] string superPassword)
        {
            var transactions = await _adminService.GetAllTransactionsAsync(superPassword);

            return Ok(transactions);
        }

        [HttpGet("transaction/{id}", Name = "GetTransactionById")]
        public async Task<ActionResult<ResultDto<Transaction>>> GetTransactionByIdAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var transaction = await _adminService.GetTransactionByIdAsync(superPassword, id);

                return Ok(transaction);
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

        [HttpGet("transactions/user/{id}", Name = "GetTransactionsByUserId")]
        public async Task<ActionResult<ResultDto<IEnumerable<Transaction>>>> GetTransactionsByUserIdAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var transaction = await _adminService.GetTransactionsByUserIdAsync(superPassword, id);

                return Ok(transaction);
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

        [HttpGet("transactions/pokemon/{id}", Name = "GetTransactionsByPokemonId")]
        public async Task<ActionResult<ResultDto<IEnumerable<Transaction>>>> GetTransactionsByPokemonIdAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var transaction = await _adminService.GetTransactionsByPokemonIdAsync(superPassword, id);

                return Ok(transaction);
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

        [HttpGet("transactions/history")]
        public async Task<ActionResult<ResultDto<IEnumerable<Transaction>>>> GetTransactionsHistory(
            [FromHeader(Name = "X-Super-Password")] string superPassword,
            [FromQuery] int? year,
            [FromQuery] int? month,
            [FromQuery] int? day)
        {
            try
            {
                var result = await _adminService.GetTransactionsHistoryAsync(superPassword, year, month, day);

                return Ok(result);
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

        [HttpDelete("transaction/delete/{id}")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> DeleteTransactionAsync([FromHeader(Name = "X-Super-Password")] string superPassword, int id)
        {
            try
            {
                var transactionDeleted = await _adminService.DeleteTransactionAsync(superPassword, id);

                return Ok(transactionDeleted);
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
    }
}
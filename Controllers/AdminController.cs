using api_project.DTOs.Management;
using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.DTOs.Entity;
using ProjetoPokeShop.Models;
using ProjetoPokeShop.Services;
using ProjetoPokeShop.Filters;

namespace ProjetoPokeShop.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    [AdminOnly]
    public class AdminController : ControllerBase
    {
        readonly IAdminService _adminService;

        public AdminController(IAdminService managementService) => _adminService = managementService;

        //User management
        [HttpGet("users")]
        public async Task<ActionResult<ResultDto<IEnumerable<User>>>> GetAllUsersAsync()
        {
            var users = await _adminService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("user/{id}", Name = "GetUserById")]
        public async Task<ActionResult<ResultDto<User>>> GetUserByIdAsync(, int id)
        {
            try
            {
                var user = await _adminService.GetUserByIdAsync(id);

                return Ok(user);
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

        public async Task<ActionResult<ResultDto<User>>> CreateUserAsync(, [FromBody] UserDto dto)
        {
            try
            {
                var newUser = await _adminService.CreateUserAsync(dto);

                return CreatedAtRoute("GetUserById", new {id = newUser.TargetEntity.Id}, newUser);
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
        public async Task<ActionResult<ResultDto<User>>> UpdateUserAsync(, int id, [FromBody] UpdateUserDto dto)
        {
            try
            {
                var updatedUser = await _adminService.UpdateUserAsync(id, dto);

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
        public async Task<ActionResult<ResultDto<User>>> DeleteUserAsync(, int id)
        {
            try
            {
                var deletedUser = await _adminService.DeleteUserAsync(id);

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
        public async Task<ActionResult<ResultDto<IEnumerable<Pokemon>>>> GetAllPokemonsAsync()
        {
            var pokemons = await _adminService.GetAllPokemonsAsync();

            return Ok(pokemons);
        }

        [HttpGet("pokemon/{id}", Name = "GetPokemonById")]
        public async Task<ActionResult<ResultDto<Pokemon>>> GetPokemonByIdAsync(int id)
        {
            try
            {
                var pokemon = await _adminService.GetPokemonByIdAsync(id);

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
        public async Task<ActionResult<ResultDto<Pokemon>>> CreatePokemonAsync(PokemonDto dto)
        {
            try
            {
                var newPokemon = await _adminService.CreatePokemonAsync(dto);

                return CreatedAtRoute ("GetPokemonById", new { id = newPokemon.TargetEntity.Id}, newPokemon);
                    
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
        public async Task<ActionResult<ResultDto<Pokemon>>> UpdatePokemonAsync(int id, [FromBody] UpdatePokemonDto dto)
        {
            try
            {
                var updatedPokemon = await _adminService.UpdatePokemonAsync(id, dto);
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
        public async Task<ActionResult<ResultDto<Pokemon>>> DeletePokemonAsync(int id)
        {
            try
            {
                var deletedPokemon = await _adminService.DeletePokemonAsync(id);

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
        [HttpPost("pokemoncenter/create")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> CreatePokemonCenterAsync([FromBody] PokemonCenterDto dto)
        {
            try
            {
                var oldPokemonCenter = await _adminService.CreatePokemonCenterAsync(dto);

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

        [HttpPatch("pokemoncenter/update/{id}")]
        public async Task<ActionResult<ResultDto<PokemonCenter>>> UpdatePokemonCenterMarketPriceAsync(int id, [FromBody] UpdatePriceDto dto)
        {
            try
            {
                var updatedPokemonCenter = await _adminService.UpdatePokemonCenterMarketPriceAsync(id, dto);
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
        public async Task<ActionResult<ResultDto<PokemonCenter>>> DeletePokemonCenterAsync(int id)
        {
            try
            {
                var deletedPokemonCenter = await _adminService.DeletePokemonCenterAsync(id);

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

        //Transaction management
        [HttpGet("transactions")]
        public async Task<ActionResult<ResultDto<IEnumerable<Transaction>>>> GetAllTransactionsAsync()
        {
            var transactions = await _adminService.GetAllTransactionsAsync();

            return Ok(transactions);
        }

        [HttpGet("transaction/{id}", Name = "GetTransactionById")]
        public async Task<ActionResult<ResultDto<Transaction>>> GetTransactionByIdAsync(int id)
        {
            try
            {
                var transaction = await _adminService.GetTransactionByIdAsync(id);

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
        public async Task<ActionResult<ResultDto<IEnumerable<Transaction>>>> GetTransactionsByUserIdAsync(int id)
        {
            try
            {
                var transaction = await _adminService.GetTransactionsByUserIdAsync(id);

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
        public async Task<ActionResult<ResultDto<IEnumerable<Transaction>>>> GetTransactionsByPokemonIdAsync(int id)
        {
            try
            {
                var transaction = await _adminService.GetTransactionsByPokemonIdAsync(id);

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
            ,
            [FromQuery] int? year,
            [FromQuery] int? month,
            [FromQuery] int? day)
        {
            try
            {
                var result = await _adminService.GetTransactionsHistoryAsync(year, month, day);

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
        public async Task<ActionResult<ResultDto<PokemonCenter>>> DeleteTransactionAsync(int id)
        {
            try
            {
                var transactionDeleted = await _adminService.DeleteTransactionAsync(id);

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
    }
}
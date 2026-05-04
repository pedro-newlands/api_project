using PokeShop.Application.DTOs.Management;

namespace PokeShop.Controllers

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
        public async Task<ActionResult<ResultDto<IEnumerable<UserManagementResponseDto>>>> GetAllUsersAsync()
        {
            var users = await _adminService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("user/{id}", Name = "GetUserById")]
        public async Task<ActionResult<ResultDto<UserManagementResponseDto>>> GetUserByIdAsync(int id)
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

        public async Task<ActionResult<ResultDto<UserManagementResponseDto>>> CreateUserAsync([FromBody] UserManagementCreateDto dto)
        {
            try
            {
                var newUser = await _adminService.CreateUserAsync(dto);

                return CreatedAtRoute("GetUserById", new {id = newUser.ResponseDto.Id}, newUser);
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
        public async Task<ActionResult<ResultDto<UserManagementResponseDto>>> UpdateUserAsync(int id, [FromBody] UserManagementUpdateDto dto)
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
        public async Task<ActionResult<ResultDto<UserManagementResponseDto>>> DeleteUserAsync(int id)
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
        public async Task<ActionResult<ResultDto<IEnumerable<PokemonManagementResponseDto>>>> GetAllPokemonsAsync()
        {
            var pokemons = await _adminService.GetAllPokemonsAsync();

            return Ok(pokemons);
        }

        [HttpGet("pokemon/{id}", Name = "GetPokemonById")]
        public async Task<ActionResult<ResultDto<PokemonManagementResponseDto>>> GetPokemonByIdAsync(int id)
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
        public async Task<ActionResult<ResultDto<PokemonManagementResponseDto>>> CreatePokemonAsync(PokemonManagementCreateDto dto)
        {
            try
            {
                var newPokemon = await _adminService.CreatePokemonAsync(dto);

                return CreatedAtRoute ("GetPokemonById", new { id = newPokemon.ResponseDto.Id}, newPokemon);
                    
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
        public async Task<ActionResult<ResultDto<PokemonManagementResponseDto>>> UpdatePokemonAsync(int id, [FromBody] PokemonManagementUpdateDto dto)
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
        public async Task<ActionResult<ResultDto<PokemonManagementResponseDto>>> DeletePokemonAsync(int id)
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
        public async Task<ActionResult<ResultDto<PokemonCenterManagementResponseDto>>> CreatePokemonCenterAsync([FromBody] PokemonCenterManagementCreateDto dto)
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
        public async Task<ActionResult<ResultDto<PokemonCenterManagementResponseDto>>> UpdatePokemonCenterMarketPriceAsync(int id, [FromBody] PokemonCenterManagementUpdateDto dto)
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
        public async Task<ActionResult<ResultDto<PokemonCenterManagementResponseDto>>> DeletePokemonCenterAsync(int id)
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
        [HttpGet("transactions/history")]
        public async Task<ActionResult<ResultDto<IEnumerable<TransactionManagementResponseDto>>>> GetTransactionsHistory(
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
        
        [HttpGet("transactions")]
        public async Task<ActionResult<ResultDto<IEnumerable<TransactionManagementResponseDto>>>> GetAllTransactionsAsync()
        {
            var transactions = await _adminService.GetAllTransactionsAsync();

            return Ok(transactions);
        }
        

        [HttpGet("transaction/{id}", Name = "GetTransactionById")]
        public async Task<ActionResult<ResultDto<TransactionManagementResponseDto>>> GetTransactionByIdAsync(int id)
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
        public async Task<ActionResult<ResultDto<IEnumerable<TransactionManagementResponseDto>>>> GetTransactionsByUserIdAsync(int id)
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
        public async Task<ActionResult<ResultDto<IEnumerable<TransactionManagementResponseDto>>>> GetTransactionsByPokemonIdAsync(int id)
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
    }
}
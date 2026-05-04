using PokeShop.Application.DTOs.Login;

namespace PokeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        readonly ILoginService _loginService;

        public LoginController(ILoginService loginService) => _loginService = loginService;

        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginDto dto)
        {
            try
            {
                var r = await _loginService.LoginAsync(dto.UserName, dto.Password);

                return Ok(r);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }  
    }
}
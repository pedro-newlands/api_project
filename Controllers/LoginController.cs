using Microsoft.AspNetCore.Mvc;
using ProjetoPokeShop.DTOs;
using ProjetoPokeShop.Models;
using ProjetoPokeShop.Services;


namespace ProjetoPokeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        readonly ILoginService _loginService;

        public LoginController(ILoginService loginService) => _loginService = loginService;

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDto dto)
        {
            try
            {
                var r = await _loginService.Login(dto.UserName, dto.Password);

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
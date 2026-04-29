using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoPokeShop.Filters 
{
  public class AdminOnlyFilter : IActionFilter 
  {
    private readonly IConfiguration _config;

    public AdminOnlyFilter(IConfiguration config) => _config = config;

    public void OnActionExecuting(ActionExecutingContext context)
    {
      var expectedPassword = _config["AdminSettings:SuperPassword"];

      if (!context.HttpContext.Request.Headers.TryGetValue("X-Super-Password", out var providedPassword)) 
      {
        context.Result = new UnauthorizedObjectResult(new { message = "Header 'X-Super-Password' missing"});
        return;
      }

      if (providedPassword != expectedPassword)
      {
        context.Result = new ContentResult 
        {
          StatusCode = 403,
          Content = "Access denied: Invalid admin password "
        };
        
        return;

      }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
  }
}
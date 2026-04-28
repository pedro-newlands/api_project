using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoPokeShop.Filters 
{
  public class AdminOnlyAttribute : ActionFilterAttribute 
  {
    public override void OnActionExecuting(ActionExecutionContext context)
    {
      var config = context.HttpRequest.RequestServices.GetService<IConfiguration>();
      var expectedPassword = config["AdminSettings:SuperPassword"];

      if (!context.HttpRequest.Request.Headers.TryGetValue("X-Super-Password", out var providedPassword)) 
      {
        context.Result = new UnauthorizedObjectResult(new { message = "Header 'X-Super-Password' missing"});
        return;
      }

      if (providedPassword != expectedPassword)
      {
        context.Result = new ContentResult 
        {
          StatusCode: 403,
          Content = "Access denied: Invalid admin password "
        }
        return;
      }

      base.OnActionExecuting(context);
    }
  }
}
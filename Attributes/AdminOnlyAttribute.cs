using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoPokeShop.Filters;

namespace ProjetoPokeShop.Attributes
{
  public class AdminOnlyAttribute : ServiceFilterAttribute 
  {
    public AdminOnlyAttribute() : base(typeof(AdminOnlyFilter)) { }
  }
}
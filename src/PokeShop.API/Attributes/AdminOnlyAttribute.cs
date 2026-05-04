using PokeShop.API.Filters;

namespace PokeShop.API.Attributes
{
  public class AdminOnlyAttribute : ServiceFilterAttribute 
  {
    public AdminOnlyAttribute() : base(typeof(AdminOnlyFilter)) { }
  }
}
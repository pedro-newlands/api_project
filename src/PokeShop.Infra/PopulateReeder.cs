using Microsoft.Extensions.DependencyInjection;
using PokeShop.Infra.Data;

namespace PokeShop.Infra
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // O arquivo .sql deve estar configurado para ser copiado para a pasta de saída
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seed", "PopulatePokeShop.sql");

            if (File.Exists(path)) 
            {
              string sql = await File.ReadAllTextAsync(path);
              await context.Database.ExecuteSqlRawAsync(sql);
            }
        }
    }
}
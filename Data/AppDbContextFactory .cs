// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;

// namespace ProjetoPokeShop.Data
// {
//     public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//     {
//         public AppDbContext CreateDbContext(string[] args)
//         {
//             IConfigurationRoot configuration = new ConfigurationBuilder()
//                 .SetBasePath(Directory.GetCurrentDirectory())
//                 .AddJsonFile("appsettings.json")
//                 .Build();

//             var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

//             var connectionString = configuration.GetConnectionString("AppDbConnectionString");

//             optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

//             return new AppDbContext(optionsBuilder.Options);
//         }
//     }
// }
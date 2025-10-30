using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ProjetoPokeShop.Models;

namespace ProjetoPokeShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<UserPokemon> UserPokemons { get; set; } // relação usuário + pokémon -> inventário
        public DbSet<PokemonCenter> PokemonCenter { get; set; } // loja


        //configuração de modelo e relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserPokemon>()
                .HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<UserPokemon>()
                .HasOne(p => p.Pokemon)
                .WithMany()
                .HasForeignKey(p => p.PokemonId);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pokemon>()
                .Property(p => p.Rarity)
                .HasConversion<string>();
                
            //indices
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Pokemon>()
                .HasIndex(p => p.Type);

            modelBuilder.Entity<Pokemon>()
                .HasIndex(p => p.Rarity);
        }

    }
}
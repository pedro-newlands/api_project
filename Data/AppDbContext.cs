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
        public DbSet<Element> Elements { get; set; }
        public DbSet<Rarity> Rarities { get; set; }


        //configuração de modelo e relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserPokemon>()
                .HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserPokemon>()
                .HasOne(p => p.Pokemon)
                .WithMany()
                .HasForeignKey(p => p.PokemonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Pokemons)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Rarity)
                .WithMany(r => r.Pokemons)
                .HasForeignKey(p => p.RarityId);

            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.Elements)
                .WithMany(t => t.Pokemons)
                .UsingEntity(j => j.ToTable("PokemonElement"));
            
            modelBuilder.Entity<PokemonCenter>()
                .HasOne(pc => pc.Pokemon)
                .WithMany()
                .HasForeignKey(pc => pc.PokemonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Element>()
                .Property(e => e.Name)
                .HasConversion<string>();

            modelBuilder.Entity<Rarity>()
                .Property(r => r.Name)
                .HasConversion<string>();

            //indices
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<UserPokemon>()
                .HasIndex(up => up.AcquiredAt);

            modelBuilder.Entity<UserPokemon>()
                .HasIndex(up => up.PokemonId)
                .IsUnique();
        }

    }
}
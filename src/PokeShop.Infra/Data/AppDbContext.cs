namespace PokeShop.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Transaction> Transactions { get; set; } // relação de histórico
        public DbSet<PokemonCenter> PokemonCenter { get; set; } // loja
        public DbSet<Element> Elements { get; set; }
        public DbSet<Rarity> Rarities { get; set; }


        //configuração de modelo e relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<User>()
            //     .ToTable(t => t.HasCheckConstraint(
            //         "CK_User_Admin_Always_Active",
            //         "Id <> 1 OR IsActive = 1"      
            //     ));

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => u.IsActive);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Pokemon)
                .WithMany()
                .HasForeignKey(p => p.PokemonId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Rarity)
                .WithMany()
                .HasForeignKey(p => p.RarityId);

            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.Elements)
                .WithMany()
                .UsingEntity(j => j.ToTable("PokemonElement"));

            modelBuilder.Entity<PokemonCenter>()
                .HasKey(pc => pc.PokemonId);
            
            modelBuilder.Entity<PokemonCenter>()
                .HasOne(pc => pc.Pokemon)
                .WithOne()
                .HasForeignKey<PokemonCenter>(pc => pc.PokemonId)
                .OnDelete(DeleteBehavior.Cascade);

            //properties
            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Status)
                .HasConversion<string>();
            
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

            modelBuilder.Entity<Element>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.TransactionDate);
        }

    }
}
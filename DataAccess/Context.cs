using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SessionToken> SessionTokens { get; set; }
    public DbSet<Purchase> Carts { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DataAccess"));
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Colors)
            .WithMany()
            .UsingEntity(j => j.ToTable("ProductColor"));

        modelBuilder.Entity<Purchase>()
            .HasMany(p => p.Products)
            .WithMany()
            .UsingEntity(j => j.ToTable("PurchaseProduct"));
    }
}
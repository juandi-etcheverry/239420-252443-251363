using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SessionToken> SessionTokens { get; set; }
        public DbSet<Purchase> Carts { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DataAccess"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Session)
                .WithOne(s => s.User)
                .HasForeignKey<SessionToken>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
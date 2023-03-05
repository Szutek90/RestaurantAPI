using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext: DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=RestaurantDb;Trusted_Connection=true;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().Property(n => n.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Restaurant>().Property(i=>i.CreatedById).IsRequired().HasDefaultValue(1);
            modelBuilder.Entity<User>().Property(n => n.Email).IsRequired();
            modelBuilder.Entity<User>().Property(n => n.FirstName).IsRequired(false);
            modelBuilder.Entity<User>().Property(n => n.LastName).IsRequired(false);
            modelBuilder.Entity<User>().Property(n => n.Nationality).IsRequired(false);
            modelBuilder.Entity<Role>().Property(n => n.Name).IsRequired();
            modelBuilder.Entity<Dish>().Property(n => n.Name).IsRequired();
            modelBuilder.Entity<Dish>().Property(n => n.Description).IsRequired(false);
            modelBuilder.Entity<Dish>().Property(p => p.Price).HasColumnType("decimal(18,4)");
            modelBuilder.Entity<Address>().Property(n => n.City).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Address>().Property(n => n.Street).IsRequired().HasMaxLength(50);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

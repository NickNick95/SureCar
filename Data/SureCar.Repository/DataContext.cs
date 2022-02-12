using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SureCar.Entities;

namespace SureCar.Repositories
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {

        public DataContext() { }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=.\\SQLEXPRESS;Database=SureCar;User Id=sa;Password=Qwerty!23456;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleOrder>()
            .HasKey(v => new { v.OrderId, v.VehicleId });

            modelBuilder.Entity<VehicleOrder>()
                .HasOne(o => o.Order)
                .WithMany(v => v.VehicleOrders)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<VehicleOrder>()
                .HasOne(v => v.Vehicle)
                .WithMany(v => v.VehicleOrders)
                .HasForeignKey(v => v.VehicleId);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<VehicleOrder> VehicleOrders { get; set; }
    }
}

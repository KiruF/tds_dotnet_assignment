using CarParkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarParkAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<VehiclePricing> Pricing { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ParkingSpace is principal, Vehicle is dependent
            modelBuilder.Entity<ParkingSpace>()
                .HasOne(p => p.ParkedVehicle)
                .WithOne(v => v.ParkedIn)
                .HasForeignKey<Vehicle>(v => v.ParkedInID);

            // ParkingSpace indexing by SpaceNumber
            modelBuilder.Entity<ParkingSpace>()
                .HasIndex(p => p.SpaceNumber)
                .IsUnique();

            // Vehicle indexing by Reg
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.Reg)
                .IsUnique();
        }
    }
}

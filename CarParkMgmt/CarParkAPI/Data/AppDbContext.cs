using CarParkAPI.Models;
using Microsoft.EntityFrameworkCore;
using static CarParkAPI.Models.VehicleTypeConverter;

namespace CarParkAPI.Data
{
    public class AppDbContext : DbContext
    {
        Dictionary<VehicleType, double> _pricing;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehiclePricing> Pricing { get; set; }

        public IQueryable<ParkingSpace> GetParkingSpacesOrdered()
            => ParkingSpaces
            .Include(ps => ps.ParkedVehicle)
            .OrderBy(ps => ps.SpaceNumber);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ParkingSpace is principal, Vehicle is dependent
            modelBuilder.Entity<ParkingSpace>()
                .HasOne(p => p.ParkedVehicle)
                .WithOne(v => v.ParkingSpace)
                .HasForeignKey<Vehicle>(v => v.ParkingSpaceID);

            // ParkingSpace indexing by SpaceNumber
            modelBuilder.Entity<ParkingSpace>()
                .HasIndex(p => p.SpaceNumber)
                .IsUnique();

            // Vehicle indexing by Reg
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.Reg)
                .IsUnique();
        }

        public bool TryGetPricing(out Dictionary<VehicleType, double> pricing)
        {            
            if (_pricing == null)
            {
                pricing = null!;

                _pricing = new Dictionary<VehicleType, double>(Pricing.Count());
                foreach (var price in Pricing)
                {
                    var vType = ToVehicleType(price.VehicleType);
                    if (!_pricing.TryAdd(vType, price.PoundsPerMinute))
                        return false;
                }
            }

            pricing = _pricing;
            return true;
        }
    }
}

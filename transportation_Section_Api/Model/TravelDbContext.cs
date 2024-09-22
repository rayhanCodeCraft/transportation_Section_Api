using Microsoft.EntityFrameworkCore;

namespace transportation_Section_Api.Model
{
    public class TravelDbContext : DbContext
    {
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Transportation> Transportations { get; set; }
        public DbSet<TransportationType> TransportationTypes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageTransportation> PackageTransportations { get; set; }

        
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        { 
        
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<TransportationType>().HasData(
                new TransportationType { TransportationTypeId = 1, TypeName = "Bus" },
                new TransportationType { TransportationTypeId = 2, TypeName = "Train" },
                new TransportationType { TransportationTypeId = 3, TypeName = "Airplane" }
            );

            modelBuilder.Entity<Provider>().HasData(
          new Provider { ProviderId = 1, Name = "Hanif", Address = "123 Main St, Dhaka", CompanyName = "Company A", ContactNumber = "123-456-7890" },
           new Provider { ProviderId = 2, Name = "Nabil", Address = "456 Elm St, Rangamati", CompanyName = "Company B", ContactNumber = "123-456-755" }

            );

            modelBuilder.Entity<Transportation>().HasData(
                new Transportation
                {
                    TransportationId = 1,
                    TransportationTypeId = 1,
                    DepartureLocation = "Dhaka",
                    DepartureDate = DateTime.Now.AddDays(1),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(2),
                    IsActive = true,
                    ProviderId = 1,
                    Description = "Bus from Dhaka to RangaMati",
                    Rating = 5
                },
                new Transportation
                {
                    TransportationId = 2,
                    TransportationTypeId = 2,
                    DepartureLocation = "Laxmipur",
                    DepartureDate = DateTime.Now.AddDays(2),
                    ArrivalTime = DateTime.Now.AddDays(2).AddHours(3),
                    IsActive = true,
                    ProviderId = 2,
                    Description = "Train from Laxmipur to Rangpur",
                    Rating = 4
                }
            );

            modelBuilder.Entity<Package>().HasData(
                new Package { PackageId = 1, Name = "Weekend Getaway" },
                new Package { PackageId = 2, Name = "Adventure Trip" }
            );

            modelBuilder.Entity<PackageTransportation>().HasData(
                new PackageTransportation { PackageTransportationId = 1, PackageId = 1, TransportationId = 1 },
                new PackageTransportation { PackageTransportationId = 2, PackageId = 2, TransportationId = 2 }
            );
        }


    }
}

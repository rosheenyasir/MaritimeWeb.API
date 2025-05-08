using Microsoft.EntityFrameworkCore;
using MariTimeWeb.API.Models;

namespace MariTimeWeb.API.Data
{
    public class MariTimeDbContext : DbContext
    {
        public MariTimeDbContext(DbContextOptions<MariTimeDbContext> options) : base(options) { }

        // DbSets represent the tables in the database
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<VisitedCountry> VisitedCountries { get; set; }
        public DbSet<Voyage> Voyages { get; set; }

        // Overriding OnModelCreating to configure relationships and keys
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primary keys
            modelBuilder.Entity<VisitedCountry>().HasKey(v => v.VisitedCountryId);
            modelBuilder.Entity<Ship>().HasKey(s => s.ShipId);
            modelBuilder.Entity<Port>().HasKey(p => p.PortId);
            modelBuilder.Entity<Voyage>().HasKey(v => v.VoyageId);

            // VisitedCountry relationships
            modelBuilder.Entity<VisitedCountry>()
                .HasOne(v => v.Ship)
                .WithMany(s => s.VisitedCountries)
                .HasForeignKey(v => v.ShipId)
                .OnDelete(DeleteBehavior.Cascade); // OK for single cascade

            // Voyage ↔ Ship
            modelBuilder.Entity<Voyage>()
                .HasOne(v => v.Ship)
                .WithMany(s => s.Voyages)
                .HasForeignKey(v => v.ShipId)
                .OnDelete(DeleteBehavior.Cascade); // OK

            // Voyage ↔ DeparturePort (restrict to prevent cascade conflict)
            modelBuilder.Entity<Voyage>()
                .HasOne(v => v.DeparturePort)
                .WithMany(p => p.DepartureVoyages)
                .HasForeignKey(v => v.DeparturePortId)
                .OnDelete(DeleteBehavior.Restrict); // 👈 IMPORTANT

            modelBuilder.Entity<Voyage>()
                .HasOne(v => v.ArrivalPort)
                .WithMany(p => p.ArrivalVoyages)
                .HasForeignKey(v => v.ArrivalPortId);
     
        }
    }
}

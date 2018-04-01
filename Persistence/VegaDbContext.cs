using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models{ get; set; }

        public DbSet<Feature> Features { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API para definir Composite PK 
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => 
            new { vf.VehicleId, vf.FeatureId });
        }

        public VegaDbContext(DbContextOptions<VegaDbContext> options) 
            : base(options)
        {
            
        }        
    }
}
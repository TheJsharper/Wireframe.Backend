using Microsoft.EntityFrameworkCore;
using Wireframe.Backend.Models;

namespace Wireframe.Backend.Contexts
{
    public class WireframeContext:DbContext
    {
        public DbSet<WireframeModel> WireframeModel { get; set; }

        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WireframeModel>()
                .HasMany(e => e.Devices)
                .WithOne(e => e.WireframeModel)
                .HasForeignKey("WireframeId")
                .IsRequired(false)
                .HasPrincipalKey(e =>e.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "WireframeDb");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}

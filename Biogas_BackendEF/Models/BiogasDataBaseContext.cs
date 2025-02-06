using Microsoft.EntityFrameworkCore;
using Biogas_BackendEF.Models;

namespace Biogas_BackendEF.Models
{
    public class BiogasDataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WasteContributor> WasteContributors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Biogas> Biogas { get; set; }

        // Renamed to BiogasOrders for clarity
        public DbSet<Order> BiogasOrders { get; set; }

        public BiogasDataBaseContext(DbContextOptions options) : base(options)
        {
        }

        // OnModelCreating can be used here for further configurations if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Foreign Key relationship between Orders and Users
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);  // Use NoAction to avoid cascading delete

            // Foreign Key relationship between Orders and Biogas
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Biogas)
                .WithMany()
                .HasForeignKey(o => o.BiogasId)
                .OnDelete(DeleteBehavior.Cascade); // Biogas can still be deleted via cascading
        }

    }
}

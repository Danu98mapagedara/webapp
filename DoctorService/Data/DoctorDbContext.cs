using DoctorService.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Data
{
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<AvailableSlot> AvailableSlots { get; set; }
 
 
   protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Slots)
                .WithOne(s => s.Doctor)
                .HasForeignKey(s => s.DoctorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
    
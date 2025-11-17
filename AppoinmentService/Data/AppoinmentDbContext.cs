namespace AppoinmentService.Data
{
  
    using Microsoft.EntityFrameworkCore;

    public class AppoinmentDbContext : DbContext
    {
        public AppoinmentDbContext(DbContextOptions<AppoinmentDbContext> options) : base(options)
        {
        }

        public DbSet<Appoinment> Appoinments { get; set; }
    }
}
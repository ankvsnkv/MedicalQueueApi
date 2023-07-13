using Microsoft.EntityFrameworkCore;
using MedicalQueueApi.Models;

namespace MedicalQueueApi.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<TypePage> TypePages { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<ColorScheme> ColorSchemes { get; set; }
        public DbSet<Display> Displays { get; set; }
        public DbSet<Monitoring> MonitoringData { get; set; }
    }
}

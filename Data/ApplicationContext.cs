using Microsoft.EntityFrameworkCore;
using MedicalQueueApi.Models;

namespace MedicalQueueApi.Data {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { 
            //Database.EnsureCreated();
        }

    }
}

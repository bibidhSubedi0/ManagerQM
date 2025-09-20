using Microsoft.EntityFrameworkCore;
using ManagerQM.Models;

namespace ManagerQM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserTask> Tasks { get; set; }
        
    }
}

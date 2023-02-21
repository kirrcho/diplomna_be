using Microsoft.EntityFrameworkCore;

namespace Diplomna.Models
{
    public class DiplomnaContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DiplomnaContext() { }

        public DiplomnaContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // manually add options on executing update-database due to bugged .net version
            
            base.OnConfiguring(options);
        }
    }
}

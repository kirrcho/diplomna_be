using Microsoft.EntityFrameworkCore;

namespace Diplomna.Models
{
    public class DiplomnaContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Tutor> Staff { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DiplomnaContext() { }

        public DiplomnaContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }
    }
}

namespace Diplomna.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime TimeScanned { get; set; }

        public bool PresenceConfirmed { get; set; }

        public DateTime? PresenceConfirmedTime { get; set; }

        public int Room { get; set; }
    }
}

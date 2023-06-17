namespace Diplomna.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime TimeScanned { get; set; }

        public bool PresenceConfirmed { get; set; }

        public DateTime? PresenceConfirmedTime { get; set; }

        public int? TutorId { get; set; }

        public Tutor? Tutor { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}

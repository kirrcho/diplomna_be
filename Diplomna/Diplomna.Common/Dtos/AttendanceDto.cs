namespace Diplomna.Common.Dtos
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }

        public string TimeScanned { get; set; }

        public bool PresenceConfirmed { get; set; }

        public string? PresenceConfirmedTime { get; set; }

        public int? TutorId { get; set; }

        public string? TutorEmail { get; set; }

        public int RoomNumber { get; set; }

        public int RoomId { get; set; }
    }
}

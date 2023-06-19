namespace Diplomna.Common.Dtos
{
    public class AttendanceRoomDto
    {
        public int AttendanceId { get; set; }

        public int UserId { get; set; }

        public string FacultyNumber { get; set; }

        public string FullName { get; set; }

        public string TimeScanned { get; set; }

        public bool PresenceConfirmed { get; set; }

        public string? PresenceConfirmedTime { get; set; }

        public int? TutorId { get; set; }

        public string? TutorEmail { get; set; }
    }
}

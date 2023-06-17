namespace Diplomna.Common.Dtos
{
    public class AddAttendanceDto
    {
        public string FacultyNumber { get; set; }

        public int RoomId { get; set; }

        public DateTime DayAttended { get; set; }
    }
}

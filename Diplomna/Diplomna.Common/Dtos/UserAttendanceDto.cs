namespace Diplomna.Common.Dtos
{
    public class UserAttendanceDto
    {
        public string FacultyNumber { get; set; }

        public IEnumerable<AttendanceDto> Attendances { get; set; }
    }
}

namespace Diplomna.Common.Dtos
{
    public class UserAttendanceDto
    {
        public string FacultyNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GroupId { get; set; }

        public int Year { get; set; }

        public string GroupNumber { get; set; }

        public IEnumerable<AttendanceDto> Attendances { get; set; }
    }
}

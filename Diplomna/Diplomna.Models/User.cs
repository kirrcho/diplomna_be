namespace Diplomna.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FacultyNumber { get; set; }

        public bool IsConfirmed { get; set; }

        public List<Attendance> Attendances { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
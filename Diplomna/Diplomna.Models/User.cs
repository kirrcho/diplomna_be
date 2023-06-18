namespace Diplomna.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FacultyNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }

        public bool IsConfirmed { get; set; }

        public List<Attendance> Attendances { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
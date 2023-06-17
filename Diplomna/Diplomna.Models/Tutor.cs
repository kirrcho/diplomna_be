namespace Diplomna.Models
{
    public class Tutor
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime? LastLogin { get; set; }

        public List<Attendance> Attendances { get; set; }
    }
}

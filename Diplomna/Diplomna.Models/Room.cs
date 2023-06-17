namespace Diplomna.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public List<Attendance> Attendances { get; set; }
    }
}

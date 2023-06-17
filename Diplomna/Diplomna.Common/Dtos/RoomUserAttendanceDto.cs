namespace Diplomna.Common.Dtos
{
    public class RoomUserAttendanceDto
    {
        public int RoomNumber { get; set; }

        public IEnumerable<AttendanceRoomDto> Attendances { get; set; }
    }
}

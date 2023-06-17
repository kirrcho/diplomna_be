namespace Diplomna.Common.Dtos
{
    public class RoomDetailsDto
    {
        public IEnumerable<RoomDto> Rooms { get; set; }

        public bool IsNextPage { get; set; }
    }
}

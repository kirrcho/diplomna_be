namespace Diplomna.Common.Dtos
{
    public class GroupUserDto
    {
        public string GroupNumber { get; set; }

        public int StartYear { get; set; }

        public IEnumerable<UserDto> Students { get; set; }
    }
}

namespace Diplomna.Common.Dtos
{
    public class GroupDetailsDto
    {
        public IEnumerable<GroupDto> Groups { get; set; }

        public bool IsNextPage { get; set; }
    }
}

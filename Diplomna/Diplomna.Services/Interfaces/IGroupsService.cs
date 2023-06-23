using Diplomna.Common;
using Diplomna.Common.Dtos;

namespace Diplomna.Services.Interfaces
{
    public interface IGroupsService
    {
        Task<Result<GroupDetailsDto>> GetGroupsAsync(int page, int pageSize);

        Task<Result<GroupUserDto>> GetGroupDetailsAsync(int startYear, int groupId);

        Task<Result<bool>> UpdateGroups();
    }
}

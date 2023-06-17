using Diplomna.Common;
using Diplomna.Common.Dtos;

namespace Diplomna.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<Result<bool>> AddAttendanceAsync(AddAttendanceDto dto, int tutorId);

        Task<Result<UserAttendanceDto>> GetUserAttendancesAsync(DateTime day, int userId);
    }
}

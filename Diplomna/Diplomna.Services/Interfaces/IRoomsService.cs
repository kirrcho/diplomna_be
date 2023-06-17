using Diplomna.Common;
using Diplomna.Common.Dtos;

namespace Diplomna.Services.Interfaces
{
    public interface IRoomsService
    {
        Task<Result<RoomDetailsDto>> GetRoomsAsync(int page, int pageSize);

        Task<Result<bool>> AddRoomAsync(AddRoomDto addRoomDto);

        Task<Result<RoomUserAttendanceDto>> GetRoomDetailsAsync(DateTime day, int roomId);

        Task<Result<bool>> ConfirmAttendanceAsync(int attendanceId, int tutorId);
    }
}
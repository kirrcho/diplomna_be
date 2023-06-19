using Diplomna.Common;
using Diplomna.Common.Dtos;

namespace Diplomna.Services.Interfaces
{
    public interface IUserService
    {
        public Task<Result<string>> LoginMobileAsync(LoginMobileRequest request);

        public Task<Result<bool>> RegisterMobileAsync(RegisterMobileRequest request);

        public Task<Result<string>> LoginAsync(string token);

        public Task<Result<bool>> RegisterAsync(string token);

        Task<Result<UserAttendanceDto>> GetUserAttendancesAsync(DateTime day, int userId);

        Task<IEnumerable<UnconfirmedUserDto>> GetUnconfirmedUsers();

        Task<Result<bool>> ConfirmUserAccount(int userId);

        Task<Result<bool>> RemoveUserAccount(int userId);

        Task<IEnumerable<UserDto>> FindUsersBySearchPhrase(string searchPhrase);
    }
}

using Diplomna.Common;
using Diplomna.Models.Dtos;

namespace Diplomna.Services
{
    public interface IUserService
    {
        public Task<Result<string>> LoginAsync(LoginRequest request);

        public Task<Result<bool>> RegisterAsync(LoginRequest request);
    }
}

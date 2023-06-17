using Diplomna.Common;
using Diplomna.Common.Dtos;

namespace Diplomna.Services.Interfaces
{
    public interface IUserService
    {
        public Task<Result<string>> LoginMobileAsync(LoginMobileRequest request);

        public Task<Result<bool>> RegisterMobileAsync(LoginMobileRequest request);

        public Task<Result<string>> LoginAsync(string token);

        public Task<Result<bool>> RegisterAsync(string token);
    }
}

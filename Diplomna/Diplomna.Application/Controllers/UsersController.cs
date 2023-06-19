using Diplomna.Common;
using Diplomna.Common.Dtos;
using Diplomna.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diplomna.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("/users/{userId}")]
        public async Task<Result<bool>> ConfirmUserAccount(int userId)
        {
            _logger.LogInformation($"{nameof(ConfirmUserAccount)} triggered.");

            return await _userService.ConfirmUserAccount(userId);
        }

        [HttpGet("/users/{userId}")]
        public async Task<Result<UserAttendanceDto>> GetUserAttendances(DateTime day, int userId)
        {
            _logger.LogInformation($"{nameof(GetUserAttendances)} triggered.");

            return await _userService.GetUserAttendancesAsync(day, userId);
        }

        [HttpGet("/unconfirmedUsers")]
        public async Task<Result<IEnumerable<UnconfirmedUserDto>>> GetUnconfirmedUsers()
        {
            _logger.LogInformation($"{nameof(GetUnconfirmedUsers)} triggered.");

            var users = await _userService.GetUnconfirmedUsers();

            return Result<IEnumerable<UnconfirmedUserDto>>.OkResult(users);
        }

        [HttpPost("/users/confirm")]
        public async Task<Result<bool>> ConfirmAccount([FromBody] int userId)
        {
            _logger.LogInformation($"{nameof(ConfirmAccount)} triggered.");

            return await _userService.ConfirmUserAccount(userId);
        }

        [HttpPost("/users/remove")]
        public async Task<Result<bool>> RemoveUnconfirmedUser([FromBody] int userId)
        {
            _logger.LogInformation($"{nameof(RemoveUnconfirmedUser)} triggered.");

            return await _userService.RemoveUserAccount(userId);
        }

        [HttpGet("/users/search")]
        public async Task<Result<IEnumerable<UserDto>>> FindUsers([FromQuery] string phrase)
        {
            _logger.LogInformation($"{nameof(FindUsers)} triggered.");

            var users = await _userService.FindUsersBySearchPhrase(phrase);

            return Result<IEnumerable<UserDto>>.OkResult(users);
        }
    }
}

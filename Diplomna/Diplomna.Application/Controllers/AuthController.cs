using Diplomna.Common;
using Diplomna.Common.Dtos;
using Diplomna.Common.Validators;
using Diplomna.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diplomna.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly LoginRequestValidator _loginRequestValidator;
        private readonly RegisterMobileRequestValidator _registerMobileRequestValidator;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> log,
            IUserService userService,
            LoginRequestValidator loginRequestValidator,
            RegisterMobileRequestValidator registerMobileRequestValidator)
        {
            _logger = log;
            _userService = userService;
            _loginRequestValidator = loginRequestValidator;
            _registerMobileRequestValidator = registerMobileRequestValidator;
        }

        [HttpPost("LoginMob")]
        public async Task<Result<string>> LoginMobAsync([FromBody] LoginMobileRequest request)
        {
            _logger.LogInformation($"{nameof(LoginMobAsync)} triggered.");

            var validationResult = _loginRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var error = string.Join("", validationResult.Errors);
                _logger.LogError(error);
                return Result<string>.BadResult(error);
            }

            var result = await _userService.LoginMobileAsync(request);
            if (!result.IsSuccessful)
            {
                _logger.LogError(result.Error);
                return Result<string>.BadResult(result.Error);
            }

            _logger.LogInformation($"{nameof(LoginMobAsync)} completed.");
            return result;
        }

        [HttpPost("RegisterMob")]
        public async Task<Result<bool>> RegisterMobAsync([FromBody] RegisterMobileRequest request)
        {
            _logger.LogInformation($"{nameof(RegisterMobAsync)} triggered.");

            var validationResult = _registerMobileRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var error = string.Join("", validationResult.Errors);
                _logger.LogError(error);
                return Result<bool>.BadResult(error);
            }

            var result = await _userService.RegisterMobileAsync(request);
            if (!result.IsSuccessful)
            {
                _logger.LogError(result.Error);
                return Result<bool>.BadResult(result.Error);
            }

            _logger.LogInformation($"{nameof(RegisterMobAsync)} completed.");
            return result;
        }

        [HttpPost("Login")]
        public async Task<Result<string>> LoginAsync(LoginRequest request)
        {
            _logger.LogInformation($"{nameof(LoginAsync)} triggered.");

            var result = await _userService.LoginAsync(request.Token);
            if (!result.IsSuccessful)
            {
                _logger.LogError(result.Error);
                return Result<string>.BadResult(result.Error);
            }

            _logger.LogInformation($"{nameof(LoginAsync)} completed.");
            return result;
        }

        [HttpPost("Register")]
        public async Task<Result<bool>> RegisterAsync(LoginRequest request)
        {
            _logger.LogInformation($"{nameof(RegisterAsync)} triggered.");

            var result = await _userService.RegisterAsync(request.Token);
            if (!result.IsSuccessful)
            {
                _logger.LogError(result.Error);
                return Result<bool>.BadResult(result.Error);
            }

            _logger.LogInformation($"{nameof(RegisterAsync)} completed.");
            return result;
        }
    }
}
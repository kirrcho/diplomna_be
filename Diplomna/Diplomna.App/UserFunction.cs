using Diplomna.Common;
using Diplomna.Common.Validators;
using Diplomna.Models.Dtos;
using Diplomna.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Diplomna.App
{
    public class UserFunction
    {
        private readonly ILogger<UserFunction> _logger;
        private readonly LoginRequestValidator _loginRequestValidator;
        private readonly IUserService _userService;

        public UserFunction(ILogger<UserFunction> log, IUserService userService, LoginRequestValidator loginRequestValidator)
        {
            _logger = log;
            _userService = userService;
            _loginRequestValidator = loginRequestValidator;
        }

        [Function("Login")]
        public async Task<Result<string>> LoginAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login")] HttpRequestData req)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(await req.ReadAsStringAsync());

            _logger.LogInformation($"{nameof(LoginAsync)} triggered.");

            var validationResult = _loginRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var error = string.Join("", validationResult.Errors);
                _logger.LogError(error);
                return Result<string>.BadResult(error);
            }

            var result = await _userService.LoginAsync(request);
            if (!result.IsSuccessful)
            {
                var error = string.Join("", validationResult.Errors);
                _logger.LogError(error);
                return Result<string>.BadResult(error);
            }

            _logger.LogInformation($"{nameof(LoginAsync)} completed.");
            return result;
        }

        [Function("Register")]
        public async Task<Result<bool>> RegisterAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "register")] HttpRequestData req)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(await req.ReadAsStringAsync());

            _logger.LogInformation($"{nameof(RegisterAsync)} triggered.");

            var validationResult = _loginRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var error = string.Join("", validationResult.Errors);
                _logger.LogError(error);
                return Result<bool>.BadResult(error);
            }

            var result = await _userService.RegisterAsync(request);
            if (!result.IsSuccessful)
            {
                var error = string.Join("", validationResult.Errors);
                _logger.LogError(error);
                return Result<bool>.BadResult(error);
            }

            _logger.LogInformation($"{nameof(RegisterAsync)} completed.");
            return result;
        }
    }
}

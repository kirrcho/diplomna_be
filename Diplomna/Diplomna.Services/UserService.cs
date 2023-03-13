using Diplomna.Common;
using Diplomna.Common.Auth;
using Diplomna.Common.Constants;
using Diplomna.Models;
using Diplomna.Models.Dtos;
using Diplomna.Services.Interfaces;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Diplomna.Services
{
    public class UserService : IUserService
    {
        private DiplomnaContext _context;
        private AuthConstants _authConstants;

        public UserService(DiplomnaContext context, AuthConstants authConstants)
        {
            _context = context;
            _authConstants = authConstants;
        }

        public async Task<Result<string>> LoginAsync(LoginRequest request)
        {
            var payload = await GetPayloadAsync(request.Token);
            if (payload == null)
            {
                return Result<string>.BadResult("Invalid request. Contact administration for assistance");
            }

            var user = await _context.Users.FirstOrDefaultAsync(p => p.FacultyNumber == request.FacultyNumber && payload.Email == p.Email);
            if (user == null)
            {
                return Result<string>.BadResult("Invalid request.");
            }
            if (!user.IsConfirmed)
            {
                return Result<string>.BadResult("This account is not yet confirmed as valid.");
            }

            var claims = new List<Claim>
            {
                new Claim("fn", request.FacultyNumber),
                new Claim("email", payload.Email),
                new Claim("exp", DateTime.UtcNow.AddHours(1).ToString())
            };

            var token = Auth.CreateToken(claims, _authConstants.PrivateKey);

            return Result<string>.OkResult(token);
        }

        public async Task<Result<bool>> RegisterAsync(LoginRequest request)
        {
            var payload = await GetPayloadAsync(request.Token);
            if (payload == null)
            {
                return Result<bool>.BadResult("Invalid request. Contact administration for assistance");
            }

            var user = await _context.Users.FirstOrDefaultAsync(p => p.FacultyNumber == request.FacultyNumber || payload.Email == p.Email);
            if (user != null)
            {
                return Result<bool>.BadResult("User already exists.");
            }

            var newUser = new User()
            {
                Email = payload.Email,
                FacultyNumber = request.FacultyNumber,
                IsConfirmed = false,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Result<bool>.OkResult(true);
        }

        private async Task<GoogleJsonWebSignature.Payload?> GetPayloadAsync(string token)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(token);
            }
            catch (InvalidJwtException)
            {
                return null;
            }

            return payload;
        }
    }
}
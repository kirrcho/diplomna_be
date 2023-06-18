using System.Security.Claims;
using Diplomna.Common;
using Diplomna.Common.Auth;
using Diplomna.Common.Constants;
using Diplomna.Common.Dtos;
using Diplomna.Models;
using Diplomna.Services.Interfaces;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Result<string>> LoginMobileAsync(LoginMobileRequest request)
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

        public async Task<Result<bool>> RegisterMobileAsync(RegisterMobileRequest request)
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

            var group = await _context.Groups.FirstOrDefaultAsync(p => p.GroupNumber == request.Group);
            if (group is null)
            {
                return Result<bool>.BadResult("Invalid group.");
            }

            var newUser = new User()
            {
                Email = payload.Email,
                FacultyNumber = request.FacultyNumber,
                GroupId = group.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsConfirmed = false,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Result<bool>.OkResult(true);
        }

        public async Task<Result<string>> LoginAsync(string payloadToken)
        {
            var payload = await GetPayloadAsync(payloadToken);
            if (payload == null)
            {
                return Result<string>.BadResult("Invalid request. Contact administration for assistance");
            }

            var tutor = await _context.Staff.FirstOrDefaultAsync(p => p.Email == payload.Email);
            if (tutor == null)
            {
                return Result<string>.BadResult("Invalid request.");
            }
            if (!tutor.IsConfirmed)
            {
                return Result<string>.BadResult("This account is not yet confirmed as valid.");
            }

            var claims = new List<Claim>
            {
                new Claim("id", tutor.Id.ToString()),
                new Claim("email", payload.Email),
                new Claim("exp", DateTime.UtcNow.AddHours(1).ToString())
            };

            var token = Auth.CreateToken(claims, _authConstants.PrivateKey);

            return Result<string>.OkResult(token);
        }

        public async Task<Result<bool>> RegisterAsync(string payloadToken)
        {
            var payload = await GetPayloadAsync(payloadToken);
            if (payload == null)
            {
                return Result<bool>.BadResult("Invalid request. Contact administration for assistance");
            }

            var user = await _context.Staff.FirstOrDefaultAsync(p => payload.Email == p.Email);
            if (user != null)
            {
                return Result<bool>.BadResult("User already exists.");
            }

            var newUser = new Tutor()
            {
                Email = payload.Email,
                IsConfirmed = false,
            };

            await _context.Staff.AddAsync(newUser);
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
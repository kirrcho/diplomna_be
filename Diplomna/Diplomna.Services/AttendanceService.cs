using Diplomna.Common;
using Diplomna.Common.Dtos;
using Diplomna.Models;
using Diplomna.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diplomna.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly DiplomnaContext _context;

        public AttendanceService(DiplomnaContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> AddAttendanceAsync(AddAttendanceDto dto, int tutorId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.FacultyNumber == dto.FacultyNumber);
            if (user is null && user.IsConfirmed == false)
            {
                return Result<bool>.BadResult("Invalid faculty number");
            }

            _context.Attendances.Add(new Attendance()
            {
                PresenceConfirmed = true,
                PresenceConfirmedTime = DateTime.UtcNow,
                RoomId = dto.RoomId,
                TimeScanned = dto.DayAttended.Date,
                TutorId = tutorId,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
            return Result<bool>.OkResult(true);
        }

        public async Task<Result<UserAttendanceDto>> GetUserAttendancesAsync(DateTime day, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);
            if (user is null)
            {
                return Result<UserAttendanceDto>.BadResult("Invalid user id");
            }

            var attendances = await _context.Attendances
                .Include(p => p.Tutor)
                .Include(p => p.Room)
                .Where(p => day.Date == p.TimeScanned.Date && p.UserId == userId)
                .Select(p => new AttendanceDto()
                {
                    AttendanceId = p.Id,
                    RoomId = p.RoomId,
                    RoomNumber = p.Room.RoomNumber,
                    PresenceConfirmed = p.PresenceConfirmed,
                    PresenceConfirmedTime = p.PresenceConfirmedTime != null ? p.PresenceConfirmedTime.Value.ToString("yyyy-MM-dd/HH:mm:ss") : string.Empty,
                    TimeScanned = p.TimeScanned.ToString("yyyy-MM-dd/HH:mm:ss"),
                    TutorEmail = p.Tutor != null ? p.Tutor.Email : null,
                    TutorId = p.TutorId

                })
                .ToListAsync();

            return Result<UserAttendanceDto>.OkResult(new UserAttendanceDto()
            {
                FacultyNumber = user.FacultyNumber,
                Attendances = attendances
            });
        }
    }
}

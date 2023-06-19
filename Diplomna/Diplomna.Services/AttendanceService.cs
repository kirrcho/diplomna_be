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
    }
}

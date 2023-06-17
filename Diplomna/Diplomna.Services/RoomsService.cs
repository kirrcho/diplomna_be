using Diplomna.Common;
using Diplomna.Common.Dtos;
using Diplomna.Models;
using Diplomna.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diplomna.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly DiplomnaContext _context;

        public RoomsService(DiplomnaContext context)
        {
            _context = context;
        }

        public async Task<Result<RoomDetailsDto>> GetRoomsAsync(int page, int pageSize)
        {
            var rooms = await _context.Rooms
                .Skip((page - 1) * pageSize)
                .Take(pageSize + 1)
                .ToListAsync();

            var isNextPage = true;
            if (rooms.Count != pageSize + 1)
            {
                isNextPage = false;
            }

            var result = new RoomDetailsDto()
            {
                Rooms = rooms.Take(pageSize).Select(p => new RoomDto() { Id = p.Id, RoomNumber = p.RoomNumber }),
                IsNextPage = isNextPage
            };

            return Result<RoomDetailsDto>.OkResult(result);
        }

        public async Task<Result<bool>> AddRoomAsync(AddRoomDto addRoomDto)
        {
            await _context.Rooms.AddAsync(new Room() { RoomNumber = addRoomDto.RoomNumber });
            await _context.SaveChangesAsync();

            return Result<bool>.OkResult(true);
        }

        public async Task<Result<RoomUserAttendanceDto>> GetRoomDetailsAsync(DateTime day, int roomId)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(p => p.Id == roomId);
            if (room is null)
            {
                return Result<RoomUserAttendanceDto>.BadResult("Invalid room id");
            }

            var attendances = await _context.Attendances
                    .Include(p => p.Tutor)
                    .Include(p => p.User)
                    .Where(p => p.TimeScanned.Date == day.Date && p.RoomId == roomId)
                    .Select(p => new AttendanceRoomDto()
                    {
                        AttendanceId = p.Id,
                        FacultyNumber = p.User.FacultyNumber,
                        PresenceConfirmed = p.PresenceConfirmed,
                        PresenceConfirmedTime = p.PresenceConfirmedTime != null ? p.PresenceConfirmedTime.Value.ToString("yyyy-MM-dd/HH:mm:ss") : string.Empty,
                        TimeScanned = p.TimeScanned.ToString("yyyy-MM-dd/HH:mm:ss"),
                        TutorEmail = p.Tutor != null ? p.Tutor.Email : null,
                        UserId = p.UserId,
                        TutorId = p.TutorId,
                    })
                    .ToListAsync();

            return Result<RoomUserAttendanceDto>.OkResult(new RoomUserAttendanceDto()
            {
                RoomNumber = room.RoomNumber,
                Attendances = attendances
            });
        }

        public async Task<Result<bool>> ConfirmAttendanceAsync(int attendanceId, int tutorId)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(p => p.Id == attendanceId);
            if (attendance is null)
            {
                return Result<bool>.BadResult("invalid attendance id");
            }

            attendance.TutorId = tutorId;
            attendance.PresenceConfirmed = true;
            attendance.PresenceConfirmedTime = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Result<bool>.OkResult(true);
        }
    }
}

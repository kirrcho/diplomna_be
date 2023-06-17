using Diplomna.Common;
using Diplomna.Common.Dtos;
using Diplomna.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diplomna.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsService _roomsService;
        private readonly ILogger<RoomsController> _logger;

        public RoomsController(ILogger<RoomsController> log, IRoomsService roomsService)
        {
            _roomsService = roomsService;
            _logger = log;
        }

        [HttpGet]
        public async Task<Result<RoomDetailsDto>> Rooms(int page, int pageSize = 10)
        {
            _logger.LogInformation($"{nameof(Rooms)} triggered.");

            if (page < 1 || pageSize < 1)
            {
                return Result<RoomDetailsDto>.BadResult("Invalid query parameters");
            }

            return await _roomsService.GetRoomsAsync(page, pageSize);
        }

        [HttpPost]
        public async Task<Result<bool>> AddRoom(AddRoomDto addRoomDto)
        {
            _logger.LogInformation($"{nameof(AddRoom)} triggered.");

            return await _roomsService.AddRoomAsync(addRoomDto);
        }

        [HttpGet("/rooms/details/{roomId}")]
        public async Task<Result<RoomUserAttendanceDto>> GetRoomDetails(DateTime day, int roomId)
        {
            _logger.LogInformation($"{nameof(GetRoomDetails)} triggered.");

            return await _roomsService.GetRoomDetailsAsync(day, roomId);
        }

        [HttpPost("/confirmPresence")]
        public async Task<Result<bool>> ConfirmPresence([FromBody] int attendanceId)
        {
            _logger.LogInformation($"{nameof(ConfirmPresence)} triggered.");

            var tutorId = HttpContext.Items["tutorId"];
            if (tutorId is null)
            {
                return Result<bool>.BadResult("Invalid request. Contact administator for assistance");
            }

            return await _roomsService.ConfirmAttendanceAsync(attendanceId, int.Parse(tutorId.ToString() ?? string.Empty));
        }
    }
}

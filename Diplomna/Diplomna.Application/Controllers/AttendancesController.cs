using Diplomna.Common;
using Diplomna.Common.Dtos;
using Diplomna.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diplomna.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly ILogger<AttendancesController> _logger;

        public AttendancesController(ILogger<AttendancesController> log, IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
            _logger = log;
        }

        [HttpPost]
        public async Task<Result<bool>> AddAttendance([FromBody] AddAttendanceDto dto)
        {
            _logger.LogInformation($"{nameof(AddAttendance)} triggered.");

            if (dto.DayAttended.Year < 2000)
            {
                return Result<bool>.BadResult("Please select a valid date.");
            }

            var tutorId = HttpContext.Items["tutorId"];
            if (tutorId is null)
            {
                return Result<bool>.BadResult("Invalid request. Contact administator for assistance");
            }

            return await _attendanceService.AddAttendanceAsync(dto, int.Parse(tutorId.ToString() ?? string.Empty));
        }
    }
}

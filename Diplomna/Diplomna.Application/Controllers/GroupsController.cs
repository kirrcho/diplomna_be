using Diplomna.Common;
using Diplomna.Common.Dtos;
using Diplomna.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diplomna.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsService _groupsService;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IGroupsService groupsService, ILogger<GroupsController> logger)
        {
            _groupsService = groupsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result<GroupDetailsDto>> GetAllGroups(int page, int pageSize = 10)
        {
            _logger.LogInformation($"{nameof(GetAllGroups)} triggered.");

            if (page < 1 || pageSize < 1)
            {
                return Result<GroupDetailsDto>.BadResult("Invalid query parameters");
            }

            return await _groupsService.GetGroupsAsync(page, pageSize);
        }

        [HttpGet("/groups/details/{groupId}")]
        public async Task<Result<GroupUserDto>> GetRoomDetails(int year, int groupId)
        {
            _logger.LogInformation($"{nameof(GetRoomDetails)} triggered.");

            return await _groupsService.GetGroupDetailsAsync(year, groupId);
        }
    }
}
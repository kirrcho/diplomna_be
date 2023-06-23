using System.Threading.Tasks;
using Diplomna.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Diplomna.BE
{
    public class GroupFunctions
    {
        private readonly ILogger<GroupFunctions> _logger;
        private readonly IGroupsService _groupsService;

        public GroupFunctions(ILogger<GroupFunctions> logger, IGroupsService groupsService)
        {
            _logger = logger;
            _groupsService = groupsService;
        }

        [Function("NewGroups")]
        public async Task CreateNewYearGroups([TimerTrigger("0 0 2 1 *")] TimerInfo info)
        {
            _logger.LogInformation($"{CreateNewYearGroups} triggered");

            var result = await _groupsService.UpdateGroups();
            if (!result.IsSuccessful)
            {
                _logger.LogError(result.Error);
                return;
            }
        }
    }
}

using Diplomna.Common;
using Diplomna.Common.Dtos;
using Diplomna.Models;
using Diplomna.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diplomna.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly DiplomnaContext _context;

        public GroupsService(DiplomnaContext context)
        {
            _context = context;
        }

        public async Task<Result<GroupDetailsDto>> GetGroupsAsync(int page, int pageSize)
        {
            var groups = await _context.Groups
                .Skip((page - 1) * pageSize)
                .Take(pageSize + 1)
                .ToListAsync();

            var isNextPage = true;
            if (groups.Count != pageSize + 1)
            {
                isNextPage = false;
            }

            var result = new GroupDetailsDto()
            {
                Groups = groups.Take(pageSize).Select(p => new GroupDto() { Id = p.Id, GroupNumber = p.GroupNumber }),
                IsNextPage = isNextPage
            };

            return Result<GroupDetailsDto>.OkResult(result);
        }

        public async Task<Result<GroupUserDto>> GetGroupDetailsAsync(int startYear, int groupId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(p => p.Id == groupId);
            if (group is null)
            {
                return Result<GroupUserDto>.BadResult("Invalid group id");
            }

            var studentsInGroup = await _context.Users
                .Include(p => p.Group)
                .Where(p => p.StartYear == startYear && p.GroupId == groupId && p.IsConfirmed == true)
                .Select(p => new UserDto()
                {
                    Id = p.Id,
                    FacultyNumber = p.FacultyNumber,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                })
                .ToListAsync();

            return Result<GroupUserDto>.OkResult(new GroupUserDto()
            {
                GroupNumber = group.GroupNumber,
                StartYear = startYear,
                Students = studentsInGroup
            });
        }
    }
}

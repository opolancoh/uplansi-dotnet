using Microsoft.EntityFrameworkCore;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.Task;
using Uplansi.Core.Entities;

namespace Uplansi.Data.EntityFramework;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<ApplicationTask> _entitySet;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
        _entitySet = context.ApplicationTasks;
    }

    public async Task<PagedListResult<TaskMyListResult>> GetAll(PaginationOptions pagination, Guid userId)
    {
        var (pageIndex, pageSize, skip, addTotalCount) = pagination;

        // Get the total count of records
        int? totalCount = null;
        if (addTotalCount)
        {
            totalCount = await _entitySet.CountAsync();
        }

        var data = await _entitySet
            .Where(x => x.AssignedToId == userId)
            .Skip(skip)
            .Take(pageSize)
            .Select(
                x => new TaskMyListResult
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Priority = x.Priority,
                    DueDate = x.DueDate,
                    Progress = x.Progress,
                    Completed = x.Completed
                })
            .AsNoTracking()
            .ToListAsync();

        return new PagedListResult<TaskMyListResult>
        {
            Data = data,
            PageInfo = new PageInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            }
        };
    }

    public async Task<TaskMyDetailResult?> GetById(Guid id, Guid userId)
    {
        return await _entitySet
            .Where(x => x.Id == id && x.AssignedToId == userId)
            .AsNoTracking()
            .Select(
                x => new TaskMyDetailResult
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Priority = x.Priority,
                    DueDate = x.DueDate,
                    Progress = x.Progress,
                    Completed = x.Completed,
                    /* CreatedAt = x.CreatedAt,
                    CreatedBy = new KeyValueDto<Guid>
                    {
                        Id = x.CreatedBy!.Id,
                        Name = x.CreatedBy!.UserName!
                    }, 
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = new KeyValueDto<Guid>
                    {
                        Id = x.UpdatedBy!.Id,
                        Name = x.UpdatedBy!.UserName!
                    } */
                })
            .SingleOrDefaultAsync();
    }

    public async Task<int> Add(ApplicationTask item)
    {
        _entitySet.Add(item);
        return await _context.SaveChangesAsync();
    }
}
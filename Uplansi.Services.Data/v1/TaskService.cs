using System.Security.Claims;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.Contracts.Services;
using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.Task;
using Uplansi.Core.Entities;

namespace Uplansi.Services.Data.v1;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApplicationResult> GetAll(PaginationOptions pagination, IEnumerable<Claim> claims)
    {
        var userId = Guid.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var result = await _repository.GetAll(pagination, userId);

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.Ok,
            PageInfo = result.PageInfo,
            Data = result.Data
        };
    }

    public async Task<ApplicationResult?> GetById(Guid id, IEnumerable<Claim> claims)
    {
        var userId = Guid.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var item = await _repository.GetById(id, userId);

        if (item == null)
        {
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.NotFound,
                Message = $"The item with id '{id}' was not found or you don't have permission to access it."
            };
        }

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.Ok,
            Data = item
        };
    }

    public async Task<ApplicationResult> Add(TaskAddModel item, IEnumerable<Claim> claims)
    {
        var newItemId = Guid.NewGuid();
        var userId = Guid.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var isMyTask = userId == item.AssignedToId;
        var now = DateTime.UtcNow;

        var newItem = new ApplicationTask
        {
            Id = newItemId,
            Title = item.Title,
            Description = item.Description,
            Priority = item.Priority,
            DueDate = item.DueDate,
            Progress = 0,
            Completed = false,
            Acceptance = isMyTask ? TaskAcceptance.Accepted : TaskAcceptance.NotAccepted,
            GroupName = isMyTask ? "my" : item.GroupName,
            AssignedToId = item.AssignedToId,
            /* CreatedAt = now,
            CreatedById = userId,
            UpdatedAt = now,
            UpdatedById = userId */
        };

        var affectedRows = await _repository.Add(newItem);

        if (affectedRows > 0)
        {
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.Created,
                Message = "Item created successfully.",
                Data = new { newItem.Id }
            };
        }

        return new ApplicationResult
        {
            Status = ApplicationResultStatus.BadRequest,
            Message = "The item could not be created."
        };
    }
}
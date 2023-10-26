using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.Contracts.Services;
using Uplansi.Core.DTOs;
using Uplansi.Core.Entities;

namespace Uplansi.Services.Data.v1;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    /* public async Task<ApplicationResult> GetAll(PaginationOptions pagination)
    {
        var result = await _repository.GetAll(pagination);

        return new ApplicationResult
        {
            Status = 200,
            PageInfo = result.PageInfo,
            Data = result.Data
        };
    }

    public async Task<ApplicationResult?> GetById(Guid id)
    {
        var item = await _repository.GetById(id);

        if (item == null)
        {
            return new ApplicationResult
            {
                Status = 404,
                Message = $"The item with id '{id}' was not found or you don't have permission to access it."
            };
        }

        return new ApplicationResult
        {
            Status = 200,
            Data = item
        };
    } */

    public async Task<ApplicationResult> Add(TaskAddOrUpdate item)
    {
        var newItem = new ApplicationTask
        {
            Id = Guid.NewGuid(),
             Title = item.Title,
             Description = item.Description,
             Priority = item.Priority,
             DueDate = item.DueDate,
             Progress = item.Progress,
             Completed = item.Completed,
             Acceptance = item.Acceptance,
             GroupName = item.GroupName,
             AssignedToId = item.AssignedToId,
             CreatedById = Guid.NewGuid(),
             UpdatedById = Guid.NewGuid()
        };

        var isValid = await _repository.Add(newItem);

        if (isValid)
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
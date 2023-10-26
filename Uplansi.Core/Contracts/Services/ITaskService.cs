using Uplansi.Core.DTOs;

namespace Uplansi.Core.Contracts.Services;

public interface ITaskService
{
    // Task<ApplicationResult> GetAll(PaginationOptions pagination);
    // Task<ApplicationResult?> GetById(Guid id);
    Task<ApplicationResult> Add(TaskAddOrUpdate item);
}
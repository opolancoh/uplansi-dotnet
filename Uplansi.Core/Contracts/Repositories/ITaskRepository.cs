using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.Task;
using Uplansi.Core.Entities;

namespace Uplansi.Core.Contracts.Repositories;

public interface ITaskRepository
{
    Task<PagedListResult<TaskMyListResult>> GetAll(PaginationOptions pagination, Guid userId);
    Task<TaskMyDetailResult?> GetById(Guid id, Guid userId);
    Task<int> Add(ApplicationTask item);
}
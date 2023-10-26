using Uplansi.Core.DTOs;
using Uplansi.Core.Entities;

namespace Uplansi.Core.Contracts.Repositories;

public interface ITaskRepository
{
    // Task<PagedListResult<EmployeeListResult>> GetAll(PaginationOptions pagination);
    // Task<EmployeeDetailResult?> GetById(Guid id);
    Task<bool> Add(ApplicationTask item);
}
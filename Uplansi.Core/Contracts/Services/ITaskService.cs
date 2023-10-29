using System.Security.Claims;
using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.Task;

namespace Uplansi.Core.Contracts.Services;

public interface ITaskService
{
    Task<ApplicationResult> GetAll(PaginationOptions pagination, IEnumerable<Claim> claims);
    Task<ApplicationResult?> GetById(Guid id, IEnumerable<Claim> claims);
    Task<ApplicationResult> Add(TaskAddModel item, IEnumerable<Claim> claims);
}
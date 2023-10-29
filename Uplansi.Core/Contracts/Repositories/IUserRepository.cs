using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.User;
using Uplansi.Core.Entities.Account;

namespace Uplansi.Core.Contracts.Repositories;

public interface IUserRepository
{
    Task<PagedListResult<UserListResult>> GetAll(PaginationOptions pagination);
    Task<UserDetailResult?> GetById(Guid id);
}
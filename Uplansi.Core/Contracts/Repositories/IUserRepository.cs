using Uplansi.Core.DTOs;
using Uplansi.Core.Entities.Account;

namespace Uplansi.Core.Contracts.Repositories;

public interface IUserRepository : IRepositoryBase<Guid,ApplicationUser,  PagedListResult<UserListResult>, UserDetailResult>
{
    // Task AddRange(IEnumerable<ApplicationUser> items);
    // Task<IEnumerable<ApplicationUserListDto>> GetAllWithRoles();
    // Task<ApplicationUserDetailsDto?> GetByIdWithRoles(string id);
}
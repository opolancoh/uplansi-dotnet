using Uplansi.Core.Entities.Account;

namespace Uplansi.Core.Contracts.Repositories;

public interface IAccountRepository
{
    // Task<ApplicationUser?> GetUserForLogin(LoginDto loginDto);
    // Task<RefreshToken?> GetRefreshToken(string userId, string token);
    Task<int> AddRefreshToken(RefreshToken refreshToken);
    // Task<bool> RemoveRefreshToken(string userId, string token);
}
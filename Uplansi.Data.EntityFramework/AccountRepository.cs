using Microsoft.AspNetCore.Identity;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.Entities.Account;

namespace Uplansi.Data.EntityFramework;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AccountDbContext _context;

    public AccountRepository(UserManager<ApplicationUser> userManager,
        AccountDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    /* public async Task<ApplicationUser?> GetUserForLogin(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username!);
        if (user == null)
        {
            return null;
        }

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password!);
        return result ? user : null;
    }

    public async Task<RefreshToken?> GetRefreshToken(string userId, string token)
    {
        return await _context.RefreshTokens
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Token == token);
    } */

    public async Task<int> AddRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        return await _context.SaveChangesAsync();
    }

    /* public async Task<bool> RemoveRefreshToken(string userId, string token)
    {
        var refreshToken = new RefreshToken { UserId = userId, Token = token };
        _context.RefreshTokens.Remove(refreshToken);
        try
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    } */
}
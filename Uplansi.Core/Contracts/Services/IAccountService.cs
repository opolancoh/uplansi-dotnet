using Uplansi.Core.DTOs;

namespace Uplansi.Core.Contracts.Services;

public interface IAccountService
{
    Task<ApplicationResult> Register(AccountRegisterModel item);
    // Task<ApplicationResult> Login(LoginDto loginDto);
    // Task<ApplicationResult> Logout(TokenDto tokenDto);
    // Task<ApplicationResult> RefreshToken(TokenDto tokenDto);
}
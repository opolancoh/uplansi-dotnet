using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.Account;

namespace Uplansi.Core.Contracts.Services;

public interface IAccountService
{
    Task<ApplicationResult> Register(AccountRegisterModel item);
    Task<ApplicationResult> Login(AccountLoginModel item);
    // Task<ApplicationResult> Logout(TokenDto tokenDto);
    // Task<ApplicationResult> RefreshToken(TokenDto tokenDto);
}
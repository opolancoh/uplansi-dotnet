using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.Contracts.Services;
using Uplansi.Core.DTOs;
using Uplansi.Core.DTOs.Account;
using Uplansi.Core.DTOs.User;
using Uplansi.Core.Entities.Account;
using Uplansi.Core.Utils;

namespace Uplansi.Services.Data.v1;

public class AccountService : IAccountService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountService> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAccountRepository _accountRepository;
    private readonly IUserService _userService;

    private string AuthenticationFailedMessage =>
        $"{nameof(Login)}: Authentication failed. Wrong user name or password.";

    public AccountService(
        IConfiguration configuration,
        ILogger<AccountService> logger,
        UserManager<ApplicationUser> userManager,
        IAccountRepository accountRepository,
        IUserService userService
    )
    {
        _configuration = configuration;
        _logger = logger;
        _userManager = userManager;
        _accountRepository = accountRepository;
        _userService = userService;
    }

    public async Task<ApplicationResult> Register(AccountRegisterModel item)
    {
        var newItem = new UserAddModel
        {
            UserName = item.UserName!,
            Password = item.Password!,
            DisplayName = item.DisplayName ?? item.UserName!,
            Email = item.Email,
            Country = item.Country ?? "us",
            Language = item.Language ?? "en"
        };

        return await _userService.Add(newItem, null);
    }

    public async Task<ApplicationResult> Login(AccountLoginModel item)
    {
        var user = await _userManager.FindByNameAsync(item.UserName!);
        if (user == null)
        {
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.Unauthorized,
                Message = AuthenticationFailedMessage
            };
        }

        // AccessToken
        var issuer = _configuration.GetSection("JwtConfig:Issuer").Value;
        var audience = _configuration.GetSection("JwtConfig:Audience").Value;
        var secret = _configuration.GetSection("JwtConfig:Secret").Value;
        var expiration =
            DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration.GetSection("JwtConfig:AccessTokenExpirationInMinutes").Value));
        // Claims generation
        var userRoles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new("sub", user.Id.ToString()),
            new("name", user.UserName!),
            /* new("email", user.Email!), */
            new("isAdmin", userRoles.Contains("Administrator").ToString(), ClaimValueTypes.Boolean)
        };
        claims.AddRange(userRoles.Select(role => new Claim("roles", role)));

        var accessToken = AuthenticationHelper.GenerateAccessToken(issuer!, audience!, secret!, claims, expiration);

        // Refresh Token
        var refreshToken = AuthenticationHelper.GenerateRefreshToken();
        var addRefreshTokenResult = await _accountRepository.AddRefreshToken(new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            Expiration =
                DateTime.UtcNow.AddDays(
                    Convert.ToDouble(_configuration.GetSection("JwtConfig:RefreshTokenExpirationInDays").Value))
        });
        
        if (addRefreshTokenResult != 0)
            return new ApplicationResult
            {
                Status = ApplicationResultStatus.Ok,
                Data = new AccountLoginResult { AccessToken = accessToken, RefreshToken = refreshToken }
            };
        
        _logger.LogError("Unable to store the refresh token. UserId:{UserId}", user.Id);
        return new ApplicationResult
        {
            Status = ApplicationResultStatus.Unauthorized,
            Message = AuthenticationFailedMessage
        };

    }

    /* public async Task<ApplicationResult> Logout(TokenDto tokenDto)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");

        // AccessToken
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var secret = jwtSettings["Secret"];
        // Access token validation
        var accessToken = AuthHelper.ValidateJwtToken(tokenDto.AccessToken, issuer, audience, secret, false);
        if (accessToken == null)
        {
            return new ApplicationResult
            {
                Status = 400,
                Message = "Failed to logout."
            };
        }

        // Remove the refresh token
        var removeRefreshTokenResult =
            await _authRepository.RemoveRefreshToken(accessToken.Subject, tokenDto.RefreshToken);
        if (!removeRefreshTokenResult)
        {
            _logger.LogError("Unable to remove the refresh token");
            return new ApplicationResult
            {
                Status = 400,
                Message = "Failed to logout."
            };
        }

        return new ApplicationResult
        {
            Status = 204,
            Message = "Logged out successfully."
        };
    }

    public async Task<ApplicationResult> RefreshToken(TokenDto tokenDto)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");

        // Access token validation
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var secret = jwtSettings["Secret"];
        var accessToken = AuthHelper.ValidateJwtToken(tokenDto.AccessToken, issuer, audience, secret, false);
        if (accessToken == null)
        {
            return new ApplicationResult
            {
                Status = 401,
                Message = "Invalid access token."
            };
        }

        // Refresh token validation
        var refreshTokenFromDb = await _authRepository.GetRefreshToken(accessToken.Subject, tokenDto.RefreshToken);
        if (refreshTokenFromDb == null || refreshTokenFromDb.ExpiryDate < DateTime.UtcNow)
        {
            return new ApplicationResult
            {
                Status = 401,
                Message = "Refresh Token is not valid."
            };
        }

        // New access token generation
        var expirationInMinutes = Convert.ToDouble(jwtSettings["AccessTokenExpirationInMinutes"]);
        var expiration = DateTime.UtcNow.AddMinutes(expirationInMinutes);
        var newAccessToken = AuthHelper.UpdateTokenExpiration(accessToken, expiration, secret);

        return new ApplicationResult
        {
            Status = 200,
            D = new { AccessToken = newAccessToken }
        };
    } */
}
namespace Uplansi.Core.DTOs.Account;

public record AccountLoginResult 
{
    public required string AccessToken { get; init; }
    public required string  RefreshToken { get; init; }
}
namespace Uplansi.Core.Entities.Account;

public class RefreshToken
{
    public required Guid UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime Expiration { get; set; }
}
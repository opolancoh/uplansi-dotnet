namespace Uplansi.Core.DTOs;

public record UserAddOrUpdate
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? FullName { get; set; }
    public required string DisplayName { get; set; }

    public required string LanguageId { get; set; }
    public required string CountryId { get; set; }
    public Guid? AuthenticatedUserId { get; set; }
}
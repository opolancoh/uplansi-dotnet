using System.Text.Json.Serialization;

namespace Uplansi.Core.DTOs;

public record ApplicationResult
{
    public required ApplicationResultStatus Status { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<ApplicationResultError>? Errors { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PageInfo? PageInfo { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; init; }
}

public record ApplicationResultError
{
    public required string Code { get; set; }
    public required string Description { get; set; }
}

public enum ApplicationResultStatus
{
    Ok = 200,
    Created = 201,
    NoContent = 204,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    InternalServerError = 500
}
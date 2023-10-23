namespace Uplansi.Core.Entities.Common;

public class CountryTranslations
{
    // Composite Key parts
    public required string CountryId { get; init; }
    public required string LanguageId { get; init; }
    //
    public required string Name { get; init; }
}
namespace Uplansi.Core.Entities.Common;

public class LanguageTranslations
{
    // Composite Key parts
    public required string LanguageId { get; init; }
    public required string TranslationId { get; init; }
    //
    public required string Name { get; init; }
}
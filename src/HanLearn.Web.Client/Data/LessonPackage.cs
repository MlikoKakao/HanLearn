namespace HanLearn.Web.Client.Data;

public record LessonPackage
{
    public required Story Story { get; init; }
    public required List<LexicalSense> LexicalSenses { get; init; }
    public required List<HanziCharacter> HanziCharacters { get; init; }
    public required int Version { get; init; }
}
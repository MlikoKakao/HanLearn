namespace HanLearn.Web.Client.Data;

public record CharacterReading
{
    public required string Pronunciations { get; init; }
    public required List<string> Meanings { get; init; }
}

public record CharacterEntry
{
    public required string Id { get; init; }
    public required string WrittenForm { get; init; }
    public required List<CharacterReading> Readings { get; init; }

}
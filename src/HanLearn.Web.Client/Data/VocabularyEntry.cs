namespace HanLearn.Web.Client.Data;


public record VocabularyEntry
{
	public required string Id { get; init; }
	public required string WrittenForm { get; init; }
	public required string Meaning { get; init; }
	public required List<string> Pronunciations { get; init; }
	public string? Example { get; init; }
	public string? AudioFileName { get; init; }
	public required List<string> CharacterIds { get; init; }
}

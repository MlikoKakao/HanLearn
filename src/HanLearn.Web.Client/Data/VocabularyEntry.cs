namespace HanLearn.Web.Client.Data;

public enum VocabularyEntryKind
{
	Character,
	Word
}

public record VocabularyEntry
{
	public required int Id { get; init; }
	public required VocabularyEntryKind Kind { get; init; }
	public required string WrittenForm { get; init; }
	public required string Meaning { get; init; }
	public required List<string> Pronunciations { get; init; }
	public string? Example { get; init; }
	public string? AudioFileName { get; init; }
}

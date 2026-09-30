using HanLearn.Web.Client.Data;


namespace HanLearn.Web.Client.Services;

public class VocabularyCatalog
{
	private readonly List<VocabularyEntry> _entries = [];

	public IReadOnlyList<VocabularyEntry> Entries => _entries;

	public void AddEntry(
		string id,
		VocabularyEntryKind kind,
		string writtenForm,
		string meaning,
		List<string> pronunciations,
		string? example,
		string? audioFileName)
	{
		if (_entries.Any(entry => entry.Id == id))
		{
			throw new InvalidOperationException($"A vocabulary entry with the ID '{id}' already exists.");
		}

		VocabularyEntry entry = new()
		{
			Id = id,
			Kind = kind,
			WrittenForm = writtenForm,
			Meaning=meaning,
			Pronunciations = pronunciations,
			Example = example,
			AudioFileName = audioFileName
		};
		_entries.Add(entry);
	}
}

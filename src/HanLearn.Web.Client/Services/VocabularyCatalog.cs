using HanLearn.Web.Client.Data;


namespace HanLearn.Web.Client.Services;

public class VocabularyCatalog
{
	private readonly List<VocabularyEntry> _entries = [];
	private int _nextId = 0;

	public IReadOnlyList<VocabularyEntry> Entries => _entries;

	public void AddEntry(
		VocabularyEntryKind kind,
		string writtenForm,
		List<string> pronunciations,
		string? example,
		string? audioFileName)
	{
		_nextId += 1;
		VocabularyEntry entry = new()
		{
			Id = _nextId,
			Kind = kind,
			WrittenForm = writtenForm,
			Pronunciations = pronunciations,
			Example = example,
			AudioFileName = audioFileName
		};
		_entries.Add(entry);
	}
}

using HanLearn.Web.Client.Data;


namespace HanLearn.Web.Client.Services;

public class VocabularyCatalog
{
	private readonly List<VocabularyEntry> _entries = [];
	private readonly List<CharacterEntry> _characterEntries = [];

	public IReadOnlyList<VocabularyEntry> Entries => _entries;
	public IReadOnlyList<CharacterEntry> CharacterEntries => _characterEntries;

	public void AddCharacterEntry(
		string id,
		string writtenForm,
		List<CharacterReading> readings)
	{
		if (_characterEntries.Any(entry => entry.Id == id))
		{
			throw new InvalidOperationException($"A character entry with the ID '{id}' already exists.");
		}

		CharacterEntry entry = new()
		{
			Id = id,
			WrittenForm = writtenForm,
			Readings = readings
		};
		_characterEntries.Add(entry);
	}

	public void AddEntry(
		string id,
		string writtenForm,
		string meaning,
		List<string> pronunciations,
		string? example,
		string? audioFileName,
		List<string> characterIds)
	{
		if (_entries.Any(entry => entry.Id == id))
		{
			throw new InvalidOperationException($"A vocabulary entry with the ID '{id}' already exists.");
		}

		VocabularyEntry entry = new()
		{
			Id = id,
			WrittenForm = writtenForm,
			Meaning=meaning,
			Pronunciations = pronunciations,
			Example = example,
			AudioFileName = audioFileName,
			CharacterIds = characterIds
		};
		_entries.Add(entry);
	}
}

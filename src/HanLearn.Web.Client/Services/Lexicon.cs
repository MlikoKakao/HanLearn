using HanLearn.Web.Client.Data;


namespace HanLearn.Web.Client.Services;

public class Lexicon
{
	private readonly Dictionary<string, LexicalSense> _senses = [];
	private readonly Dictionary<string, HanziCharacter> _characters = [];

	public IReadOnlyDictionary<string, LexicalSense> Senses => _senses;
	public IReadOnlyDictionary<string, HanziCharacter> Characters => _characters;

	public LexicalSense? FindSense(string id) =>
		_senses.GetValueOrDefault(id);

	public HanziCharacter? FindCharacter(string id) =>
		_characters.GetValueOrDefault(id);

	public bool ContainsSense(string id) => _senses.ContainsKey(id);

	public bool ContainsCharacter(string id) => _characters.ContainsKey(id);

	public void AddCharacter(HanziCharacter character)
	{
		if (!_characters.TryAdd(character.Id, character))
		{
			throw new InvalidOperationException(
				$"A Hanzi character with the ID '{character.Id}' already exists.");
		}
	}

	public void AddCharacter(
		string id,
		string writtenForm,
		List<CharacterReading> readings)
	{
		HanziCharacter character = new()
		{
			Id = id,
			WrittenForm = writtenForm,
			Readings = readings
		};
		AddCharacter(character);
	}

	public void AddSense(LexicalSense sense)
	{
		if (!_senses.TryAdd(sense.Id, sense))
		{
			throw new InvalidOperationException(
				$"A lexical sense with the ID '{sense.Id}' already exists.");
		}
	}

	public void AddSense(
		string id,
		string writtenForm,
		string definition,
		List<string> pronunciations,
		string? example,
		string? audioFileName,
		List<string> characterIds)
	{
		LexicalSense sense = new()
		{
			Id = id,
			WrittenForm = writtenForm,
			Definition = definition,
			Pronunciations = pronunciations,
			Example = example,
			AudioFileName = audioFileName,
			CharacterIds = characterIds
		};
		AddSense(sense);
	}
}

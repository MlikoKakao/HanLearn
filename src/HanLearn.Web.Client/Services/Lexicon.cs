using HanLearn.Web.Client.Data;


namespace HanLearn.Web.Client.Services;

public class Lexicon
{
	private readonly List<LexicalSense> _senses = [];
	private readonly List<HanziCharacter> _characters = [];

	public IReadOnlyList<LexicalSense> Senses => _senses;
	public IReadOnlyList<HanziCharacter> Characters => _characters;

	public void AddCharacter(
		string id,
		string writtenForm,
		List<CharacterReading> readings)
	{
		if (_characters.Any(character => character.Id == id))
		{
			throw new InvalidOperationException($"A character entry with the ID '{id}' already exists.");
		}

		HanziCharacter character = new()
		{
			Id = id,
			WrittenForm = writtenForm,
			Readings = readings
		};
		_characters.Add(character);
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
		if (_senses.Any(sense => sense.Id == id))
		{
			throw new InvalidOperationException($"A lexical sense with the ID '{id}' already exists.");
		}

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
		_senses.Add(sense);
	}
}

using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

// Applies the same validation and cross-lesson consistency rules on the host and client.
public sealed class LessonPackageCatalog(
    LessonPackageValidator validator,
    Lexicon lexicon,
    StoryCatalog storyCatalog)
{
    private readonly LessonPackageValidator _validator = validator;
    private readonly Lexicon _lexicon = lexicon;
    private readonly StoryCatalog _storyCatalog = storyCatalog;
    private readonly Dictionary<string, LessonPackage> _packages = new(StringComparer.Ordinal);

    public IReadOnlyCollection<LessonPackage> Packages => _packages.Values;

    public LessonPackage? Find(string id) => _packages.GetValueOrDefault(id);

    public void Add(LessonPackage package, string path)
    {
        List<string> errors = [.. _validator.Validate(package)];
        if (errors.Count == 0)
        {
            AddCatalogConflictErrors(package, errors);
        }

        if (errors.Count > 0)
        {
            throw new LessonPackageValidationException(path, errors);
        }

        foreach (HanziCharacter character in package.HanziCharacters)
        {
            if (!_lexicon.ContainsCharacter(character.Id))
            {
                _lexicon.AddCharacter(character);
            }
        }

        foreach (LexicalSense sense in package.LexicalSenses)
        {
            if (!_lexicon.ContainsSense(sense.Id))
            {
                _lexicon.AddSense(sense);
            }
        }

        _storyCatalog.AddStory(package.Story, package.RecommendedLexicalSenseIds);
        _packages.Add(package.Story.Id, package);
    }

    private void AddCatalogConflictErrors(LessonPackage package, List<string> errors)
    {
        foreach (HanziCharacter character in package.HanziCharacters)
        {
            HanziCharacter? existingCharacter = _lexicon.FindCharacter(character.Id);
            if (existingCharacter is not null &&
                !HasSameContent(existingCharacter, character))
            {
                errors.Add(
                    $"Hanzi character '{character.Id}' conflicts with the existing lexicon record.");
            }
        }

        foreach (LexicalSense sense in package.LexicalSenses)
        {
            LexicalSense? existingSense = _lexicon.FindSense(sense.Id);
            if (existingSense is not null && !HasSameContent(existingSense, sense))
            {
                errors.Add(
                    $"Lexical sense '{sense.Id}' conflicts with the existing lexicon record.");
            }
        }

        if (_storyCatalog.ContainsStory(package.Story.Id))
        {
            errors.Add($"Story '{package.Story.Id}' already exists in the story catalog.");
        }
    }

    private static bool HasSameContent(HanziCharacter left, HanziCharacter right)
    {
        return left.Id == right.Id &&
            left.WrittenForm == right.WrittenForm &&
            left.Readings.Count == right.Readings.Count &&
            left.Readings.Zip(right.Readings).All(pair =>
                pair.First.Pronunciations == pair.Second.Pronunciations &&
                pair.First.Meanings.SequenceEqual(pair.Second.Meanings, StringComparer.Ordinal));
    }

    private static bool HasSameContent(LexicalSense left, LexicalSense right)
    {
        return left.Id == right.Id &&
            left.WrittenForm == right.WrittenForm &&
            left.Definition == right.Definition &&
            left.Example == right.Example &&
            left.AudioFileName == right.AudioFileName &&
            left.Pronunciations.SequenceEqual(right.Pronunciations, StringComparer.Ordinal) &&
            left.CharacterIds.SequenceEqual(right.CharacterIds, StringComparer.Ordinal);
    }
}

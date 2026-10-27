using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public sealed class LessonPackageLoader
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        RespectNullableAnnotations = true,
        RespectRequiredConstructorParameters = true
    };

    private readonly HttpClient _httpClient;
    private readonly LessonPackageValidator _validator;
    private readonly Lexicon _lexicon;
    private readonly StoryCatalog _storyCatalog;

    public LessonPackageLoader(
        HttpClient httpClient,
        LessonPackageValidator validator,
        Lexicon lexicon,
        StoryCatalog storyCatalog)
    {
        _httpClient = httpClient;
        _validator = validator;
        _lexicon = lexicon;
        _storyCatalog = storyCatalog;
    }

    public async Task<LessonPackage> LoadIntoCatalogsAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        LessonPackage package = await _httpClient.GetFromJsonAsync<LessonPackage>(
            path,
            SerializerOptions,
            cancellationToken)
            ?? throw new JsonException($"Lesson package '{path}' contained JSON null.");

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

        _storyCatalog.AddStory(package.Story);
        return package;
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

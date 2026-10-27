using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public sealed class LessonPackageValidator
{
    public const int SupportedVersion = 1;

    public IReadOnlyList<string> Validate(LessonPackage package)
    {
        List<string> errors = [];

        if (package.Version != SupportedVersion)
        {
            errors.Add(
                $"Unsupported version {package.Version}; expected {SupportedVersion}.");
        }

        if (package.Story is null)
        {
            errors.Add("Story is required.");
            return errors;
        }

        if (package.LexicalSenses is null)
        {
            errors.Add("LexicalSenses is required.");
            return errors;
        }

        if (package.HanziCharacters is null)
        {
            errors.Add("HanziCharacters is required.");
            return errors;
        }

        if (package.RecommendedLexicalSenseIds is null)
        {
            errors.Add("RecommendedLexicalSenseIds is required.");
            return errors;
        }

        ValidateStory(package.Story, errors);
        ValidateSenses(package.LexicalSenses, errors);
        ValidateCharacters(package.HanziCharacters, errors);
        ValidateReferences(package, errors);

        return errors;
    }

    private static void ValidateStory(Story story, List<string> errors)
    {
        RequireText(story.Id, "Story.Id", errors);
        RequireText(story.Title, "Story.Title", errors);

        if (story.Text is null)
        {
            errors.Add("Story.Text is required.");
        }

        if (story.Occurrences is null)
        {
            errors.Add("Story.Occurrences is required.");
            return;
        }

        AddDuplicateErrors(
            story.Occurrences.Select(occurrence => occurrence?.Id),
            "story occurrence",
            errors);

        foreach (StoryOccurrence? occurrence in story.Occurrences)
        {
            if (occurrence is null)
            {
                errors.Add("Story.Occurrences cannot contain null items.");
                continue;
            }

            RequireText(occurrence.Id, "StoryOccurrence.Id", errors);
            if (occurrence.Text is null)
            {
                errors.Add($"Story occurrence '{occurrence.Id}' is missing Text.");
            }
        }

        string reconstructedText = string.Concat(
            story.Occurrences.Select(occurrence => occurrence?.Text));

        if (story.Text is not null && reconstructedText != story.Text)
        {
            errors.Add("Joining Story.Occurrences does not reproduce Story.Text.");
        }
    }

    private static void ValidateSenses(
        IReadOnlyCollection<LexicalSense> senses,
        List<string> errors)
    {
        AddDuplicateErrors(senses.Select(sense => sense?.Id), "lexical sense", errors);

        foreach (LexicalSense? sense in senses)
        {
            if (sense is null)
            {
                errors.Add("LexicalSenses cannot contain null items.");
                continue;
            }

            RequireText(sense.Id, "LexicalSense.Id", errors);
            RequireText(sense.WrittenForm, $"LexicalSense '{sense.Id}'.WrittenForm", errors);
            RequireText(sense.Definition, $"LexicalSense '{sense.Id}'.Definition", errors);

            if (sense.Pronunciations is null || sense.Pronunciations.Count == 0 ||
                sense.Pronunciations.Any(string.IsNullOrWhiteSpace))
            {
                errors.Add($"Lexical sense '{sense.Id}' needs at least one pronunciation.");
            }

            if (sense.CharacterIds is null || sense.CharacterIds.Count == 0 ||
                sense.CharacterIds.Any(string.IsNullOrWhiteSpace))
            {
                errors.Add($"Lexical sense '{sense.Id}' needs at least one character ID.");
            }
        }
    }

    private static void ValidateCharacters(
        IReadOnlyCollection<HanziCharacter> characters,
        List<string> errors)
    {
        AddDuplicateErrors(
            characters.Select(character => character?.Id),
            "Hanzi character",
            errors);

        foreach (HanziCharacter? character in characters)
        {
            if (character is null)
            {
                errors.Add("HanziCharacters cannot contain null items.");
                continue;
            }

            RequireText(character.Id, "HanziCharacter.Id", errors);
            RequireText(
                character.WrittenForm,
                $"HanziCharacter '{character.Id}'.WrittenForm",
                errors);

            if (character.Readings is null || character.Readings.Count == 0)
            {
                errors.Add($"Hanzi character '{character.Id}' needs at least one reading.");
                continue;
            }

            foreach (CharacterReading? reading in character.Readings)
            {
                if (reading is null)
                {
                    errors.Add(
                        $"Hanzi character '{character.Id}' cannot contain a null reading.");
                    continue;
                }

                RequireText(
                    reading.Pronunciations,
                    $"HanziCharacter '{character.Id}' reading pronunciation",
                    errors);

                if (reading.Meanings is null || reading.Meanings.Count == 0 ||
                    reading.Meanings.Any(string.IsNullOrWhiteSpace))
                {
                    errors.Add(
                        $"A reading for Hanzi character '{character.Id}' needs at least one meaning.");
                }
            }
        }
    }

    private static void ValidateReferences(LessonPackage package, List<string> errors)
    {
        if (package.Story?.Occurrences is null ||
            package.LexicalSenses is null ||
            package.HanziCharacters is null ||
            package.RecommendedLexicalSenseIds is null ||
            package.Story.Occurrences.Any(occurrence => occurrence is null) ||
            package.LexicalSenses.Any(sense => sense is null) ||
            package.HanziCharacters.Any(character => character is null))
        {
            return;
        }

        HashSet<string> senseIds = package.LexicalSenses
            .Select(sense => sense.Id)
            .ToHashSet(StringComparer.Ordinal);
        HashSet<string> characterIds = package.HanziCharacters
            .Select(character => character.Id)
            .ToHashSet(StringComparer.Ordinal);

        foreach (StoryOccurrence occurrence in package.Story.Occurrences)
        {
            if (occurrence.LexicalSenseId is not null &&
                !senseIds.Contains(occurrence.LexicalSenseId))
            {
                errors.Add(
                    $"Story occurrence '{occurrence.Id}' references missing lexical sense " +
                    $"'{occurrence.LexicalSenseId}'.");
            }
        }

        foreach (LexicalSense sense in package.LexicalSenses)
        {
            if (sense.CharacterIds is null)
            {
                continue;
            }

            foreach (string characterId in sense.CharacterIds)
            {
                if (!characterIds.Contains(characterId))
                {
                    errors.Add(
                        $"Lexical sense '{sense.Id}' references missing Hanzi character " +
                        $"'{characterId}'.");
                }
            }
        }

        AddDuplicateErrors(
            package.RecommendedLexicalSenseIds,
            "recommended lexical sense",
            errors);

        foreach (string? senseId in package.RecommendedLexicalSenseIds)
        {
            if (string.IsNullOrWhiteSpace(senseId))
            {
                errors.Add("Recommended lexical-sense IDs cannot be blank.");
            }
            else if (!senseIds.Contains(senseId))
            {
                errors.Add($"Recommended lexical sense '{senseId}' does not exist.");
            }
        }
    }

    private static void AddDuplicateErrors(
        IEnumerable<string?> ids,
        string itemName,
        List<string> errors)
    {
        IEnumerable<string> duplicateIds = ids
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id!)
            .GroupBy(id => id, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key);

        foreach (string duplicateId in duplicateIds)
        {
            errors.Add($"Duplicate {itemName} ID '{duplicateId}'.");
        }
    }

    private static void RequireText(string? value, string fieldName, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add($"{fieldName} is required.");
        }
    }
}

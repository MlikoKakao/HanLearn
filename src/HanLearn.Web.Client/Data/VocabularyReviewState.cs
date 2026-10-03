using FSRS.Core.Models;

namespace HanLearn.Web.Client.Data;

public enum ReviewItemKind
{
    Vocabulary,
    Character
}

public record VocabularyReviewState
{
    public required string EntryId { get; init; }
    public required ReviewItemKind Kind { get; init; }
    public required Card Card { get; set; }
}

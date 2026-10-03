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
    public DateTimeOffset AddedAt { get; init; }
    public ReviewRating? LastRating { get; set; }
    public int? ReviewCount { get; set; }
}

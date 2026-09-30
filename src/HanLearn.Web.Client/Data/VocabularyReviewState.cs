namespace HanLearn.Web.Client.Data;

public record VocabularyReviewState
{
    public required string VocabularyEntryId { get; init; }
    public DateTimeOffset AddedAt { get; init; }
    public ReviewRating? LastRating { get; set; }
    public int? ReviewCount { get; set; }
}

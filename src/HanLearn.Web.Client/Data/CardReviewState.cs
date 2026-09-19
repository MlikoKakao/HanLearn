using HanLearn.Web.Client.Components;


namespace HanLearn.Web.Client.Data;

public record CardReviewState
{
    public required int StudyCardId { get; init; }
    public DateTimeOffset AddedAt { get; init; }
    public Rating? LastRating { get; set; }
    public int ReviewCount { get; set; }
}
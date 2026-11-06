namespace HanLearn.Web.Client.Data;

public record Story
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Summary { get; init; }
    public required string Difficulty { get; init; }
    public required int EstimatedReadingMinutes { get; init; }
    public required string ImagePath { get; init; }
    public required string Text { get; set; }
    public required StoryOccurrence[] Occurrences { get; set; }
}

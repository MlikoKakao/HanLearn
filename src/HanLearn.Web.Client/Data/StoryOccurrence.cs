namespace HanLearn.Web.Client.Data;

public record StoryOccurrence
{
    public required string Id { get; init; }
    public required string Text { get; init; }
    public string? LexicalSenseId { get; init; }
}

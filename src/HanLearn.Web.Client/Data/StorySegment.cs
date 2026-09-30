namespace HanLearn.Web.Client.Data;

public record StorySegment
{
    public required string Id { get; init; }
    public required string Text { get; init; }
    public string? VocabularyEntryId { get; init; }
}

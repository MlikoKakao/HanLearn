namespace HanLearn.Web.Client.Data;

public record StorySegment
{
    public required int Id { get; init; }
    public required string Text { get; init; }
    public int? VocabularyEntryId { get; init; }
}
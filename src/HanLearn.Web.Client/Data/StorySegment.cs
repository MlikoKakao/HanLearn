namespace HanLearn.Web.Client.Data;

public class StorySegment
{
    public required int Id { get; init; }
    public required string Text { get; init; }
    public int? VocabularyEntryId { get; init; }
}
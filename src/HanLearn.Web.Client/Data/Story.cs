namespace HanLearn.Web.Client.Data;

public record Story
{
    public required int Id { get; init; }
    public required string Text { get; set; }
    public required StorySegment[] Segments { get; set; }
}

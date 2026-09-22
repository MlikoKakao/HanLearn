namespace HanLearn.Web.Client.Data;

public record Story
{
    public required int Id { get; init; }
    public required string Content { get; set; }
    public required string[] SeparatedContent { get; set; }
}
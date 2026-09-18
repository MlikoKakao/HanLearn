namespace HanLearn.Web.Client.Data;

public record CardFront
{
	public required string Character { get; init; }
}

public record CardBack
{
	public required List<string> Pronunciation { get; init; }
	public string? Example { get; init; }
	public string? Voice { get; init; }
}


public record StudyCard
{
	public required int Id { get; init; }
	public required CardFront CardFront { get; init; }
	public required CardBack CardBack { get; init; }
}

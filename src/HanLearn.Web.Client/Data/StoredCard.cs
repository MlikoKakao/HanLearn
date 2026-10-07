using FSRS.Core.Enums;

namespace HanLearn.Web.Client.Data;

public record StoredCard
{
    public required Guid CardId { get; init; }
    public required State State { get; set; }
    public int? Step { get; set; }
    public double? Stability { get; set; }
    public double? Difficulty { get; set; }
    public DateTime Due { get; set; }
    public DateTime? LastReview { get; set; }
}
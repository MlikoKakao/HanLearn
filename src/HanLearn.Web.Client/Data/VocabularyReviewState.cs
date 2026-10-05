using FSRS.Core.Models;

namespace HanLearn.Web.Client.Data;

public record VocabularyReviewState
{
    public required string VocabularyEntryId { get; init; }
    public required Card Card { get; set; }
}

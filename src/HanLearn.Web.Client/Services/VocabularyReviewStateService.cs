using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public class VocabularyReviewStateService
{
    private readonly List<VocabularyReviewState> _reviewStates = [];

    public IReadOnlyList<VocabularyReviewState> ReviewStates => _reviewStates;

    public void AddToReview(int vocabularyEntryId)
    {
        VocabularyReviewState reviewState = new()
        {
            VocabularyEntryId = vocabularyEntryId,
            AddedAt = DateTime.UtcNow
        };
        _reviewStates.Add(reviewState);
    }
}

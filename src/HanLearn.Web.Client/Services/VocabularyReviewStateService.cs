using FSRS.Core.Models;
using FSRS.Core.Interfaces;

using HanLearn.Web.Client.Data;
using FSRS.Core.Enums;

namespace HanLearn.Web.Client.Services;

public class VocabularyReviewStateService
{
    private readonly List<VocabularyReviewState> _reviewStates = [];

    public IReadOnlyList<VocabularyReviewState> ReviewStates => _reviewStates;
    private readonly IScheduler _scheduler;

    public VocabularyReviewStateService(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }
    public void AddToReview(string vocabularyEntryId)
    {
        VocabularyReviewState reviewState = new()
        {
            VocabularyEntryId = vocabularyEntryId,
            Card = new Card()
        };

        _reviewStates.Add(reviewState);
    }

    public void Review(string vocabularyEntryId, Rating rating, DateTime reviewDateTime)
    {
        VocabularyReviewState? reviewedState = _reviewStates.FirstOrDefault(entry => entry.VocabularyEntryId == vocabularyEntryId);
        if (reviewedState is null)
        {
            return;
        }
        else 
        {
            var updatedCard = _scheduler.ReviewCard(reviewedState.Card, rating, reviewDateTime);
            reviewedState.Card = updatedCard.UpdatedCard;
        }
    }
}

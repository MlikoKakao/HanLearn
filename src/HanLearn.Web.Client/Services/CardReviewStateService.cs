using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public class CardReviewStateService
{
    private readonly List<CardReviewState> _reviewCards = [];

    public IReadOnlyList<CardReviewState> ReviewStates => _reviewCards;

    public void AddCardReviewState(int id)
    {
        CardReviewState newCardReviewState = new CardReviewState()
        {
            StudyCardId=id,
            AddedAt=DateTime.UtcNow
        };
        _reviewCards.Add(newCardReviewState);
    }
}
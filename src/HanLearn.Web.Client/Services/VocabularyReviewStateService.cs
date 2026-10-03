using FSRS.Core.Models;
using FSRS.Core.Services;

using HanLearn.Web.Client.Data;


namespace HanLearn.Web.Client.Services;

public class VocabularyReviewStateService
{

    private readonly List<VocabularyReviewState> _reviewStates = [];

    public IReadOnlyList<VocabularyReviewState> ReviewStates => _reviewStates;

    public void AddToReview(string vocabularyEntryId)
    {
        AddToReview(vocabularyEntryId, ReviewItemKind.Vocabulary);
    }

    public void AddCharacterToReview(string characterEntryId)
    {
        AddToReview(characterEntryId, ReviewItemKind.Character);
    }

    private void AddToReview(string entryId, ReviewItemKind kind)
    {
        VocabularyReviewState reviewState = new()
        {
            EntryId = entryId,
            Kind = kind,
            Card = new(){}
        };
        _reviewStates.Add(reviewState);
    }
}

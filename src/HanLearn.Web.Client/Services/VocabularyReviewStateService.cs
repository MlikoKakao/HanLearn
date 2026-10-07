using FSRS.Core.Models;
using FSRS.Core.Interfaces;
using Microsoft.JSInterop;

using HanLearn.Web.Client.Data;
using FSRS.Core.Enums;
using System.Text.Json;

namespace HanLearn.Web.Client.Services;


public class VocabularyReviewStateService
{
    private readonly List<VocabularyReviewState> _reviewStates = [];

    public IReadOnlyList<VocabularyReviewState> ReviewStates => _reviewStates;
    private readonly IScheduler _scheduler;
    private readonly IJSRuntime _jsRuntime;
    private bool _isLoaded;

    public VocabularyReviewStateService(IScheduler scheduler, IJSRuntime js)
    {
        _scheduler = scheduler;
        _jsRuntime = js;
    }
    public async Task AddToReviewAsync(string vocabularyEntryId)
    {
        await LoadAsync();
        if (_reviewStates.Any(r => r.VocabularyEntryId == vocabularyEntryId))
        {
            return;
        }
        VocabularyReviewState reviewState = new()
        {
            VocabularyEntryId = vocabularyEntryId,
            Card = new Card()
        };

        _reviewStates.Add(reviewState);
        await SaveAsync();
    }

    public async Task ReviewAsync(string vocabularyEntryId, Rating rating, DateTime reviewDateTime)
    {
        await LoadAsync();
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
        await SaveAsync();
    }

    public List<VocabularyReviewState> GetDueCards()
    {
        var now = DateTime.UtcNow;
        List<VocabularyReviewState> dueCards;
        dueCards = _reviewStates.Where(entry => entry.Card.Due <= now).OrderBy(entry => entry.Card.Due).ToList();
        return dueCards;
    }

    public string Serialize()
    {
        List<StoredVocabularyReviewState> storedStates = _reviewStates
            .Select(reviewState => new StoredVocabularyReviewState
            {
                VocabularyEntryId = reviewState.VocabularyEntryId,
                Card = new StoredCard
                {
                    CardId = reviewState.Card.CardId,
                    State = reviewState.Card.State,
                    Step = reviewState.Card.Step,
                    Stability = reviewState.Card.Stability,
                    Difficulty = reviewState.Card.Difficulty,
                    Due = reviewState.Card.Due,
                    LastReview = reviewState.Card.LastReview
                }
            })
            .ToList();

        return JsonSerializer.Serialize(storedStates);
    }
    private List<VocabularyReviewState> Deserialize(string reviewState)
    {
        var storedStates = JsonSerializer.Deserialize<List<StoredVocabularyReviewState>>(reviewState);
        List<VocabularyReviewState> translatedList;
        if (storedStates is null)
        {
            return [];
        }
        translatedList = storedStates
        .Select(storedStates => new VocabularyReviewState
        {
            VocabularyEntryId = storedStates.VocabularyEntryId,
                Card = new Card
                {
                    CardId = storedStates.Card.CardId,
                    State = storedStates.Card.State,
                    Step = storedStates.Card.Step,
                    Stability = storedStates.Card.Stability,
                    Difficulty = storedStates.Card.Difficulty,
                    Due = storedStates.Card.Due,
                    LastReview = storedStates.Card.LastReview
                }
            })
            .ToList();
        return translatedList;
    }
    
    public async Task LoadAsync()
    {
        if (_isLoaded) { return; }
        string? storedJson = await _jsRuntime.InvokeAsync<string?>(
            "localStorage.getItem",
            "hanlearn.vocabulary-review-states"
        );
        if (string.IsNullOrWhiteSpace(storedJson))
        {
            _isLoaded = true;
            return;
        }
        List<VocabularyReviewState> restoredStates = Deserialize(storedJson);
        _reviewStates.Clear();
        _reviewStates.AddRange(restoredStates);
        _isLoaded = true;

    }

    public async Task SaveAsync()
    {
        string json = Serialize();
        await _jsRuntime.InvokeVoidAsync(
            "localStorage.setItem",
            "hanlearn.vocabulary-review-states",
            json
        );
    }


}

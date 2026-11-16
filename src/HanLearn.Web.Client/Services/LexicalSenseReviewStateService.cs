using FSRS.Core.Models;
using FSRS.Core.Interfaces;
using Microsoft.JSInterop;

using HanLearn.Web.Client.Data;
using FSRS.Core.Enums;
using System.Text.Json;

namespace HanLearn.Web.Client.Services;


public class LexicalSenseReviewStateService
{
    private readonly List<LexicalSenseReviewState> _reviewStates = [];

    public IReadOnlyList<LexicalSenseReviewState> ReviewStates => _reviewStates;
    private readonly IScheduler _scheduler;
    private readonly IJSRuntime _jsRuntime;
    private bool _isLoaded;

    public LexicalSenseReviewStateService(IScheduler scheduler, IJSRuntime js)
    {
        _scheduler = scheduler;
        _jsRuntime = js;
    }
    public Task AddToReviewAsync(string lexicalSenseId) =>
        AddManyToReviewAsync([lexicalSenseId]);

    public async Task AddManyToReviewAsync(IEnumerable<string> lexicalSenseIds)
    {
        await LoadAsync();
        var existingIds = _reviewStates.Select(state => state.LexicalSenseId).ToHashSet();
        var additions = lexicalSenseIds.Distinct()
            .Where(id => !existingIds.Contains(id))
            .Select(id => new LexicalSenseReviewState { LexicalSenseId = id, Card = new Card() })
            .ToList();
        if (additions.Count == 0)
        {
            return;
        }

        _reviewStates.AddRange(additions);
        try
        {
            await SaveAsync();
        }
        catch
        {
            foreach (var addition in additions) _reviewStates.Remove(addition);
            throw;
        }
    }

    public async Task ReviewAsync(string lexicalSenseId, Rating rating, DateTime reviewDateTime)
    {
        await LoadAsync();
        LexicalSenseReviewState? reviewedState = _reviewStates.FirstOrDefault(
            state => state.LexicalSenseId == lexicalSenseId);
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

    public List<LexicalSenseReviewState> GetDueCards()
    {
        var now = DateTime.UtcNow;
        List<LexicalSenseReviewState> dueCards;
        dueCards = _reviewStates.Where(entry => entry.Card.Due <= now).OrderBy(entry => entry.Card.Due).ToList();
        return dueCards;
    }

    public string Serialize()
    {
        List<StoredLexicalSenseReviewState> storedStates = _reviewStates
            .Select(reviewState => new StoredLexicalSenseReviewState
            {
                LexicalSenseId = reviewState.LexicalSenseId,
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
    private List<LexicalSenseReviewState> Deserialize(string reviewState)
    {
        var storedStates = JsonSerializer.Deserialize<List<StoredLexicalSenseReviewState>>(reviewState);
        List<LexicalSenseReviewState> translatedList;
        if (storedStates is null)
        {
            throw new JsonException("Expected an array of review states.");
        }
        if (storedStates.Any(state =>
            state is null ||
            string.IsNullOrWhiteSpace(state.LexicalSenseId) ||
            state.Card is null))
        {
            throw new JsonException("A review state is missing required data.");
        }
        translatedList = storedStates
        .Select(storedStates => new LexicalSenseReviewState
        {
            LexicalSenseId = storedStates.LexicalSenseId,
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
        List<LexicalSenseReviewState> restoredStates = Deserialize(storedJson);
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

    public async Task ImportAsync(Stream stream)
    {
        using StreamReader reader = new(stream);
        string json = await reader.ReadToEndAsync();
        List<LexicalSenseReviewState> importedStates = Deserialize(json);

        _reviewStates.Clear();
        _reviewStates.AddRange(importedStates);
        _isLoaded = true;

        await SaveAsync();
    }


}

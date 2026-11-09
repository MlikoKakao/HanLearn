using System.Text;
using System.Text.Json;
using FSRS.Core.Enums;
using FSRS.Core.Extensions;
using FSRS.Core.Interfaces;
using HanLearn.Web.Client.Data;
using HanLearn.Web.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace HanLearn.Tests;

public class ReviewStateTests
{
    [Fact]
    public async Task AddingSameSenseTwiceStoresOneCard()
    {
        using var services = CreateServices();
        var storage = new MemoryStorage();
        var service = CreateService(services, storage);

        await service.AddToReviewAsync("tea");
        await service.AddToReviewAsync("tea");

        Assert.Equal("tea", Assert.Single(service.ReviewStates).LexicalSenseId);
        Assert.Single(JsonSerializer.Deserialize<List<StoredLexicalSenseReviewState>>(storage.Json!)!);
        Assert.Equal(1, storage.Writes);
    }

    [Fact]
    public async Task SaveReloadAndBackupImportPreserveEveryStoredCardField()
    {
        using var services = CreateServices();
        var storage = new MemoryStorage();
        var service = CreateService(services, storage);
        var expected = new StoredLexicalSenseReviewState
        {
            LexicalSenseId = "tea",
            Card = new()
            {
                CardId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                State = State.Review, Step = 2, Stability = 4.5, Difficulty = 6.2,
                Due = new DateTime(2026, 11, 12, 0, 0, 0, DateTimeKind.Utc),
                LastReview = new DateTime(2026, 11, 9, 0, 0, 0, DateTimeKind.Utc)
            }
        };
        using var backup = JsonStream(JsonSerializer.Serialize(new[] { expected }));
        await service.ImportAsync(backup);

        var reloaded = CreateService(services, storage);
        await reloaded.LoadAsync();
        var restored = Assert.Single(JsonSerializer.Deserialize<List<StoredLexicalSenseReviewState>>(reloaded.Serialize())!);
        Assert.Equal(expected, restored);
        Assert.Contains("\"VocabularyEntryId\"", reloaded.Serialize());

        var importedStorage = new MemoryStorage();
        var imported = CreateService(services, importedStorage);
        using var exportedBackup = JsonStream(reloaded.Serialize());
        await imported.ImportAsync(exportedBackup);
        Assert.Equal(reloaded.Serialize(), imported.Serialize());
        Assert.Equal(imported.Serialize(), importedStorage.Json);
    }

    [Theory]
    [InlineData("not json")]
    [InlineData("null")]
    [InlineData("[null]")]
    [InlineData("[{}]")]
    [InlineData("[{\"VocabularyEntryId\":\"tea\",\"Card\":null}]")]
    [InlineData("[{\"VocabularyEntryId\":\" \",\"Card\":{\"CardId\":\"11111111-2222-3333-4444-555555555555\",\"State\":0}}]")]
    public async Task MalformedImportPreservesMemoryAndStorage(string json)
    {
        using var services = CreateServices();
        var storage = new MemoryStorage();
        var service = CreateService(services, storage);
        await service.AddToReviewAsync("tea");
        var before = service.Serialize();
        using var stream = JsonStream(json);

        await Assert.ThrowsAsync<JsonException>(() => service.ImportAsync(stream));

        Assert.Equal(before, service.Serialize());
        Assert.Equal(before, storage.Json);
        Assert.Equal(1, storage.Writes);
    }

    [Fact]
    public async Task RatingUpdatesCardAndPersistsReviewTime()
    {
        using var services = CreateServices();
        var storage = new MemoryStorage();
        var service = CreateService(services, storage);
        await service.AddToReviewAsync("tea");
        var reviewTime = new DateTime(2026, 11, 9, 12, 0, 0, DateTimeKind.Utc);

        await service.ReviewAsync("tea", Rating.Good, reviewTime);

        var card = Assert.Single(service.ReviewStates).Card;
        Assert.Equal(reviewTime, card.LastReview);
        Assert.True(card.Due > reviewTime);
        Assert.Equal(service.Serialize(), storage.Json);
        var reloaded = CreateService(services, storage);
        await reloaded.LoadAsync();
        Assert.Equal(reviewTime, Assert.Single(reloaded.ReviewStates).Card.LastReview);
    }

    private static ServiceProvider CreateServices()
    {
        var services = new ServiceCollection();
        services.AddFSRS(options => options.EnableFuzzing = false);
        return services.BuildServiceProvider();
    }

    private static LexicalSenseReviewStateService CreateService(ServiceProvider services, MemoryStorage storage) =>
        new(services.GetRequiredService<IScheduler>(), storage);

    private static MemoryStream JsonStream(string json) => new(Encoding.UTF8.GetBytes(json));

    // Fail unexpected interop calls instead of silently allowing browser dependencies.
    private sealed class MemoryStorage : IJSRuntime
    {
        public string? Json { get; private set; }
        public int Writes { get; private set; }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
            InvokeAsync<TValue>(identifier, CancellationToken.None, args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
        {
            Assert.Equal("hanlearn.vocabulary-review-states", args![0]);
            switch (identifier)
            {
                case "localStorage.getItem": return ValueTask.FromResult((TValue)(object?)Json!);
                case "localStorage.setItem":
                    Json = (string)args[1]!;
                    Writes++;
                    return ValueTask.FromResult(default(TValue)!);
                default: throw new InvalidOperationException($"Unexpected JS call: {identifier}");
            }
        }
    }
}

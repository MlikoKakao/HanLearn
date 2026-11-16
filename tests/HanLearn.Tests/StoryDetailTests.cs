using Bunit;
using FSRS.Core.Extensions;
using HanLearn.Web.Client.Pages;
using HanLearn.Web.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace HanLearn.Tests;

public class StoryDetailTests : BunitContext
{
    public StoryDetailTests()
    {
        var package = LessonFixture.Create();
        var stories = new StoryCatalog();
        stories.AddStory(package.Story, package.RecommendedLexicalSenseIds);
        var lexicon = new Lexicon();
        foreach (var sense in package.LexicalSenses) lexicon.AddSense(sense);
        foreach (var character in package.HanziCharacters) lexicon.AddCharacter(character);
        Services.AddSingleton(stories);
        Services.AddSingleton(lexicon);
        Services.AddFSRS(options => options.EnableFuzzing = false);
        Services.AddScoped<LexicalSenseReviewStateService>();
        JSInterop.Setup<string?>("localStorage.getItem", "hanlearn.vocabulary-review-states").SetResult(null);
        JSInterop.SetupVoid("localStorage.setItem", _ => true).SetVoidResult();
    }

    [Fact]
    public void SelectedWordStaysOpenAfterPointerLeavesAndEscapeClosesIt()
    {
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "test-story"));
        page.Find(".story-text").Click();
        page.Find(".story-word-anchor").MouseLeave();

        Assert.Single(page.FindAll(".word-popover"));
        Assert.Equal("true", page.Find(".story-text").GetAttribute("aria-expanded"));

        page.Find(".story-body").KeyDown("Escape");
        Assert.Empty(page.FindAll(".word-popover"));
        Assert.Equal("false", page.Find(".story-text").GetAttribute("aria-expanded"));
    }

    [Fact]
    public void KeyboardFocusOpensWordAndClickingSelectedWordClosesIt()
    {
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "test-story"));
        page.Find(".story-text").Focus();
        Assert.Single(page.FindAll(".word-popover"));

        page.Find(".story-text").Click();
        page.Find(".story-text").Click();
        Assert.Empty(page.FindAll(".word-popover"));
    }

    [Fact]
    public void NavigatingToUnavailableStoryClearsLookupAndShowsRecoveryLink()
    {
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "test-story"));
        page.Find(".story-text").Click();

        page.Render(p => p.Add(c => c.StoryId, "missing"));

        Assert.Empty(page.FindAll(".word-popover"));
        Assert.Contains("Dialogue not found", page.Find("h1").TextContent);
        Assert.Equal("dialogues", page.Find(".missing-story a").GetAttribute("href"));
    }

    [Fact]
    public void RecommendationsCanBeDeselectedAndOnlySelectedWordsAreSaved()
    {
        var stories = Services.GetRequiredService<StoryCatalog>();
        var lexicon = Services.GetRequiredService<Lexicon>();
        lexicon.AddSense(LessonFixture.Create().LexicalSenses[0] with
        {
            Id = "milk-tea", WrittenForm = "奶茶", Definition = "milk tea"
        });
        stories.AddStory(LessonFixture.Create().Story with { Id = "two-words" }, ["tea", "milk-tea"]);
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "two-words"));

        Assert.Equal(2, page.FindAll("input[type=checkbox]:checked").Count);
        Assert.Contains("ㄔㄚˊ", page.Find(".review-selection").TextContent);
        page.Find("input[value=milk-tea]").Change(false);
        page.Find(".add-selected").Click();

        var service = Services.GetRequiredService<LexicalSenseReviewStateService>();
        Assert.Equal("tea", Assert.Single(service.ReviewStates).LexicalSenseId);
        Assert.Contains("Added 1 word", page.Find(".review-selection-message").TextContent);
        Assert.Equal("Already in review", page.Find(".review-word-status").TextContent.Trim());
        Assert.True(page.Find(".add-selected").HasAttribute("disabled"));
        Assert.Equal("review", page.Find(".review-selection-actions a").GetAttribute("href"));
        Assert.Single(JSInterop.Invocations["localStorage.setItem"]);
    }

    [Fact]
    public async Task ExistingCardsAreDisabledAndKeepTheirSchedule()
    {
        var service = Services.GetRequiredService<LexicalSenseReviewStateService>();
        await service.AddToReviewAsync("tea");
        await service.ReviewAsync("tea", FSRS.Core.Enums.Rating.Good, DateTime.UtcNow);
        var before = service.Serialize();

        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "test-story"));

        Assert.True(page.Find("input[value=tea]").HasAttribute("disabled"));
        Assert.False(page.Find("input[value=tea]").HasAttribute("checked"));
        Assert.Contains("Already in review", page.Find(".review-selection").TextContent);
        Assert.True(page.Find(".add-selected").HasAttribute("disabled"));
        Assert.Equal(before, service.Serialize());
    }

    [Fact]
    public void AddingWordFromGlossUpdatesChecklist()
    {
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "test-story"));
        page.Find(".story-text").Click();
        page.Find(".vocabulary-details .btn-primary").Click();

        Assert.True(page.Find("input[value=tea]").HasAttribute("disabled"));
        Assert.Contains("Already in review", page.Find(".review-selection").TextContent);
        Assert.True(page.Find(".add-selected").HasAttribute("disabled"));
    }

    [Fact]
    public void FailedSaveKeepsSelectionAndAllowsRetry()
    {
        var save = JSInterop.SetupVoid("localStorage.setItem", _ => true);
        save.SetException(new JSException("Storage unavailable"));
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "test-story"));

        page.Find(".add-selected").Click();

        Assert.Contains("couldn’t be saved", page.Find("[role=alert]").TextContent);
        Assert.Empty(Services.GetRequiredService<LexicalSenseReviewStateService>().ReviewStates);
        Assert.True(page.Find("input[value=tea]").HasAttribute("checked"));
        Assert.False(page.Find(".add-selected").HasAttribute("disabled"));

        save.SetVoidResult();
        page.Find(".add-selected").Click();
        Assert.Single(Services.GetRequiredService<LexicalSenseReviewStateService>().ReviewStates);
        Assert.Empty(page.FindAll("[role=alert]"));
    }

    [Fact]
    public void FailedLoadDisablesAddingAndOffersRetry()
    {
        var load = JSInterop.Setup<string?>("localStorage.getItem", "hanlearn.vocabulary-review-states");
        load.SetException(new JSException("Storage unavailable"));
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "test-story"));

        Assert.Contains("couldn’t be loaded", page.Find("[role=alert]").TextContent);
        Assert.True(page.Find(".add-selected").HasAttribute("disabled"));

        load.SetResult(null);
        page.FindAll(".review-selection button").Single(button => button.TextContent == "Try again").Click();
        Assert.False(page.Find(".add-selected").HasAttribute("disabled"));
        Assert.Empty(page.FindAll("[role=alert]"));
    }

    [Fact]
    public void LessonWithoutRecommendationsHasNoChecklist()
    {
        Services.GetRequiredService<StoryCatalog>().AddStory(
            LessonFixture.Create().Story with { Id = "no-recommendations" });
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "no-recommendations"));

        Assert.Empty(page.FindAll(".review-selection"));
        Assert.Single(page.FindAll(".story-text"));
    }

    [Fact]
    public void ChangingDialoguesResetsVocabularySelection()
    {
        var stories = Services.GetRequiredService<StoryCatalog>();
        stories.AddStory(LessonFixture.Create().Story with { Id = "second-story" }, ["tea"]);
        var page = Render<StoryDetail>(p => p.Add(c => c.StoryId, "test-story"));
        page.Find("input[value=tea]").Change(false);
        Assert.True(page.Find(".add-selected").HasAttribute("disabled"));

        page.Render(p => p.Add(c => c.StoryId, "second-story"));

        Assert.True(page.Find("input[value=tea]").HasAttribute("checked"));
        Assert.False(page.Find(".add-selected").HasAttribute("disabled"));
        Assert.Empty(Services.GetRequiredService<LexicalSenseReviewStateService>().ReviewStates);
    }
}

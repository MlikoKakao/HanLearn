using Bunit;
using FSRS.Core.Extensions;
using HanLearn.Web.Client.Pages;
using HanLearn.Web.Client.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HanLearn.Tests;

public class StoryDetailTests : BunitContext
{
    public StoryDetailTests()
    {
        var package = LessonFixture.Create();
        var stories = new StoryCatalog();
        stories.AddStory(package.Story);
        var lexicon = new Lexicon();
        foreach (var sense in package.LexicalSenses) lexicon.AddSense(sense);
        foreach (var character in package.HanziCharacters) lexicon.AddCharacter(character);
        Services.AddSingleton(stories);
        Services.AddSingleton(lexicon);
        Services.AddFSRS(options => options.EnableFuzzing = false);
        Services.AddScoped<LexicalSenseReviewStateService>();
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
}

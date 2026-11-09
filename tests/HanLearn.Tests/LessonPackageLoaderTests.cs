using System.Net;
using System.Net.Http.Json;
using HanLearn.Web.Client.Data;
using HanLearn.Web.Client.Services;

namespace HanLearn.Tests;

public class LessonPackageLoaderTests
{
    [Fact]
    public async Task ValidPackagePopulatesCatalogs()
    {
        var package = LessonFixture.Create();
        var lexicon = new Lexicon();
        var stories = new StoryCatalog();
        using var http = CreateHttp(package);
        var loader = new LessonPackageLoader(http, new(), lexicon, stories);

        await loader.LoadIntoCatalogsAsync("lesson.json");

        Assert.Equal("tea", Assert.Single(lexicon.Senses).Value.Id);
        Assert.Equal("茶", Assert.Single(lexicon.Characters).Value.Id);
        Assert.Equal(package.Story.Text, Assert.Single(stories.Stories).Text);
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("sense-conflict")]
    [InlineData("character-conflict")]
    [InlineData("story-conflict")]
    public async Task RejectedPackageLeavesExistingCatalogsUntouched(string fault)
    {
        var package = LessonFixture.Create();
        var lexicon = new Lexicon();
        var stories = new StoryCatalog();
        var existingSense = package.LexicalSenses[0] with { Id = "existing" };
        lexicon.AddSense(existingSense);
        switch (fault)
        {
            case "invalid": package.Story.Text = "invalid"; break;
            case "sense-conflict": lexicon.AddSense(package.LexicalSenses[0] with { Definition = "other" }); break;
            case "character-conflict": lexicon.AddCharacter(package.HanziCharacters[0] with { WrittenForm = "other" }); break;
            case "story-conflict": stories.AddStory(package.Story); break;
        }
        var sensesBefore = lexicon.Senses.ToArray();
        var charactersBefore = lexicon.Characters.ToArray();
        var storiesBefore = stories.Stories.ToArray();
        using var http = CreateHttp(package);
        var loader = new LessonPackageLoader(http, new(), lexicon, stories);

        await Assert.ThrowsAsync<LessonPackageValidationException>(() => loader.LoadIntoCatalogsAsync("lesson.json"));

        Assert.Equal(sensesBefore, lexicon.Senses.ToArray());
        Assert.Equal(charactersBefore, lexicon.Characters.ToArray());
        Assert.Equal(storiesBefore, stories.Stories.ToArray());
        Assert.Same(existingSense, lexicon.FindSense("existing"));
    }

    [Fact]
    public async Task IdenticalLexiconRecordsCanBeSharedByLessons()
    {
        var package = LessonFixture.Create();
        var lexicon = new Lexicon();
        lexicon.AddSense(package.LexicalSenses[0]);
        lexicon.AddCharacter(package.HanziCharacters[0]);
        var stories = new StoryCatalog();
        using var http = CreateHttp(package);
        var loader = new LessonPackageLoader(http, new(), lexicon, stories);

        await loader.LoadIntoCatalogsAsync("lesson.json");

        Assert.Single(lexicon.Senses);
        Assert.Single(lexicon.Characters);
        Assert.Single(stories.Stories);
    }

    private static HttpClient CreateHttp(LessonPackage package) =>
        new(new PackageHandler(package)) { BaseAddress = new Uri("https://test.invalid/") };

    private sealed class PackageHandler(LessonPackage package) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(package) });
    }
}

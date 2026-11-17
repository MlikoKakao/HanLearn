using System.Text.Json;
using HanLearn.Web.Client.Data;
using HanLearn.Web.Client.Services;
using HanLearn.Web.Services;

namespace HanLearn.Tests;

public sealed class PublishedLessonCatalogTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), $"hanlearn-lessons-{Guid.NewGuid():N}");

    public PublishedLessonCatalogTests() => Directory.CreateDirectory(_directory);

    [Fact]
    public async Task LoadsAllPackagesAndRetainsTheirRecommendations()
    {
        var first = LessonFixture.Create();
        var second = first with { Story = first.Story with { Id = "second-story", Title = "More tea" } };
        await WriteAsync("first.json", first);
        await WriteAsync("second.json", second);
        var catalog = new PublishedLessonCatalog();

        await catalog.LoadAsync(_directory);

        Assert.Equal(2, catalog.Summaries.Count);
        Assert.Equal(first.Story.Title, catalog.Summaries[0].Title);
        Assert.Equal(first.RecommendedLexicalSenseIds, catalog.Find(first.Story.Id)!.RecommendedLexicalSenseIds);
        Assert.Equal(second.Story.Text, catalog.Find(second.Story.Id)!.Story.Text);
        Assert.Null(catalog.Find("missing"));
        Assert.Null(catalog.Find("../first.json"));
    }

    [Fact]
    public async Task EmptyDirectoryProducesEmptyCatalog()
    {
        var catalog = new PublishedLessonCatalog();
        await catalog.LoadAsync(_directory);

        Assert.Empty(catalog.Summaries);
        Assert.Null(catalog.Find("missing"));
    }

    [Fact]
    public async Task MissingDirectoryIsNotSilentlyTreatedAsEmpty()
    {
        await Assert.ThrowsAsync<DirectoryNotFoundException>(() =>
            new PublishedLessonCatalog().LoadAsync(Path.Combine(_directory, "missing")));
    }

    [Theory]
    [InlineData("{broken")]
    [InlineData("null")]
    [InlineData("{}")]
    public async Task MalformedJsonIdentifiesTheFile(string json)
    {
        var path = Path.Combine(_directory, "broken.json");
        await File.WriteAllTextAsync(path, json);

        var error = await Assert.ThrowsAsync<InvalidDataException>(() =>
            new PublishedLessonCatalog().LoadAsync(_directory));

        Assert.Contains(path, error.Message);
    }

    [Theory]
    [InlineData("invalid-content", "does not reproduce Story.Text")]
    [InlineData("duplicate-story", "already exists")]
    [InlineData("conflicting-sense", "conflicts with the existing lexicon")]
    public async Task InvalidReleaseIsRejectedWithoutPublishingPartialCatalog(string fault, string expected)
    {
        var first = LessonFixture.Create();
        await WriteAsync("first.json", first);
        var second = LessonFixture.Create();
        if (fault != "duplicate-story") second = second with { Story = second.Story with { Id = "second" } };
        if (fault == "invalid-content") second.Story.Text = "wrong";
        if (fault == "conflicting-sense") second.LexicalSenses[0] = second.LexicalSenses[0] with { Definition = "different" };
        await WriteAsync("second.json", second);
        var catalog = new PublishedLessonCatalog();

        var error = await Assert.ThrowsAsync<LessonPackageValidationException>(() => catalog.LoadAsync(_directory));

        Assert.Contains("second.json", error.Message);
        Assert.Contains(expected, error.Message);
        Assert.Empty(catalog.Summaries);
        Assert.Null(catalog.Find(first.Story.Id));
    }

    private Task WriteAsync(string name, LessonPackage package) =>
        File.WriteAllTextAsync(Path.Combine(_directory, name), JsonSerializer.Serialize(package, LessonPackageJson.Options));

    public void Dispose() => Directory.Delete(_directory, recursive: true);
}

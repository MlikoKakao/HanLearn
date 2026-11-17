using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using HanLearn.Web.Client.Data;
using HanLearn.Web.Client.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HanLearn.Tests;

public class LessonApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public LessonApiTests(WebApplicationFactory<Program> factory) => _factory = factory;

    private HttpClient CreateClient() => _factory.CreateClient(new()
    {
        BaseAddress = new Uri("https://localhost"), AllowAutoRedirect = false
    });

    [Fact]
    public async Task ListReturnsRealSummariesWithoutLessonBodies()
    {
        using var client = CreateClient();
        using var response = await client.GetAsync("/api/lessons");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType!.MediaType);
        var lessons = await response.Content.ReadFromJsonAsync<List<LessonSummary>>();
        var lesson = Assert.Single(lessons!);
        Assert.Equal("breakfast-shop-morning", lesson.Id);
        Assert.False(string.IsNullOrWhiteSpace(lesson.Summary));
        Assert.True(lesson.EstimatedReadingMinutes > 0);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.False(json.RootElement[0].TryGetProperty("text", out _));
        Assert.False(json.RootElement[0].TryGetProperty("occurrences", out _));
        Assert.False(json.RootElement[0].TryGetProperty("lexicalSenses", out _));
    }

    [Fact]
    public async Task DetailReturnsCompleteValidatedLesson()
    {
        using var client = CreateClient();
        using var response = await client.GetAsync("/api/lessons/breakfast-shop-morning");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var package = await response.Content.ReadFromJsonAsync<LessonPackage>(LessonPackageJson.Options);
        Assert.NotNull(package);
        Assert.Empty(new LessonPackageValidator().Validate(package));
        Assert.Equal("breakfast-shop-morning", package.Story.Id);
        Assert.NotEmpty(package.LexicalSenses);
        Assert.NotEmpty(package.HanziCharacters);
        Assert.NotEmpty(package.RecommendedLexicalSenseIds);
    }

    [Fact]
    public async Task MissingLessonReturnsJson404()
    {
        using var client = CreateClient();
        using var response = await client.GetAsync("/api/lessons/missing");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType!.MediaType);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(404, json.RootElement.GetProperty("status").GetInt32());
    }

    [Fact]
    public async Task ClientDiscoversLessonsAndPopulatesReaderCatalogsThroughApi()
    {
        using var client = CreateClient();
        var stories = new StoryCatalog();
        var lexicon = new Lexicon();
        var loader = new LessonPackageLoader(client, new LessonPackageCatalog(new(), lexicon, stories));

        await loader.LoadPublishedLessonsAsync();

        var story = Assert.Single(stories.Stories);
        Assert.Equal("breakfast-shop-morning", story.Id);
        Assert.NotEmpty(stories.GetRecommendedLexicalSenseIds(story.Id));
        foreach (var occurrence in story.Occurrences.Where(item => item.LexicalSenseId is not null))
        {
            Assert.NotNull(lexicon.FindSense(occurrence.LexicalSenseId!));
        }
        Assert.NotEmpty(lexicon.Characters);
    }

    [Fact]
    public async Task EmptyContentDirectoryReturnsEmptyJsonList()
    {
        var root = Path.Combine(Path.GetTempPath(), $"hanlearn-host-{Guid.NewGuid():N}");
        Directory.CreateDirectory(Path.Combine(root, "Content", "Lessons"));
        try
        {
            using var factory = _factory.WithWebHostBuilder(builder => builder.UseContentRoot(root));
            using var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

            var lessons = await client.GetFromJsonAsync<List<LessonSummary>>("/api/lessons");

            Assert.Empty(lessons!);
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    [Fact]
    public async Task ClientDiscoversMultipleLessonsWithoutDependingOnFilenames()
    {
        var root = Path.Combine(Path.GetTempPath(), $"hanlearn-host-{Guid.NewGuid():N}");
        var directory = Path.Combine(root, "Content", "Lessons");
        Directory.CreateDirectory(directory);
        var first = LessonFixture.Create();
        var second = first with { Story = first.Story with { Id = "another-lesson" } };
        await File.WriteAllTextAsync(Path.Combine(directory, "first-file.json"),
            JsonSerializer.Serialize(first, LessonPackageJson.Options));
        await File.WriteAllTextAsync(Path.Combine(directory, "second-file.json"),
            JsonSerializer.Serialize(second, LessonPackageJson.Options));
        try
        {
            using var factory = _factory.WithWebHostBuilder(builder => builder.UseContentRoot(root));
            using var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });
            var stories = new StoryCatalog();
            var loader = new LessonPackageLoader(client, new LessonPackageCatalog(new(), new(), stories));

            await loader.LoadPublishedLessonsAsync();

            Assert.Equal(new[] { "test-story", "another-lesson" }, stories.Stories.Select(story => story.Id));
            Assert.All(stories.Stories, story =>
                Assert.Equal(new[] { "tea" }, stories.GetRecommendedLexicalSenseIds(story.Id)));
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    [Fact]
    public void InvalidPublishedContentPreventsHostStartup()
    {
        var root = Path.Combine(Path.GetTempPath(), $"hanlearn-host-{Guid.NewGuid():N}");
        var directory = Path.Combine(root, "Content", "Lessons");
        Directory.CreateDirectory(directory);
        File.WriteAllText(Path.Combine(directory, "broken.json"), "null");
        try
        {
            using var factory = _factory.WithWebHostBuilder(builder => builder.UseContentRoot(root));

            var error = Assert.ThrowsAny<Exception>(() => factory.CreateClient());

            Assert.Contains("broken.json", error.ToString());
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }
}

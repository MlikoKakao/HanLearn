using System.Text.Json;
using HanLearn.Web.Client.Data;
using HanLearn.Web.Client.Services;

namespace HanLearn.Web.Services;

public sealed class PublishedLessonCatalog
{
    private LessonPackageCatalog _catalog = new(new(), new(), new());
    private IReadOnlyList<LessonSummary> _summaries = [];

    public IReadOnlyList<LessonSummary> Summaries => _summaries;

    public LessonPackage? Find(string id) => _catalog.Find(id);

    // Called before the host starts listening. A bad release fails as a whole.
    public async Task LoadAsync(string directory, CancellationToken cancellationToken = default)
    {
        var catalog = new LessonPackageCatalog(new(), new(), new());
        foreach (var path in Directory.GetFiles(directory, "*.json").Order(StringComparer.Ordinal))
        {
            try
            {
                await using var stream = File.OpenRead(path);
                var package = await JsonSerializer.DeserializeAsync<LessonPackage>(
                    stream, LessonPackageJson.Options, cancellationToken)
                    ?? throw new JsonException("Expected a lesson package, received JSON null.");
                catalog.Add(package, path);
            }
            catch (JsonException error)
            {
                throw new InvalidDataException($"Lesson package '{path}' could not be read: {error.Message}", error);
            }
        }

        _summaries = catalog.Packages.Select(package => new LessonSummary(
            package.Story.Id,
            package.Story.Title,
            package.Story.Summary,
            package.Story.Difficulty,
            package.Story.EstimatedReadingMinutes,
            package.Story.ImagePath)).ToArray();
        _catalog = catalog;
    }
}

using System.Net.Http.Json;
using System.Text.Json;
using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public sealed class LessonPackageLoader(HttpClient httpClient, LessonPackageCatalog catalog)
{
    public async Task LoadPublishedLessonsAsync(CancellationToken cancellationToken = default)
    {
        var lessons = await httpClient.GetFromJsonAsync<List<LessonSummary>>(
            "api/lessons", cancellationToken)
            ?? throw new JsonException("The lesson list contained JSON null.");

        foreach (var lesson in lessons)
        {
            await LoadIntoCatalogsAsync(
                $"api/lessons/{Uri.EscapeDataString(lesson.Id)}", cancellationToken);
        }
    }

    public async Task<LessonPackage> LoadIntoCatalogsAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        var package = await httpClient.GetFromJsonAsync<LessonPackage>(
            path, LessonPackageJson.Options, cancellationToken)
            ?? throw new JsonException($"Lesson package '{path}' contained JSON null.");

        catalog.Add(package, path);
        return package;
    }
}

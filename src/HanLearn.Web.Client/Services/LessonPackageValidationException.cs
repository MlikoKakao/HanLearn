namespace HanLearn.Web.Client.Services;

public sealed class LessonPackageValidationException : Exception
{
    public LessonPackageValidationException(string path, IReadOnlyList<string> errors)
        : base($"Lesson package '{path}' is invalid:{Environment.NewLine}- {string.Join($"{Environment.NewLine}- ", errors)}")
    {
        Path = path;
        Errors = errors;
    }

    public string Path { get; }
    public IReadOnlyList<string> Errors { get; }
}

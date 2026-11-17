namespace HanLearn.Web.Client.Data;

public record LessonSummary(
    string Id,
    string Title,
    string Summary,
    string Difficulty,
    int EstimatedReadingMinutes,
    string ImagePath);

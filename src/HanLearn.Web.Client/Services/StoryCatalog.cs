using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public class StoryCatalog
{
    private readonly List<Story> _stories = [];
    private readonly Dictionary<string, IReadOnlyList<string>> _recommendations = [];

    public IReadOnlyList<Story> Stories => _stories;

    public bool ContainsStory(string id) =>
        _stories.Any(story => story.Id == id);

    public IReadOnlyList<string> GetRecommendedLexicalSenseIds(string storyId) =>
        _recommendations.GetValueOrDefault(storyId) ?? [];

    public void AddStory(Story story, IReadOnlyList<string>? recommendedLexicalSenseIds = null)
    {
        if (ContainsStory(story.Id))
        {
            throw new InvalidOperationException($"A story with the ID '{story.Id}' already exists.");
        }

        _stories.Add(story);
        _recommendations.Add(story.Id, recommendedLexicalSenseIds?.ToArray() ?? []);
    }

    public void AddStory(
        string id,
        string title,
        string summary,
        string difficulty,
        int estimatedReadingMinutes,
        string imagePath,
        string text,
        StoryOccurrence[] occurrences)
    {
        Story newStory = new ()
        {
            Id = id,
            Title = title,
            Summary = summary,
            Difficulty = difficulty,
            EstimatedReadingMinutes = estimatedReadingMinutes,
            ImagePath = imagePath,
            Text = text,
            Occurrences = occurrences
        };
        AddStory(newStory);
    }
}

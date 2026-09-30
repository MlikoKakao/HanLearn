using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public class StoryCatalog
{
    private readonly List<Story> _stories = [];

    public IReadOnlyList<Story> Stories => _stories;

    public void AddStory(string id, string title, string text, StorySegment[] segments)
    {
        if (_stories.Any(story => story.Id == id))
        {
            throw new InvalidOperationException($"A story with the ID '{id}' already exists.");
        }

        Story newStory = new ()
        {
            Id = id,
            Title = title,
            Text = text,
            Segments = segments
        };
        _stories.Add(newStory);
    }
}

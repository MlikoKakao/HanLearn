using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public class StoryCatalog
{
    private readonly List<Story> _stories = [];
    private int _nextId = 0;

    public IReadOnlyList<Story> Stories => _stories;

    public void AddStory(string title, string text, StorySegment[] segments)
    {
        _nextId +=1;
        Story newStory = new ()
        {
            Id = _nextId,
            Title = title,
            Text = text,
            Segments = segments
        };
        _stories.Add(newStory);
    }
}

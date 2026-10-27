using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public class StoryCatalog
{
    private readonly List<Story> _stories = [];

    public IReadOnlyList<Story> Stories => _stories;

    public bool ContainsStory(string id) =>
        _stories.Any(story => story.Id == id);

    public void AddStory(Story story)
    {
        if (ContainsStory(story.Id))
        {
            throw new InvalidOperationException($"A story with the ID '{story.Id}' already exists.");
        }

        _stories.Add(story);
    }

    public void AddStory(string id, string title, string text, StoryOccurrence[] occurrences)
    {
        Story newStory = new ()
        {
            Id = id,
            Title = title,
            Text = text,
            Occurrences = occurrences
        };
        AddStory(newStory);
    }
}

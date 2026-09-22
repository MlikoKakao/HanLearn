using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public class StoryCollectionService
{
    private readonly List<Story> _stories = [];
    private int _nextId = 0;

    public IReadOnlyList<Story> Stories => _stories;

    public void AddStory(string content, string[] separatedContent)
    {
        _nextId +=1;
        Story newStory = new ()
        {
            Id = _nextId,
            Content=content,
            SeparatedContent=separatedContent
        };
        _stories.Add(newStory);
    }
}
// Another short-term file for testing stories

namespace HanLearn.Web.Client.Services;
using HanLearn.Web.Client.Data;


public class AutomatedStoryAddition
{
    StoryCollectionService _dialogues;

    public AutomatedStoryAddition(StoryCollectionService stories)
    {
        _dialogues = stories;
    }
    public void AddExampleStories()
    {
        _dialogues.AddStory(
                "你好好味",
                ["你好", "好味"]
        );
        _dialogues.AddStory(
                "好味你好",
                ["好味", "你好"]
        );
    }
}
// Short-term file for seeding example stories.

namespace HanLearn.Web.Client.Services;
using HanLearn.Web.Client.Data;


public class ExampleStorySeeder
{
    private readonly StoryCatalog _stories;

    public ExampleStorySeeder(StoryCatalog stories)
    {
        _stories = stories;
    }

    public void Seed()
    {
        _stories.AddStory(
                "你好好味",
                ["你好", "好味"]
        );
        _stories.AddStory(
                "好味你好",
                ["好味", "你好"]
        );
    }
}

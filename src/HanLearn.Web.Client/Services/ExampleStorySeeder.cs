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
                "First story",
                "你好好味",
                [new StorySegment{
                    Id=1,
                    Text="你好",
                    VocabularyEntryId=1
                },
                new StorySegment{
                    Id=2,
                    Text="好味",
                    VocabularyEntryId=2
                }
                ]
        );
        _stories.AddStory(
                "Second story",
                "好味你好",
                [new StorySegment{
                    Id=1,
                    Text="好味",
                    VocabularyEntryId=2
                },
                new StorySegment{
                    Id=2,
                    Text="你好",
                    VocabularyEntryId=1
                }
                ]
        );
    }
}

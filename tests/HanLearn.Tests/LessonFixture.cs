using HanLearn.Web.Client.Data;
using HanLearn.Web.Client.Services;

namespace HanLearn.Tests;

internal static class LessonFixture
{
    public static LessonPackage Create() => new()
    {
        Version = LessonPackageValidator.SupportedVersion,
        Story = new Story
        {
            Id = "test-story", Title = "Tea", Summary = "Ordering tea",
            Difficulty = "Beginner", EstimatedReadingMinutes = 1,
            ImagePath = "test.webp", Text = "茶。",
            Occurrences =
            [
                new() { Id = "word", Text = "茶", LexicalSenseId = "tea" },
                new() { Id = "punctuation", Text = "。" }
            ]
        },
        LexicalSenses =
        [
            new() { Id = "tea", WrittenForm = "茶", Definition = "tea",
                Pronunciations = ["ㄔㄚˊ"], CharacterIds = ["茶"] }
        ],
        HanziCharacters =
        [
            new() { Id = "茶", WrittenForm = "茶", Readings =
                [new() { Pronunciations = "ㄔㄚˊ", Meanings = ["tea"] }] }
        ],
        RecommendedLexicalSenseIds = ["tea"]
    };
}

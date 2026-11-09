using HanLearn.Web.Client.Services;

namespace HanLearn.Tests;

public class LessonValidationTests
{
    [Fact]
    public void ValidPackagePasses() =>
        Assert.Empty(new LessonPackageValidator().Validate(LessonFixture.Create()));

    [Theory]
    [InlineData("version", "Unsupported version")]
    [InlineData("duplicate-sense", "Duplicate lexical sense ID")]
    [InlineData("duplicate-character", "Duplicate Hanzi character ID")]
    [InlineData("duplicate-occurrence", "Duplicate story occurrence ID")]
    [InlineData("missing-sense", "references missing lexical sense")]
    [InlineData("missing-character", "references missing Hanzi character")]
    [InlineData("recommendation", "does not exist")]
    [InlineData("text", "does not reproduce Story.Text")]
    public void InvalidContentReportsRelevantError(string fault, string expected)
    {
        var package = LessonFixture.Create();
        switch (fault)
        {
            case "version": package = package with { Version = -1 }; break;
            case "duplicate-sense": package.LexicalSenses.Add(package.LexicalSenses[0]); break;
            case "duplicate-character": package.HanziCharacters.Add(package.HanziCharacters[0]); break;
            case "duplicate-occurrence":
                package.Story.Occurrences[1] = package.Story.Occurrences[1] with { Id = "word" }; break;
            case "missing-sense":
                package.Story.Occurrences[0] = package.Story.Occurrences[0] with { LexicalSenseId = "missing" }; break;
            case "missing-character": package.LexicalSenses[0].CharacterIds.Add("missing"); break;
            case "recommendation": package.RecommendedLexicalSenseIds.Add("missing"); break;
            case "text": package.Story.Text = "different"; break;
            default: throw new ArgumentOutOfRangeException(nameof(fault));
        }

        Assert.Contains(new LessonPackageValidator().Validate(package), error => error.Contains(expected));
    }
}

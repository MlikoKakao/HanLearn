namespace HanLearn.Tests;

using HanLearn.Web.Client.Data;
using HanLearn.Web.Client.Services;
using Xunit;

public class LessonPackageValidatorTests
{
    [Fact]
    public void Validate_WhenStoryIsMissing_ReportsError()
    {
        // Arrange
        var package = new LessonPackage
        {
            Version = LessonPackageValidator.SupportedVersion,
            Story = null!,
            LexicalSenses = [],
            HanziCharacters = [],
            RecommendedLexicalSenseIds = []
        };

        var validator = new LessonPackageValidator();

        // Act
        var errors = validator.Validate(package);

        // Assert
        Assert.Contains("Story is required.", errors);
    }
}

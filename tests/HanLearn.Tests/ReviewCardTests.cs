using Bunit;
using FSRS.Core.Enums;
using HanLearn.Web.Client.Components;

namespace HanLearn.Tests;

public class ReviewCardTests : BunitContext
{
    [Fact]
    public void AnswerAndRatingsAreHiddenUntilRevealed()
    {
        var card = Render<ReviewCard>(p => p.Add(c => c.Sense, LessonFixture.Create().LexicalSenses[0]));
        Assert.Empty(card.FindAll(".answer"));
        Assert.Empty(card.FindAll(".rating-button"));

        card.Find(".reveal-button").Click();

        Assert.Contains("ㄔㄚˊ", card.Find(".answer").TextContent);
        Assert.Contains("tea", card.Find(".answer").TextContent);
        Assert.Equal(4, card.FindAll(".rating-button").Count);
    }

    [Theory]
    [InlineData("again", Rating.Again)]
    [InlineData("hard", Rating.Hard)]
    [InlineData("good", Rating.Good)]
    [InlineData("easy", Rating.Easy)]
    public void RatingButtonsEmitExpectedRating(string buttonClass, Rating expected)
    {
        var ratings = new List<Rating>();
        var card = Render<ReviewCard>(p => p
            .Add(c => c.Sense, LessonFixture.Create().LexicalSenses[0])
            .Add(c => c.OnRatingSelected, (Rating rating) => ratings.Add(rating)));

        card.Find(".reveal-button").Click();
        card.Find($".rating-button.{buttonClass}").Click();

        Assert.Equal(expected, Assert.Single(ratings));
    }

    [Theory]
    [InlineData("1", Rating.Again)]
    [InlineData("2", Rating.Hard)]
    [InlineData("3", Rating.Good)]
    [InlineData("4", Rating.Easy)]
    public void KeyboardRatingsRequireRevealingAnswer(string key, Rating expected)
    {
        var ratings = new List<Rating>();
        var card = Render<ReviewCard>(p => p
            .Add(c => c.Sense, LessonFixture.Create().LexicalSenses[0])
            .Add(c => c.OnRatingSelected, (Rating rating) => ratings.Add(rating)));
        card.Find("article").KeyDown(key);
        Assert.Empty(ratings);

        card.Find(".reveal-button").Click();
        card.Find("article").KeyDown(key);

        Assert.Equal(expected, Assert.Single(ratings));
    }

    [Fact]
    public void ChangingSenseHidesAnswerAndAllowsNextRating()
    {
        var sense = LessonFixture.Create().LexicalSenses[0];
        var ratings = new List<Rating>();
        var card = Render<ReviewCard>(p => p.Add(c => c.Sense, sense)
            .Add(c => c.OnRatingSelected, (Rating rating) => ratings.Add(rating)));
        card.Find(".reveal-button").Click();
        card.Find(".rating-button.good").Click();

        card.Render(p => p.Add(c => c.Sense, sense with { Id = "another-sense" }));
        Assert.Empty(card.FindAll(".answer"));
        card.Find(".reveal-button").Click();
        card.Find(".rating-button.easy").Click();

        Assert.Equal(new[] { Rating.Good, Rating.Easy }, ratings);
    }
}

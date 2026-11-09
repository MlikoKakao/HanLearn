using Bunit;
using HanLearn.Web.Client.Pages;
using HanLearn.Web.Client.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HanLearn.Tests;

public class ZhuyinTestTests : BunitContext
{
    public ZhuyinTestTests() => Services.AddSingleton<ZhuyinSymbolCatalog>();

    [Fact]
    public void AnswerMustBeRevealedBeforeAdvancingAndResetsForNextSymbol()
    {
        var page = Render<ZhuyinTest>();
        Assert.Empty(page.FindAll(".answer"));
        Assert.Empty(page.FindAll(".next-button"));

        page.Find(".reveal-button").Click();

        Assert.Equal("b", page.Find(".answer strong").TextContent);
        Assert.Single(page.FindAll(".next-button"));

        page.Find(".reveal-button").Click();
        Assert.Empty(page.FindAll(".answer"));
        Assert.Empty(page.FindAll(".next-button"));

        page.Find(".reveal-button").Click();
        page.Find(".next-button").Click();

        Assert.Equal("ㄆ", page.Find(".written-form").TextContent);
        Assert.Empty(page.FindAll(".answer"));
        Assert.Empty(page.FindAll(".next-button"));
    }

    [Fact]
    public void FullPracticeCycleRevealsEveryPronunciationAndReturnsToHiddenFirstCard()
    {
        var page = Render<ZhuyinTest>();
        var symbols = Services.GetRequiredService<ZhuyinSymbolCatalog>().Symbols;
        Assert.Equal(37, symbols.Count);

        foreach (var symbol in symbols)
        {
            Assert.Equal(symbol.WrittenForm, page.Find(".written-form").TextContent);
            Assert.Empty(page.FindAll(".answer"));
            page.Find(".reveal-button").Click();
            Assert.Equal(symbol.Pronunciation, page.Find(".answer strong").TextContent);
            page.Find(".next-button").Click();
        }

        Assert.Equal("ㄅ", page.Find(".written-form").TextContent);
        Assert.Empty(page.FindAll(".answer"));
        Assert.Empty(page.FindAll(".next-button"));
    }
}

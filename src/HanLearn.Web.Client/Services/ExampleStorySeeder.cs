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
        const string firstParagraph =
            "早上七點，小安走進早餐店。老闆娘笑著說：「早安！今天想吃什麼？」小安說：「我要一個蛋餅和一杯冰豆漿，謝謝。」";
        const string secondParagraph =
            "老闆娘問：「豆漿要加糖嗎？」小安回答：「不要糖。請問一共多少錢？」老闆娘說：「六十五元。」小安付了錢，拿著早餐去上班。";

        _stories.AddStory(
            "breakfast-shop-morning",
            "早餐店的早晨",
            $"{firstParagraph}\n\n{secondParagraph}",
            [
                SenseOccurrence("p1-morning", "早上", "morning"),
                SenseOccurrence("p1-seven-oclock", "七點", "seven-oclock"),
                PlainTextOccurrence("p1-comma-1", "，"),
                SenseOccurrence("p1-xiao-an-1", "小安", "name-xiao-an"),
                SenseOccurrence("p1-enter", "走進", "enter-on-foot"),
                SenseOccurrence("p1-breakfast-shop", "早餐店", "breakfast-shop"),
                PlainTextOccurrence("p1-period-1", "。"),
                SenseOccurrence("p1-shop-owner", "老闆娘", "female-shop-owner"),
                SenseOccurrence("p1-smiling", "笑著", "smiling"),
                SenseOccurrence("p1-say-1", "說", "say"),
                PlainTextOccurrence("p1-opening-quote-1", "：「"),
                SenseOccurrence("p1-good-morning", "早安", "good-morning"),
                PlainTextOccurrence("p1-exclamation", "！"),
                SenseOccurrence("p1-today", "今天", "today"),
                SenseOccurrence("p1-want-to", "想", "want-to"),
                SenseOccurrence("p1-eat", "吃", "eat"),
                SenseOccurrence("p1-what", "什麼", "what"),
                PlainTextOccurrence("p1-question-close-1", "？」"),
                SenseOccurrence("p1-xiao-an-2", "小安", "name-xiao-an"),
                SenseOccurrence("p1-say-2", "說", "say"),
                PlainTextOccurrence("p1-opening-quote-2", "：「"),
                SenseOccurrence("p1-i", "我", "i-me"),
                SenseOccurrence("p1-want-order", "要", "want-order"),
                SenseOccurrence("p1-one-item", "一個", "one-item"),
                SenseOccurrence("p1-egg-crepe", "蛋餅", "egg-crepe"),
                SenseOccurrence("p1-and", "和", "and-han4"),
                SenseOccurrence("p1-one-cup", "一杯", "one-cup"),
                SenseOccurrence("p1-cold-soy-milk", "冰豆漿", "cold-soy-milk"),
                PlainTextOccurrence("p1-comma-2", "，"),
                SenseOccurrence("p1-thank-you", "謝謝", "thank-you"),
                PlainTextOccurrence("p1-period-close", "。」"),
                PlainTextOccurrence("paragraph-break", "\n\n"),
                SenseOccurrence("p2-shop-owner-1", "老闆娘", "female-shop-owner"),
                SenseOccurrence("p2-ask", "問", "ask"),
                PlainTextOccurrence("p2-opening-quote-1", "：「"),
                SenseOccurrence("p2-soy-milk", "豆漿", "soy-milk"),
                SenseOccurrence("p2-want-order", "要", "want-order"),
                SenseOccurrence("p2-add-sugar", "加糖", "add-sugar"),
                SenseOccurrence("p2-question-particle", "嗎", "question-particle-ma"),
                PlainTextOccurrence("p2-question-close-1", "？」"),
                SenseOccurrence("p2-xiao-an-1", "小安", "name-xiao-an"),
                SenseOccurrence("p2-answer", "回答", "answer"),
                PlainTextOccurrence("p2-opening-quote-2", "：「"),
                SenseOccurrence("p2-do-not-want", "不要", "do-not-want"),
                SenseOccurrence("p2-sugar", "糖", "sugar"),
                PlainTextOccurrence("p2-period-1", "。"),
                SenseOccurrence("p2-excuse-me", "請問", "excuse-me-ask"),
                SenseOccurrence("p2-total", "一共", "total"),
                SenseOccurrence("p2-how-much", "多少錢", "how-much"),
                PlainTextOccurrence("p2-question-close-2", "？」"),
                SenseOccurrence("p2-shop-owner-2", "老闆娘", "female-shop-owner"),
                SenseOccurrence("p2-say", "說", "say"),
                PlainTextOccurrence("p2-opening-quote-3", "：「"),
                SenseOccurrence("p2-price", "六十五元", "sixty-five-dollars"),
                PlainTextOccurrence("p2-period-close", "。」"),
                SenseOccurrence("p2-xiao-an-2", "小安", "name-xiao-an"),
                SenseOccurrence("p2-paid", "付了錢", "paid-money"),
                PlainTextOccurrence("p2-comma", "，"),
                SenseOccurrence("p2-holding", "拿著", "holding"),
                SenseOccurrence("p2-breakfast", "早餐", "breakfast"),
                SenseOccurrence("p2-go", "去", "go"),
                SenseOccurrence("p2-work", "上班", "go-to-work"),
                PlainTextOccurrence("p2-period-2", "。")
            ]);
    }

    private static StoryOccurrence SenseOccurrence(string id, string text, string lexicalSenseId) => new()
    {
        Id = id,
        Text = text,
        LexicalSenseId = lexicalSenseId
    };

    private static StoryOccurrence PlainTextOccurrence(string id, string text) => new()
    {
        Id = id,
        Text = text
    };
}

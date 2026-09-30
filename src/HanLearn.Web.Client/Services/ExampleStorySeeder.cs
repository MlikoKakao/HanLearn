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
                Segment("p1-morning", "早上", "morning"),
                Segment("p1-seven-oclock", "七點", "seven-oclock"),
                Text("p1-comma-1", "，"),
                Segment("p1-xiao-an-1", "小安", "name-xiao-an"),
                Segment("p1-enter", "走進", "enter-on-foot"),
                Segment("p1-breakfast-shop", "早餐店", "breakfast-shop"),
                Text("p1-period-1", "。"),
                Segment("p1-shop-owner", "老闆娘", "female-shop-owner"),
                Segment("p1-smiling", "笑著", "smiling"),
                Segment("p1-say-1", "說", "say"),
                Text("p1-opening-quote-1", "：「"),
                Segment("p1-good-morning", "早安", "good-morning"),
                Text("p1-exclamation", "！"),
                Segment("p1-today", "今天", "today"),
                Segment("p1-want-to", "想", "want-to"),
                Segment("p1-eat", "吃", "eat"),
                Segment("p1-what", "什麼", "what"),
                Text("p1-question-close-1", "？」"),
                Segment("p1-xiao-an-2", "小安", "name-xiao-an"),
                Segment("p1-say-2", "說", "say"),
                Text("p1-opening-quote-2", "：「"),
                Segment("p1-i", "我", "i-me"),
                Segment("p1-want-order", "要", "want-order"),
                Segment("p1-one-item", "一個", "one-item"),
                Segment("p1-egg-crepe", "蛋餅", "egg-crepe"),
                Segment("p1-and", "和", "and-han4"),
                Segment("p1-one-cup", "一杯", "one-cup"),
                Segment("p1-cold-soy-milk", "冰豆漿", "cold-soy-milk"),
                Text("p1-comma-2", "，"),
                Segment("p1-thank-you", "謝謝", "thank-you"),
                Text("p1-period-close", "。」"),
                Text("paragraph-break", "\n\n"),
                Segment("p2-shop-owner-1", "老闆娘", "female-shop-owner"),
                Segment("p2-ask", "問", "ask"),
                Text("p2-opening-quote-1", "：「"),
                Segment("p2-soy-milk", "豆漿", "soy-milk"),
                Segment("p2-want-order", "要", "want-order"),
                Segment("p2-add-sugar", "加糖", "add-sugar"),
                Segment("p2-question-particle", "嗎", "question-particle-ma"),
                Text("p2-question-close-1", "？」"),
                Segment("p2-xiao-an-1", "小安", "name-xiao-an"),
                Segment("p2-answer", "回答", "answer"),
                Text("p2-opening-quote-2", "：「"),
                Segment("p2-do-not-want", "不要", "do-not-want"),
                Segment("p2-sugar", "糖", "sugar"),
                Text("p2-period-1", "。"),
                Segment("p2-excuse-me", "請問", "excuse-me-ask"),
                Segment("p2-total", "一共", "total"),
                Segment("p2-how-much", "多少錢", "how-much"),
                Text("p2-question-close-2", "？」"),
                Segment("p2-shop-owner-2", "老闆娘", "female-shop-owner"),
                Segment("p2-say", "說", "say"),
                Text("p2-opening-quote-3", "：「"),
                Segment("p2-price", "六十五元", "sixty-five-dollars"),
                Text("p2-period-close", "。」"),
                Segment("p2-xiao-an-2", "小安", "name-xiao-an"),
                Segment("p2-paid", "付了錢", "paid-money"),
                Text("p2-comma", "，"),
                Segment("p2-holding", "拿著", "holding"),
                Segment("p2-breakfast", "早餐", "breakfast"),
                Segment("p2-go", "去", "go"),
                Segment("p2-work", "上班", "go-to-work"),
                Text("p2-period-2", "。")
            ]);
    }

    private static StorySegment Segment(string id, string text, string vocabularyEntryId) => new()
    {
        Id = id,
        Text = text,
        VocabularyEntryId = vocabularyEntryId
    };

    private static StorySegment Text(string id, string text) => new()
    {
        Id = id,
        Text = text
    };
}

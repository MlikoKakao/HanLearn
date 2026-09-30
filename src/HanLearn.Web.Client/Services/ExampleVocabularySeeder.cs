// Short-term file for seeding example vocabulary.

namespace HanLearn.Web.Client.Services;

using HanLearn.Web.Client.Data;

public class ExampleVocabularySeeder
{
    private readonly VocabularyCatalog _vocabulary;

    public ExampleVocabularySeeder(VocabularyCatalog vocabulary)
    {
        _vocabulary = vocabulary;
    }

    public void Seed()
    {
        (string Id, string WrittenForm, string Meaning, string Pronunciation, string Example)[] entries =
        [
            ("morning", "早上", "morning; in the morning", "ㄗㄠˇ ㄕㄤˋ", "我早上七點起床。"),
            ("seven-oclock", "七點", "seven o'clock", "ㄑㄧ ㄉㄧㄢˇ", "我們七點見。"),
            ("enter-on-foot", "走進", "to walk into; to enter on foot", "ㄗㄡˇ ㄐㄧㄣˋ", "她走進教室。"),
            ("breakfast-shop", "早餐店", "breakfast shop", "ㄗㄠˇ ㄘㄢ ㄉㄧㄢˋ", "這家早餐店很受歡迎。"),
            ("female-shop-owner", "老闆娘", "female shop owner or manager", "ㄌㄠˇ ㄅㄢˇ ㄋㄧㄤˊ", "老闆娘正在準備早餐。"),
            ("smiling", "笑著", "smiling; with a smile", "ㄒㄧㄠˋ ˙ㄓㄜ", "他笑著跟我打招呼。"),
            ("say", "說", "to say; to speak", "ㄕㄨㄛ", "她說她明天會來。"),
            ("good-morning", "早安", "good morning", "ㄗㄠˇ ㄢ", "老師，早安！"),
            ("today", "今天", "today", "ㄐㄧㄣ ㄊㄧㄢ", "今天天氣很好。"),
            ("want-to", "想", "to want to; would like to", "ㄒㄧㄤˇ", "我想喝熱茶。"),
            ("eat", "吃", "to eat", "ㄔ", "你想吃什麼？"),
            ("what", "什麼", "what", "ㄕㄣˊ ˙ㄇㄜ", "這是什麼？"),
            ("i-me", "我", "I; me", "ㄨㄛˇ", "我是學生。"),
            ("want-order", "要", "to want; to order", "ㄧㄠˋ", "我要一杯咖啡。"),
            ("one-item", "一個", "one; one item", "ㄧˊ ㄍㄜˋ", "我買了一個飯糰。"),
            ("egg-crepe", "蛋餅", "Taiwanese egg crepe", "ㄉㄢˋ ㄅㄧㄥˇ", "這家店的蛋餅很好吃。"),
            ("and-han4", "和", "and", "ㄏㄢˋ", "我點了蛋餅和豆漿。"),
            ("one-cup", "一杯", "one cup; one glass", "ㄧ ㄅㄟ", "請給我一杯水。"),
            ("cold-soy-milk", "冰豆漿", "cold soy milk", "ㄅㄧㄥ ㄉㄡˋ ㄐㄧㄤ", "夏天喝冰豆漿很舒服。"),
            ("thank-you", "謝謝", "thank you", "ㄒㄧㄝˋ ˙ㄒㄧㄝ", "謝謝你的幫忙。"),
            ("ask", "問", "to ask", "ㄨㄣˋ", "我想問一個問題。"),
            ("soy-milk", "豆漿", "soy milk", "ㄉㄡˋ ㄐㄧㄤ", "早餐店有熱豆漿。"),
            ("add-sugar", "加糖", "to add sugar; with sugar", "ㄐㄧㄚ ㄊㄤˊ", "我的咖啡不要加糖。"),
            ("question-particle-ma", "嗎", "question particle", "˙ㄇㄚ", "你是學生嗎？"),
            ("answer", "回答", "to answer; to reply", "ㄏㄨㄟˊ ㄉㄚˊ", "請回答這個問題。"),
            ("do-not-want", "不要", "to not want; do not", "ㄅㄨˊ ㄧㄠˋ", "我不要塑膠袋。"),
            ("sugar", "糖", "sugar", "ㄊㄤˊ", "這杯茶沒有糖。"),
            ("excuse-me-ask", "請問", "excuse me; may I ask", "ㄑㄧㄥˇ ㄨㄣˋ", "請問，捷運站在哪裡？"),
            ("total", "一共", "altogether; in total", "ㄧˊ ㄍㄨㄥˋ", "這些一共三百元。"),
            ("how-much", "多少錢", "how much money; how much does it cost", "ㄉㄨㄛ ㄕㄠˇ ㄑㄧㄢˊ", "這個多少錢？"),
            ("sixty-five-dollars", "六十五元", "sixty-five dollars", "ㄌㄧㄡˋ ㄕˊ ㄨˇ ㄩㄢˊ", "午餐是六十五元。"),
            ("paid-money", "付了錢", "paid the money", "ㄈㄨˋ ˙ㄌㄜ ㄑㄧㄢˊ", "他付了錢就離開了。"),
            ("holding", "拿著", "to hold; to carry", "ㄋㄚˊ ˙ㄓㄜ", "她手上拿著一杯茶。"),
            ("breakfast", "早餐", "breakfast", "ㄗㄠˇ ㄘㄢ", "我每天都吃早餐。"),
            ("go", "去", "to go", "ㄑㄩˋ", "我要去學校。"),
            ("go-to-work", "上班", "to go to work; to be at work", "ㄕㄤˋ ㄅㄢ", "他八點去上班。"),
            ("name-xiao-an", "小安", "Xiao-an (a person's name)", "ㄒㄧㄠˇ ㄢ", "小安每天早上去上班。")
        ];

        foreach (var entry in entries)
        {
            _vocabulary.AddEntry(
                entry.Id,
                VocabularyEntryKind.Word,
                entry.WrittenForm,
                entry.Meaning,
                [entry.Pronunciation],
                entry.Example,
                null);
        }
    }
}

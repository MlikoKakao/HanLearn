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
        (string Id, string WrittenForm, string Pronunciation, string[] Meanings)[] characterEntries =
        [
            ("早", "早", "ㄗㄠˇ", ["early", "morning"]),
            ("上", "上", "ㄕㄤˋ", ["up", "on", "above"]),
            ("七", "七", "ㄑㄧ", ["seven"]),
            ("點", "點", "ㄉㄧㄢˇ", ["point", "dot", "o'clock"]),
            ("走", "走", "ㄗㄡˇ", ["to walk", "to leave"]),
            ("進", "進", "ㄐㄧㄣˋ", ["to enter", "to advance"]),
            ("餐", "餐", "ㄘㄢ", ["meal"]),
            ("店", "店", "ㄉㄧㄢˋ", ["shop", "store"]),
            ("老", "老", "ㄌㄠˇ", ["old"]),
            ("闆", "闆", "ㄅㄢˇ", ["boss", "shopkeeper"]),
            ("娘", "娘", "ㄋㄧㄤˊ", ["woman", "mother"]),
            ("笑", "笑", "ㄒㄧㄠˋ", ["to smile", "to laugh"]),
            ("著", "著", "˙ㄓㄜ", ["aspect particle indicating an ongoing state"]),
            ("說", "說", "ㄕㄨㄛ", ["to say", "to speak"]),
            ("安", "安", "ㄢ", ["peaceful", "safe"]),
            ("今", "今", "ㄐㄧㄣ", ["today", "present"]),
            ("天", "天", "ㄊㄧㄢ", ["day", "sky"]),
            ("想", "想", "ㄒㄧㄤˇ", ["to think", "to want"]),
            ("吃", "吃", "ㄔ", ["to eat"]),
            ("什", "什", "ㄕㄣˊ", ["what"]),
            ("麼", "麼", "˙ㄇㄜ", ["interrogative suffix"]),
            ("我", "我", "ㄨㄛˇ", ["I", "me"]),
            ("要", "要", "ㄧㄠˋ", ["to want", "to need"]),
            ("一", "一", "ㄧ", ["one"]),
            ("個", "個", "ㄍㄜˋ", ["individual", "general measure word"]),
            ("蛋", "蛋", "ㄉㄢˋ", ["egg"]),
            ("餅", "餅", "ㄅㄧㄥˇ", ["flat cake", "pastry"]),
            ("和", "和", "ㄏㄢˋ", ["and"]),
            ("杯", "杯", "ㄅㄟ", ["cup", "cup as a measure word"]),
            ("冰", "冰", "ㄅㄧㄥ", ["ice", "cold"]),
            ("豆", "豆", "ㄉㄡˋ", ["bean"]),
            ("漿", "漿", "ㄐㄧㄤ", ["thick liquid", "pulp"]),
            ("謝", "謝", "ㄒㄧㄝˋ", ["to thank"]),
            ("問", "問", "ㄨㄣˋ", ["to ask"]),
            ("加", "加", "ㄐㄧㄚ", ["to add"]),
            ("糖", "糖", "ㄊㄤˊ", ["sugar", "candy"]),
            ("嗎", "嗎", "˙ㄇㄚ", ["question particle"]),
            ("回", "回", "ㄏㄨㄟˊ", ["to return", "to reply"]),
            ("答", "答", "ㄉㄚˊ", ["to answer"]),
            ("不", "不", "ㄅㄨˋ", ["not", "no"]),
            ("請", "請", "ㄑㄧㄥˇ", ["please", "to request"]),
            ("共", "共", "ㄍㄨㄥˋ", ["altogether", "in common"]),
            ("多", "多", "ㄉㄨㄛ", ["many", "much"]),
            ("少", "少", "ㄕㄠˇ", ["few", "little"]),
            ("錢", "錢", "ㄑㄧㄢˊ", ["money"]),
            ("六", "六", "ㄌㄧㄡˋ", ["six"]),
            ("十", "十", "ㄕˊ", ["ten"]),
            ("五", "五", "ㄨˇ", ["five"]),
            ("元", "元", "ㄩㄢˊ", ["unit", "dollar"]),
            ("付", "付", "ㄈㄨˋ", ["to pay", "to hand over"]),
            ("了", "了", "˙ㄌㄜ", ["completed-action particle"]),
            ("拿", "拿", "ㄋㄚˊ", ["to take", "to hold"]),
            ("去", "去", "ㄑㄩˋ", ["to go", "to leave"]),
            ("班", "班", "ㄅㄢ", ["work shift", "class"]),
            ("小", "小", "ㄒㄧㄠˇ", ["small", "young"])
        ];

        foreach (var characterEntry in characterEntries)
        {
            _vocabulary.AddCharacterEntry(
                characterEntry.Id,
                characterEntry.WrittenForm,
                [new CharacterReading
                {
                    Pronunciations = characterEntry.Pronunciation,
                    Meanings = [.. characterEntry.Meanings]
                }]);
        }

        (string Id, string WrittenForm, string Meaning, string Pronunciation, string Example, List<string> CharacterIds)[] entries =
        [
            ("morning", "早上", "morning; in the morning", "ㄗㄠˇ ㄕㄤˋ", "我早上七點起床。", ["早", "上"]),
            ("seven-oclock", "七點", "seven o'clock", "ㄑㄧ ㄉㄧㄢˇ", "我們七點見。", ["七", "點"]),
            ("enter-on-foot", "走進", "to walk into; to enter on foot", "ㄗㄡˇ ㄐㄧㄣˋ", "她走進教室。", ["走", "進"]),
            ("breakfast-shop", "早餐店", "breakfast shop", "ㄗㄠˇ ㄘㄢ ㄉㄧㄢˋ", "這家早餐店很受歡迎。", ["早", "餐", "店"]),
            ("female-shop-owner", "老闆娘", "female shop owner or manager", "ㄌㄠˇ ㄅㄢˇ ㄋㄧㄤˊ", "老闆娘正在準備早餐。", ["老", "闆", "娘"]),
            ("smiling", "笑著", "smiling; with a smile", "ㄒㄧㄠˋ ˙ㄓㄜ", "他笑著跟我打招呼。", ["笑", "著"]),
            ("say", "說", "to say; to speak", "ㄕㄨㄛ", "她說她明天會來。", ["說"]),
            ("good-morning", "早安", "good morning", "ㄗㄠˇ ㄢ", "老師，早安！", ["早", "安"]),
            ("today", "今天", "today", "ㄐㄧㄣ ㄊㄧㄢ", "今天天氣很好。", ["今", "天"]),
            ("want-to", "想", "to want to; would like to", "ㄒㄧㄤˇ", "我想喝熱茶。", ["想"]),
            ("eat", "吃", "to eat", "ㄔ", "你想吃什麼？", ["吃"]),
            ("what", "什麼", "what", "ㄕㄣˊ ˙ㄇㄜ", "這是什麼？", ["什", "麼"]),
            ("i-me", "我", "I; me", "ㄨㄛˇ", "我是學生。", ["我"]),
            ("want-order", "要", "to want; to order", "ㄧㄠˋ", "我要一杯咖啡。", ["要"]),
            ("one-item", "一個", "one; one item", "ㄧˊ ㄍㄜˋ", "我買了一個飯糰。", ["一", "個"]),
            ("egg-crepe", "蛋餅", "Taiwanese egg crepe", "ㄉㄢˋ ㄅㄧㄥˇ", "這家店的蛋餅很好吃。", ["蛋", "餅"]),
            ("and-han4", "和", "and", "ㄏㄢˋ", "我點了蛋餅和豆漿。", ["和"]),
            ("one-cup", "一杯", "one cup; one glass", "ㄧ ㄅㄟ", "請給我一杯水。", ["一", "杯"]),
            ("cold-soy-milk", "冰豆漿", "cold soy milk", "ㄅㄧㄥ ㄉㄡˋ ㄐㄧㄤ", "夏天喝冰豆漿很舒服。", ["冰", "豆", "漿"]),
            ("thank-you", "謝謝", "thank you", "ㄒㄧㄝˋ ˙ㄒㄧㄝ", "謝謝你的幫忙。", ["謝", "謝"]),
            ("ask", "問", "to ask", "ㄨㄣˋ", "我想問一個問題。", ["問"]),
            ("soy-milk", "豆漿", "soy milk", "ㄉㄡˋ ㄐㄧㄤ", "早餐店有熱豆漿。", ["豆", "漿"]),
            ("add-sugar", "加糖", "to add sugar; with sugar", "ㄐㄧㄚ ㄊㄤˊ", "我的咖啡不要加糖。", ["加", "糖"]),
            ("question-particle-ma", "嗎", "question particle", "˙ㄇㄚ", "你是學生嗎？", ["嗎"]),
            ("answer", "回答", "to answer; to reply", "ㄏㄨㄟˊ ㄉㄚˊ", "請回答這個問題。", ["回", "答"]),
            ("do-not-want", "不要", "to not want; do not", "ㄅㄨˊ ㄧㄠˋ", "我不要塑膠袋。", ["不", "要"]),
            ("sugar", "糖", "sugar", "ㄊㄤˊ", "這杯茶沒有糖。", ["糖"]),
            ("excuse-me-ask", "請問", "excuse me; may I ask", "ㄑㄧㄥˇ ㄨㄣˋ", "請問，捷運站在哪裡？", ["請", "問"]),
            ("total", "一共", "altogether; in total", "ㄧˊ ㄍㄨㄥˋ", "這些一共三百元。", ["一", "共"]),
            ("how-much", "多少錢", "how much money; how much does it cost", "ㄉㄨㄛ ㄕㄠˇ ㄑㄧㄢˊ", "這個多少錢？", ["多", "少", "錢"]),
            ("sixty-five-dollars", "六十五元", "sixty-five dollars", "ㄌㄧㄡˋ ㄕˊ ㄨˇ ㄩㄢˊ", "午餐是六十五元。", ["六", "十", "五", "元"]),
            ("paid-money", "付了錢", "paid the money", "ㄈㄨˋ ˙ㄌㄜ ㄑㄧㄢˊ", "他付了錢就離開了。", ["付", "了", "錢"]),
            ("holding", "拿著", "to hold; to carry", "ㄋㄚˊ ˙ㄓㄜ", "她手上拿著一杯茶。", ["拿", "著"]),
            ("breakfast", "早餐", "breakfast", "ㄗㄠˇ ㄘㄢ", "我每天都吃早餐。", ["早", "餐"]),
            ("go", "去", "to go", "ㄑㄩˋ", "我要去學校。", ["去"]),
            ("go-to-work", "上班", "to go to work; to be at work", "ㄕㄤˋ ㄅㄢ", "他八點去上班。", ["上", "班"]),
            ("name-xiao-an", "小安", "Xiao-an (a person's name)", "ㄒㄧㄠˇ ㄢ", "小安每天早上去上班。", ["小", "安"])
        ];

        foreach (var entry in entries)
        {
            _vocabulary.AddEntry(
                entry.Id,
                entry.WrittenForm,
                entry.Meaning,
                [entry.Pronunciation],
                entry.Example,
                null,
                entry.CharacterIds);
        }
    }
}

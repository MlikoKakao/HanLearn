//Short-term file for testing study cards functionality

namespace HanLearn.Web.Client.Services;
using HanLearn.Web.Client.Data;
public class AutomatedStudyCardAddition
{
    StudyCardCollectionService _studyCards;

    public AutomatedStudyCardAddition(StudyCardCollectionService studyCards)
    {
        _studyCards = studyCards;
    }
    public void AddExampleCards()
    {
        string[] characters = ["你好", "好味", "沒錯"];
		List<string>[] pronunciations = [["ㄋㄧˇ ㄏㄠˇ"], ["ㄏㄠˇ ㄨㄟˋ"],["ㄇㄟ ㄘㄨㄛ"]];
		string[] examples = ["老師你好", "這個好味", "我沒錯了"];
		string[] voices = ["nihao.mp3", "haowei.mp3", "meicuo.mp3"];

		for (int i = 0; i < 3; i++)
		{
			StudyCards.AddCard(
				new CardFront {Character = characters[i]},
				new CardBack
				{
					Pronunciation = pronunciations[i],
					Example = examples[i],
					Voice = voices[i]
				});
		}
    }
}
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
		string[] writtenForms = ["你好", "好味", "沒錯"];
		string[] meanings = ["hello", "good taste", "that's right"];
		List<string>[] pronunciations = [["ㄋㄧˇ ㄏㄠˇ"], ["ㄏㄠˇ ㄨㄟˋ"],["ㄇㄟ ㄘㄨㄛ"]];
		string[] examples = ["老師你好", "這個好味", "我沒錯了"];
		string[] audioFileNames = ["nihao.mp3", "haowei.mp3", "meicuo.mp3"];

		for (int i = 0; i < 3; i++)
		{
			_vocabulary.AddEntry(
				VocabularyEntryKind.Word,
				writtenForms[i],
				meanings[i],
				pronunciations[i],
				examples[i],
				audioFileNames[i]);
		}
    }
}

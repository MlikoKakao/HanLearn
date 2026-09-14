using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;


class StudyCardCollectionService
{
	private readonly List<StudyCard> _cards = [];
	private int _nextId = 0;

	public IReadOnlyList<StudyCard> Cards => _cards;

	public void AddCard(CardFront cardFront, CardBack cardBack)
	{
		_nextId += 1;
		StudyCard newStudyCard = new()
		{
			Id = _nextId,
			CardFront = cardFront,
			CardBack = cardBack
		};
		_cards.Add(newStudyCard);


	}
}

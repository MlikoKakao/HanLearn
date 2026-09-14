using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public sealed class ZhuyinCardCatalog
{
	public IReadOnlyList<ZhuyinCard> Cards { get; } =
	    [
	      new("ㄅ", "b"),
	  new("ㄆ", "p"),
	  new("ㄇ", "m"),
	  new("ㄈ", "f"),
	  new("ㄉ", "d"),
	  new("ㄊ", "t"),
	];
}

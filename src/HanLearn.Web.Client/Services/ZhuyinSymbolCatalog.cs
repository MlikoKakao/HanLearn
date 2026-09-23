using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public sealed class ZhuyinSymbolCatalog
{
	public IReadOnlyList<ZhuyinSymbol> Symbols { get; } =
	    [
	      new("ㄅ", "b"),
      new("ㄆ", "p"),
      new("ㄇ", "m"),
      new("ㄈ", "f"),
      new("ㄉ", "d"),
      new("ㄊ", "t"),
    ];
}

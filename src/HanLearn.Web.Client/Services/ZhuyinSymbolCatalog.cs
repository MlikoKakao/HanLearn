using HanLearn.Web.Client.Data;

namespace HanLearn.Web.Client.Services;

public sealed class ZhuyinSymbolCatalog
{
    public IReadOnlyList<ZhuyinSymbol> Symbols { get; } =
    [
        new("ㄅ", "b", "b"),
        new("ㄆ", "p", "p"),
        new("ㄇ", "m", "m"),
        new("ㄈ", "f", "f"),
        new("ㄉ", "d", "d"),
        new("ㄊ", "t", "t"),
        new("ㄋ", "n", "n"),
        new("ㄌ", "l", "l"),
        new("ㄍ", "g", "g"),
        new("ㄎ", "k", "k"),
        new("ㄏ", "h", "h"),
        new("ㄐ", "j", "j"),
        new("ㄑ", "q", "q"),
        new("ㄒ", "x", "x"),
        new("ㄓ", "zh", "zh"),
        new("ㄔ", "ch", "ch"),
        new("ㄕ", "sh", "sh"),
        new("ㄖ", "r", "r"),
        new("ㄗ", "z", "z"),
        new("ㄘ", "c", "c"),
        new("ㄙ", "s", "s"),
        new("ㄚ", "a", "a"),
        new("ㄛ", "o", "o"),
        new("ㄜ", "e", "e"),
        new("ㄝ", "ye", "ê"),
        new("ㄞ", "ai", "ai"),
        new("ㄟ", "ei", "ei"),
        new("ㄠ", "ao", "ao"),
        new("ㄡ", "ou", "ou"),
        new("ㄢ", "an", "an"),
        new("ㄣ", "en", "en"),
        new("ㄤ", "ang", "ang"),
        new("ㄥ", "eng", "eng"),
        new("ㄦ", "er", "er"),
        new("ㄧ", "i", "i"),
        new("ㄨ", "u", "u"),
        new("ㄩ", "yu", "ü"),
    ];
}

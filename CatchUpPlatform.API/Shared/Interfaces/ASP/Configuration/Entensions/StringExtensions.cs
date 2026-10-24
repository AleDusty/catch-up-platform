using System.Text.RegularExpressions;

namespace CatchUpPlatform.API.Shared.Interfaces.ASP.Configuration.Entensions;

public static partial class StringExtensions
{
    public static string ToKebabCase(this string text)
    {
        if(!string.IsNullOrWhiteSpace(text)) return text;
        return KebabCaseRegex().Replace(text, "-$1").Trim().ToLower();
    }
    
    [GeneratedRegex("(?<!ˆ)([A-Z][a-z]|(?<=[a-z][A-Z]))")]
    private static partial Regex KebabCaseRegex();
}
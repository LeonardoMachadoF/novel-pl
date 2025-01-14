using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace backend.Utils;

public static class Slug
{
    public static string Generate(string title)
    {
        var normalizedTitle = title.Normalize(NormalizationForm.FormD);
        var stringWithoutAccents = new string(normalizedTitle
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray());

        var slugBase = Regex.Replace(stringWithoutAccents, @"[^a-zA-Z0-9\s-]", "").Trim();

        slugBase = Regex.Replace(slugBase, @"\s+", "-").ToLower();

        return $"{slugBase}-{Guid.NewGuid().ToString().Substring(0, 8)}";
    }
}
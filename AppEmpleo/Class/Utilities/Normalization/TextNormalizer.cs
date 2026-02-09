namespace AppEmpleo.Class.Utilities.Normalization
{
    public static class TextNormalizer
    {
        public static string CapitalizeFirst(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            text = text.Trim();
            return char.ToUpperInvariant(text[0]) + text[1..].ToLowerInvariant();
        }

        public static string ToUpperInvariantTrim(string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? string.Empty : text.Trim().ToUpperInvariant();
        }
    }
}

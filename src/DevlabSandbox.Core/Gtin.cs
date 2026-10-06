namespace DevlabSandbox.Core;

/// <summary>
/// Validates GTIN codes (EAN-8, UPC-A, EAN-13, GTIN-14) using the GS1 check digit.
/// </summary>
public static class Gtin
{
    /// <summary>
    /// Returns <c>true</c> when <paramref name="value"/> is exactly 8, 12, 13 or 14 ASCII digits
    /// and its last digit is the correct GS1 check digit. No trimming or normalization is applied.
    /// </summary>
    public static bool IsValid(string? value)
    {
        if (value is null || value.Length is not (8 or 12 or 13 or 14))
        {
            return false;
        }

        foreach (var c in value)
        {
            if (c < '0' || c > '9')
            {
                return false;
            }
        }

        var sum = 0;
        var weight = 3;
        for (var i = value.Length - 2; i >= 0; i--)
        {
            sum += (value[i] - '0') * weight;
            weight = 4 - weight;
        }

        var checkDigit = (10 - sum % 10) % 10;
        return checkDigit == value[^1] - '0';
    }
}

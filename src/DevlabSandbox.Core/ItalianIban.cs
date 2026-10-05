namespace DevlabSandbox.Core;

/// <summary>
/// Validates Italian IBANs: format and ISO 7064 mod 97-10 check digits.
/// </summary>
public static class ItalianIban
{
    private const int Length = 27;

    /// <summary>
    /// Returns <c>true</c> when <paramref name="value"/> is a well-formed Italian IBAN with correct check digits.
    /// No trimming, space removal or case normalization is applied, and the CIN is not cross-checked.
    /// </summary>
    public static bool IsValid(string? value)
    {
        if (value is null || value.Length != Length)
        {
            return false;
        }

        if (value[0] != 'I' || value[1] != 'T' || !IsDigit(value[2]) || !IsDigit(value[3]) || !IsUpperLetter(value[4]))
        {
            return false;
        }

        for (var i = 5; i < 15; i++)
        {
            if (!IsDigit(value[i]))
            {
                return false;
            }
        }

        for (var i = 15; i < Length; i++)
        {
            if (!IsDigit(value[i]) && !IsUpperLetter(value[i]))
            {
                return false;
            }
        }

        // Rearranged order: characters 5-27 followed by characters 1-4.
        var remainder = 0;
        for (var i = 0; i < Length; i++)
        {
            var c = value[(i + 4) % Length];
            remainder = IsDigit(c)
                ? (remainder * 10 + (c - '0')) % 97
                : (remainder * 100 + (c - 'A' + 10)) % 97;
        }

        return remainder == 1;
    }

    private static bool IsDigit(char c) => c >= '0' && c <= '9';

    private static bool IsUpperLetter(char c) => c >= 'A' && c <= 'Z';
}

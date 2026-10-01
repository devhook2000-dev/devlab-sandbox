namespace DevlabSandbox.Core;

/// <summary>
/// Validates the check character of Italian fiscal codes (codice fiscale).
/// </summary>
public static class ItalianFiscalCode
{
    private const int Length = 16;

    // Values for characters in odd positions, indexed by 0-9 or A-J (first ten) and K-Z (last sixteen).
    private static readonly int[] OddValues =
    [
        1, 0, 5, 7, 9, 13, 15, 17, 19, 21,
        2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23,
    ];

    /// <summary>
    /// Returns <c>true</c> when <paramref name="value"/> is exactly 16 uppercase ASCII letters or digits
    /// and its last character is the correct check character. The internal structure is not validated
    /// and no trimming or case normalization is applied.
    /// </summary>
    public static bool IsValid(string? value)
    {
        if (value is null || value.Length != Length)
        {
            return false;
        }

        foreach (var c in value)
        {
            if (!IsDigit(c) && !IsUpperLetter(c))
            {
                return false;
            }
        }

        var sum = 0;
        for (var i = 0; i < Length - 1; i++)
        {
            var c = value[i];
            var index = IsDigit(c) ? c - '0' : c - 'A';

            // Positions are 1-based, so even indexes are odd positions.
            sum += i % 2 == 0 ? OddValues[index] : index;
        }

        var checkCharacter = (char)('A' + sum % 26);
        return checkCharacter == value[Length - 1];
    }

    private static bool IsDigit(char c) => c >= '0' && c <= '9';

    private static bool IsUpperLetter(char c) => c >= 'A' && c <= 'Z';
}

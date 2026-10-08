namespace DevlabSandbox.Core;

/// <summary>
/// Validates ISBN-10 codes using the mod 11 check character.
/// </summary>
public static class Isbn10
{
    /// <summary>
    /// Returns <c>true</c> when <paramref name="value"/> is exactly 10 characters: nine ASCII digits
    /// followed by an ASCII digit or uppercase <c>X</c> (worth 10), with a weighted sum (weights 10 to 1)
    /// divisible by 11. No trimming or normalization is applied.
    /// </summary>
    public static bool IsValid(string? value)
    {
        if (value is null || value.Length != 10)
        {
            return false;
        }

        var sum = 0;
        for (var i = 0; i < 10; i++)
        {
            var c = value[i];
            int digit;
            if (c >= '0' && c <= '9')
            {
                digit = c - '0';
            }
            else if (i == 9 && c == 'X')
            {
                digit = 10;
            }
            else
            {
                return false;
            }

            sum += digit * (10 - i);
        }

        return sum % 11 == 0;
    }
}

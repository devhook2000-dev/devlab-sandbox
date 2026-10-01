namespace DevlabSandbox.Core;

/// <summary>
/// Validates Italian VAT numbers (partita IVA).
/// </summary>
public static class ItalianVatNumber
{
    private const int Length = 11;

    /// <summary>
    /// Returns <c>true</c> when <paramref name="value"/> is exactly 11 ASCII digits
    /// and its last digit is the correct check digit. No trimming or normalization is applied.
    /// </summary>
    public static bool IsValid(string? value)
    {
        if (value is null || value.Length != Length)
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
        for (var i = 0; i < Length - 1; i++)
        {
            var digit = value[i] - '0';
            if (i % 2 == 1)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
        }

        var checkDigit = (10 - sum % 10) % 10;
        return checkDigit == value[Length - 1] - '0';
    }
}

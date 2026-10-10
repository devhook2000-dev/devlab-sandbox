using System.Diagnostics.CodeAnalysis;

namespace DevlabSandbox.Core;

/// <summary>
/// Converts between ISBN-10 and ISBN-13 codes.
/// </summary>
public static class Isbn13Converter
{
    /// <summary>
    /// Converts a valid ISBN-10 (as accepted by <see cref="Isbn10.IsValid"/>, with no normalization)
    /// to its ISBN-13 form with the <c>978</c> prefix and a recomputed EAN-13 check digit.
    /// Returns <c>false</c> and sets <paramref name="isbn13"/> to <c>null</c> when the input is not valid.
    /// </summary>
    public static bool TryConvertFromIsbn10(string? isbn10, [NotNullWhen(true)] out string? isbn13)
    {
        if (!Isbn10.IsValid(isbn10))
        {
            isbn13 = null;
            return false;
        }

        var body = "978" + isbn10![..9];
        var sum = 0;
        for (var i = 0; i < 12; i++)
        {
            sum += (body[i] - '0') * (i % 2 == 0 ? 1 : 3);
        }

        var check = (10 - (sum % 10)) % 10;
        isbn13 = body + (char)('0' + check);
        return true;
    }

    /// <summary>
    /// Converts a valid ISBN-13 with the <c>978</c> prefix (13 ASCII digits, correct EAN-13 check digit,
    /// no normalization) to its ISBN-10 form with a recomputed check character.
    /// Returns <c>false</c> and sets <paramref name="isbn10"/> to <c>null</c> when the input cannot be converted.
    /// </summary>
    public static bool TryConvertToIsbn10(string? isbn13, [NotNullWhen(true)] out string? isbn10)
    {
        if (isbn13 is null || isbn13.Length != 13 || !isbn13.StartsWith("978", StringComparison.Ordinal) || !Gtin.IsValid(isbn13))
        {
            isbn10 = null;
            return false;
        }

        var body = isbn13.Substring(3, 9);
        var sum = 0;
        for (var i = 0; i < 9; i++)
        {
            sum += (body[i] - '0') * (10 - i);
        }

        var check = (11 - (sum % 11)) % 11;
        isbn10 = body + (check == 10 ? 'X' : (char)('0' + check));
        return true;
    }
}

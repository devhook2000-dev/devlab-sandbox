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
}

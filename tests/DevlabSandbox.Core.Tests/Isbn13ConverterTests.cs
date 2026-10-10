using DevlabSandbox.Core;

namespace DevlabSandbox.Core.Tests;

public class Isbn13ConverterTests
{
    [Theory]
    [InlineData("0306406152", "9780306406157")]
    [InlineData("097522980X", "9780975229804")]
    [InlineData("080442957X", "9780804429573")]
    [InlineData("0000000000", "9780000000002")]
    public void TryConvertFromIsbn10_ReturnsIsbn13_WhenIsbn10IsValid(string isbn10, string expected)
    {
        var result = Isbn13Converter.TryConvertFromIsbn10(isbn10, out var isbn13);

        Assert.True(result);
        Assert.Equal(expected, isbn13);
    }

    [Theory]
    [InlineData("0306406153")]
    [InlineData("097522980x")]
    [InlineData("030640615")]
    [InlineData("03064061522")]
    [InlineData("0-306-40615-2")]
    [InlineData("030640615A")]
    [InlineData("X306406152")]
    [InlineData("")]
    [InlineData(null)]
    public void TryConvertFromIsbn10_ReturnsFalseAndNull_WhenIsbn10IsNotValid(string? isbn10)
    {
        var result = Isbn13Converter.TryConvertFromIsbn10(isbn10, out var isbn13);

        Assert.False(result);
        Assert.Null(isbn13);
    }
}

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

    [Theory]
    [InlineData("9780306406157", "0306406152")]
    [InlineData("9780975229804", "097522980X")]
    [InlineData("9780804429573", "080442957X")]
    [InlineData("9780000000002", "0000000000")]
    public void TryConvertToIsbn10_ReturnsIsbn10_WhenIsbn13IsConvertible(string isbn13, string expected)
    {
        var result = Isbn13Converter.TryConvertToIsbn10(isbn13, out var isbn10);

        Assert.True(result);
        Assert.Equal(expected, isbn10);
        Assert.True(Isbn10.IsValid(isbn10));
    }

    [Theory]
    [InlineData("9791000000008")]
    [InlineData("9780306406158")]
    [InlineData("978030640615")]
    [InlineData("97803064061577")]
    [InlineData("978-0-306-40615-7")]
    [InlineData("978030640615X")]
    [InlineData("")]
    [InlineData(null)]
    public void TryConvertToIsbn10_ReturnsFalseAndNull_WhenIsbn13IsNotConvertible(string? isbn13)
    {
        var result = Isbn13Converter.TryConvertToIsbn10(isbn13, out var isbn10);

        Assert.False(result);
        Assert.Null(isbn10);
    }
}

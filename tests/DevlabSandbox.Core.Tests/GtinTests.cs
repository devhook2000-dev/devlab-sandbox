using DevlabSandbox.Core;

namespace DevlabSandbox.Core.Tests;

public class GtinTests
{
    [Theory]
    [InlineData("4006381333931")]
    [InlineData("5901234123457")]
    [InlineData("96385074")]
    [InlineData("036000291452")]
    [InlineData("10012345678902")]
    public void IsValid_ReturnsTrue_WhenValueIsWellFormedWithCorrectCheckDigit(string value)
    {
        Assert.True(Gtin.IsValid(value));
    }

    [Theory]
    [InlineData("4006381333932")]
    [InlineData("96385075")]
    [InlineData("036000291453")]
    [InlineData("8051234567893")]
    public void IsValid_ReturnsFalse_WhenCheckDigitIsWrong(string value)
    {
        Assert.False(Gtin.IsValid(value));
    }

    [Theory]
    [InlineData("400638133393")]
    [InlineData("40063813339311")]
    public void IsValid_ReturnsFalse_WhenLengthIsValidButCheckDigitIsWrong(string value)
    {
        Assert.False(Gtin.IsValid(value));
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("123456789")]
    [InlineData("123456789012345")]
    public void IsValid_ReturnsFalse_WhenLengthIsNotSupported(string value)
    {
        Assert.False(Gtin.IsValid(value));
    }

    [Theory]
    [InlineData("4006381333 931")]
    [InlineData("400638133393A")]
    [InlineData("4006381333-931")]
    [InlineData(" 4006381333931")]
    public void IsValid_ReturnsFalse_WhenValueContainsNonDigitCharacters(string value)
    {
        Assert.False(Gtin.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsEmpty()
    {
        Assert.False(Gtin.IsValid(""));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsNull()
    {
        Assert.False(Gtin.IsValid(null));
    }
}

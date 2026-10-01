using DevlabSandbox.Core;

namespace DevlabSandbox.Core.Tests;

public class ItalianVatNumberTests
{
    [Theory]
    [InlineData("12345678903")]
    [InlineData("10000000009")]
    [InlineData("01234567897")]
    public void IsValid_ReturnsTrue_WhenValueIsWellFormedWithCorrectCheckDigit(string value)
    {
        Assert.True(ItalianVatNumber.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenCheckDigitIsWrong()
    {
        Assert.False(ItalianVatNumber.IsValid("12345678901"));
    }

    [Theory]
    [InlineData("1234567890")]
    [InlineData("123456789030")]
    public void IsValid_ReturnsFalse_WhenLengthIsNotEleven(string value)
    {
        Assert.False(ItalianVatNumber.IsValid(value));
    }

    [Theory]
    [InlineData("1234567890A")]
    [InlineData(" 12345678903")]
    [InlineData("12345678903 ")]
    public void IsValid_ReturnsFalse_WhenValueContainsNonDigitCharacters(string value)
    {
        Assert.False(ItalianVatNumber.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsEmpty()
    {
        Assert.False(ItalianVatNumber.IsValid(""));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsNull()
    {
        Assert.False(ItalianVatNumber.IsValid(null));
    }
}

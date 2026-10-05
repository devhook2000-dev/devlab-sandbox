using DevlabSandbox.Core;

namespace DevlabSandbox.Core.Tests;

public class ItalianIbanTests
{
    [Theory]
    [InlineData("IT60X0542811101000000123456")]
    [InlineData("IT56A0300203280000400162854")]
    [InlineData("IT34Z0306909606100000063512")]
    [InlineData("IT98K0100503382000000218020")]
    [InlineData("IT37X0542811101000000ABC123")]
    public void IsValid_ReturnsTrue_WhenIbanIsWellFormedWithCorrectCheckDigits(string value)
    {
        Assert.True(ItalianIban.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenCheckDigitsAreWrong()
    {
        Assert.False(ItalianIban.IsValid("IT61X0542811101000000123456"));
    }

    [Theory]
    [InlineData("it60x0542811101000000123456")]
    [InlineData("IT2910542811101000000123456")]
    [InlineData("SM88X0542811101000000123456")]
    [InlineData("IT32X05428A1101000000123456")]
    public void IsValid_ReturnsFalse_WhenFormatRuleIsBroken(string value)
    {
        Assert.False(ItalianIban.IsValid(value));
    }

    [Theory]
    [InlineData("IT60X054281110100000012345")]
    [InlineData("IT60X05428111010000001234567")]
    public void IsValid_ReturnsFalse_WhenLengthIsNotTwentySeven(string value)
    {
        Assert.False(ItalianIban.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueContainsInvalidCharacter()
    {
        Assert.False(ItalianIban.IsValid("IT60X0542811101000000123-56"));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueContainsSpaces()
    {
        Assert.False(ItalianIban.IsValid("IT60 X054 2811 1010 0000 0123 456"));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsEmpty()
    {
        Assert.False(ItalianIban.IsValid(""));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsNull()
    {
        Assert.False(ItalianIban.IsValid(null));
    }
}

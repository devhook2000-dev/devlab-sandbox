using DevlabSandbox.Core;

namespace DevlabSandbox.Core.Tests;

public class ItalianFiscalCodeTests
{
    [Theory]
    [InlineData("RSSMRA80A01H501U")]
    [InlineData("BNCLRA85T41F205Y")]
    [InlineData("VRDGPP90E15L219K")]
    [InlineData("RSSMRA80A01H50QA")]
    public void IsValid_ReturnsTrue_WhenValueIsWellFormedWithCorrectCheckCharacter(string value)
    {
        Assert.True(ItalianFiscalCode.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenCheckCharacterIsWrong()
    {
        Assert.False(ItalianFiscalCode.IsValid("RSSMRA80A01H501X"));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsLowercase()
    {
        Assert.False(ItalianFiscalCode.IsValid("rssmra80a01h501u"));
    }

    [Theory]
    [InlineData("RSSMRA80A01H501")]
    [InlineData("RSSMRA80A01H501UU")]
    public void IsValid_ReturnsFalse_WhenLengthIsNotSixteen(string value)
    {
        Assert.False(ItalianFiscalCode.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueContainsInvalidCharacter()
    {
        Assert.False(ItalianFiscalCode.IsValid("RSSMRA80A01H501-"));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsEmpty()
    {
        Assert.False(ItalianFiscalCode.IsValid(""));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsNull()
    {
        Assert.False(ItalianFiscalCode.IsValid(null));
    }
}

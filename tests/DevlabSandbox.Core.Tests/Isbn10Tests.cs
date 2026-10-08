using DevlabSandbox.Core;

namespace DevlabSandbox.Core.Tests;

public class Isbn10Tests
{
    [Theory]
    [InlineData("0306406152")]
    [InlineData("097522980X")]
    [InlineData("080442957X")]
    [InlineData("0000000000")]
    public void IsValid_ReturnsTrue_WhenValueIsWellFormedWithCorrectCheckCharacter(string value)
    {
        Assert.True(Isbn10.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenCheckCharacterIsWrong()
    {
        Assert.False(Isbn10.IsValid("0306406153"));
    }

    [Theory]
    [InlineData("097522980x")]
    [InlineData("030640615")]
    [InlineData("03064061522")]
    [InlineData("0-306-40615-2")]
    [InlineData("030640615A")]
    [InlineData("X306406152")]
    public void IsValid_ReturnsFalse_WhenValueIsMalformed(string value)
    {
        Assert.False(Isbn10.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsEmpty()
    {
        Assert.False(Isbn10.IsValid(""));
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenValueIsNull()
    {
        Assert.False(Isbn10.IsValid(null));
    }
}

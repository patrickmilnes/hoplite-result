using HopliteLabs.Result.Core;
using HopliteLabs.Result.Test.Unit.Assets;

namespace HopliteLabs.Result.Test.Unit;

public class ResultTests
{
    [Fact]
    public void GivenMethodSuccess_MatchResult_ReturnsTrueIsOk()
    {
        // Arrange
        var expectedMessage = "Hello";
        var result = Result<string, MyError>.Ok(expectedMessage);
        
        // Act
        var output = result.Match(
            value => value,
            err => err.Message);
        
        // Assert
        Assert.True(result.IsOk);
        Assert.Equal(expectedMessage, output);
    }
    
    [Fact]
    public void GivenMethodSuccess_SwitchResult_ReturnsTrueIsOk()
    {
        // Arrange
        var expectedMessage = "Hello";
        var result = Result<string, MyError>.Ok(expectedMessage);
        
        // Act
        var output = result switch
        {
            Result<string, MyError>.OkVariant ok => ok.Value,
            Result<string, MyError>.ErrorVariant err => err.ErrorValue.Message,
            _ => "Unknown result variant"
        };
        
        // Assert
        Assert.True(result.IsOk);
        Assert.Equal(expectedMessage, output);
    }
    
    [Fact]
    public void GivenMethodSuccess_Result_ReturnsTrueIsOk_WithInt()
    {
        // Arrange
        var expectedValue = 42;
        var result = Result<int, MyError>.Ok(expectedValue);
        
        // Act
        var output = result.Match(
            value => value,
            err => -1);
        
        // Assert
        Assert.True(result.IsOk);
        Assert.Equal(expectedValue, output);
    }
    
    

    [Fact]
    public void GivenMethodError_Result_ReturnsFalseIsOk()
    {
        // Arrange
        var expectedErrorMessage = "An error has occurred";
        var result = Result<string, MyError>.Err(new MyError(expectedErrorMessage));
        
        // Act
        var output = result.Match(
            value => value,
            err => err.Message);
        
        // Assert
        Assert.False(result.IsOk);
        Assert.Equal(expectedErrorMessage, output);
    }
    
    [Fact]
    public void GivenMethodSuccess_SwitchResult_ReturnsFalseIsOk()
    {
        // Arrange
        var expectedErrorMessage = "An error has occurred";
        var result = Result<string, MyError>.Err(new MyError(expectedErrorMessage));
        
        // Act
        var output = result switch
        {
            Result<string, MyError>.OkVariant ok => ok.Value,
            Result<string, MyError>.ErrorVariant err => err.ErrorValue.Message,
            _ => "Unknown result variant"
        };
        
        // Assert
        Assert.False(result.IsOk);
        Assert.Equal(expectedErrorMessage, output);
    }
}
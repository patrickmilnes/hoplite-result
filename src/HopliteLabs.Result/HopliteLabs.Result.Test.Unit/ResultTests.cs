using HopliteLabs.Result.Core;
using HopliteLabs.Result.Test.Unit.Assets;

namespace HopliteLabs.Result.Test.Unit;

public class ResultTests
{
    [Fact]
    public void GivenMethodSuccess_Result_ReturnsTrueIsOk()
    {
        // Arrange
        
        // Act
        var result = Result<string, MyError>.Ok("Hello");
        
        // Assert
        Assert.True(result.IsOk);
        Assert.Equal("Hello", result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void GivenMethodError_Result_ReturnsFalseIsOk()
    {
        // Arrange
        
        // Act
        var result = Result<string, MyError>.Err(new MyError("An error has occurred"));
        
        // Assert
        Assert.False(result.IsOk);
        Assert.Equal("An error has occurred", result.Error?.Message);
        Assert.Null(result.Value);
    }
}
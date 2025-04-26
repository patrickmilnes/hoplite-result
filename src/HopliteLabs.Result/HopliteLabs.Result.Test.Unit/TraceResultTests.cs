using HopliteLabs.Result.Core;
using HopliteLabs.Result.Test.Unit.Assets;

namespace HopliteLabs.Result.Test.Unit;

public class TraceResultTests
{
    [Fact]
    public void GivenMethodSuccess_TraceResult_ReturnsTrueIsOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var result = TraceResult<string, MyError>.Ok(id, "Hello");
        
        // Assert
        Assert.True(result.IsOk);
        Assert.Equal("Hello", result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void GivenMethodError_TraceResult_ReturnsFalseIsOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var result = TraceResult<string, MyError>.Err(id, new MyError("An error has occurred"));
        
        // Assert
        Assert.False(result.IsOk);
        Assert.Equal("An error has occurred", result.Error?.Message);
        Assert.Null(result.Value);
    }
}
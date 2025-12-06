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
        var result = TraceResult<string, MyError>.Ok(id, "Hello");

        // Act
        var (traceId, output) = result.Match(
            (traceId, value) => (traceId, value),
            (traceId, err) => (traceId, err.Message));

        // Assert
        Assert.True(result.IsOk);
        Assert.Equal("Hello", output);
        Assert.Equal(id, traceId);
    }

    [Fact]
    public void GivenMethodError_TraceResult_ReturnsFalseIsOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var result = TraceResult<string, MyError>.Err(id, new MyError("An error has occurred"));

        // Act
        var (traceId, output) = result.Match(
            (traceId, value) => (traceId, value),
            (traceId, err) => (traceId, err.Message));

        // Assert
        Assert.False(result.IsOk);
        Assert.Equal("An error has occurred", output);
        Assert.Equal(id, traceId);
    }
}
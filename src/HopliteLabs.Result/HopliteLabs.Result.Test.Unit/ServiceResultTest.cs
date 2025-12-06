using HopliteLabs.Result.Core;
using HopliteLabs.Result.Test.Unit.Assets;

namespace HopliteLabs.Result.Test.Unit;

public class ServiceResultTest
{
    [Fact]
    public void ServiceResult_OkVariant_ShouldHaveStatusCodeAndValue()
    {
        var statusCode = HttpStatusCode.OK;
        var value = "Success";
        var result = ServiceResult<string, MyError>.Ok(value, statusCode);

        var (output, outputStatus) = result.Match(
            (val, status) => (val, status),
            (err, status) => (err.Message, status));

        Assert.True(result.IsOk);
        Assert.Equal(value, output);
        Assert.Equal(statusCode, outputStatus);
    }

    [Fact]
    public void ServiceResult_ErrorVariant_ShouldHaveStatusCodeAndValue()
    {
        var statusCode = HttpStatusCode.NotFound;
        var error = new MyError("Not Found");
        var result = ServiceResult<string, MyError>.Err(error, statusCode);

        var (output, outputStatus) = result.Match(
            (val, status) => (val, status),
            (err, status) => (err.Message, status));

        Assert.False(result.IsOk);
        Assert.Equal(error.Message, output);
        Assert.Equal(statusCode, outputStatus);
    }

    private async Task<ServiceResult<Guid, bool>> GetServiceResultAsync(bool succeed)
    {
        if (succeed) return ServiceResult<Guid, bool>.Ok(Guid.NewGuid(), HttpStatusCode.OK);

        return ServiceResult<Guid, bool>.Err(false, HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task GivenServiceResult_WhenAwaited_ShouldReturnOkVariant()
    {
        var result = await GetServiceResultAsync(true);
        Assert.True(result.IsOk);
    }

    [Fact]
    public async Task GivenServiceResult_WhenAwaited_ShouldReturnErrorVariant()
    {
        var result = await GetServiceResultAsync(false);
        Assert.False(result.IsOk);
    }
}
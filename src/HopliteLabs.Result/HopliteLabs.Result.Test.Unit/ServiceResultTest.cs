using System.Net;
using HopliteLabs.Result.Core;

namespace HopliteLabs.Result.Test.Unit;

public class ServiceResultTest
{
    [Fact]
    public void ServiceResult_OkVariant_ShouldHaveStatusCodeAndValue()
    {
        var statusCode = HttpStatusCode.OK;
        var value = "Success";
        var result = ServiceResult<string, string>.Ok(value, statusCode);

        var output = result.Match(
            val => val,
            err => err);

        Assert.True(result.IsOk);
        Assert.Equal(value, output);
        Assert.Equal(statusCode, result.StatusCode);
    }

    [Fact]
    public void ServiceResult_ErrorVariant_ShouldHaveStatusCodeAndValue()
    {
        var statusCode = HttpStatusCode.NotFound;
        var error = "Not Found";
        var result = ServiceResult<string, string>.Err(error, statusCode);

        var output = result.Match(
            val => val,
            err => err);

        Assert.False(result.IsOk);
        Assert.Equal(error, output);
        Assert.Equal(statusCode, result.StatusCode);
    }
}
using HopliteLabs.Result.Core;

namespace HopliteLabs.Result.Test.Unit.Assets;

public class MyError : IError
{
    public MyError(string message)
    {
        Message = message;
    }

    public string Message { get; }
}
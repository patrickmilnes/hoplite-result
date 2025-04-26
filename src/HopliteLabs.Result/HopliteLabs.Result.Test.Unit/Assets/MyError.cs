using HopliteLabs.Result.Core;

namespace HopliteLabs.Result.Test.Unit.Assets;

public class MyError : IError
{
    public string Message { get; private set; }

    public MyError(string message)
    {
        Message = message;
    }
}
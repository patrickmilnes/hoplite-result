namespace HopliteLabs.Result.Core;

public class Result<T, TS> where TS : IError
{
    public bool IsOk { get; }
    public T? Value { get; }
    public TS? Error { get; }

    protected Result(bool isOk, T? value, TS? error)
    {
        IsOk = isOk;
        Value = value;
        Error = error;
    }

    public static Result<T, TS> Ok(T? value) => new (true, value, default);
    public static Result<T, TS> Err(TS? err) => new (false, default, err);
}

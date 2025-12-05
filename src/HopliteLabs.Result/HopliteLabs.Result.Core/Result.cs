namespace HopliteLabs.Result.Core;

public abstract class Result<TValue, TError>
{

    public static Result<TValue, TError> Ok(TValue value)
    {
        return new ResultOk<TValue, TError>(value);
    }

    public static Result<TValue, TError> Err(TError error)
    {
        return new ResultErr<TValue, TError>(error);
    }

    public abstract bool IsOk { get; }
    public bool IsErr => !IsOk;

    public T Match<T>(Func<TValue, T> onOk, Func<TError, T> onErr)
    {
        return this switch
        {
            ResultOk<TValue, TError> ok => onOk(ok.Value),
            ResultErr<TValue, TError> err => onErr(err.ErrorValue),
            _ => throw new InvalidOperationException("Unknown variant of Result")
        };
    }


    public void Match(Action<TValue> onOk, Action<TError> onErr)
    {
        switch (this)
        {
            case ResultOk<TValue, TError> ok:
                onOk(ok.Value);
                break;
            case ResultErr<TValue, TError> err:
                onErr(err.ErrorValue);
                break;
            default:
                throw new InvalidOperationException("Unknown variant of Result");
        }
    }
}

public sealed class ResultOk<TValue, TError> : Result<TValue, TError>
{
    internal ResultOk(TValue value) : base()
    {
        Value = value;
    }

    public TValue Value { get; }
    public override bool IsOk => true;
}

public sealed class ResultErr<TValue, TError> : Result<TValue, TError>
{
    internal ResultErr(TError error) : base()
    {
        ErrorValue = error;
    }

    public TError ErrorValue { get; }
    public override bool IsOk => false;
}
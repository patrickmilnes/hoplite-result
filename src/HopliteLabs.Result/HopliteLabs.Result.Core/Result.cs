namespace HopliteLabs.Result.Core;

public abstract class Result<TValue, TError>
{
    public abstract bool IsOk { get; }

    public static Result<TValue, TError> Ok(TValue value)
    {
        return new OkVariant(value);
    }

    public static Result<TValue, TError> Err(TError error)
    {
        return new ErrorVariant(error);
    }


    public T Match<T>(Func<TValue, T> onOk, Func<TError, T> onErr)
    {
        return this switch
        {
            OkVariant ok => onOk(ok.Value),
            ErrorVariant err => onErr(err.ErrorValue),
            _ => throw new InvalidOperationException("Unknown variant of Result")
        };
    }

    public class OkVariant : Result<TValue, TError>
    {
        internal OkVariant(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; }
        public override bool IsOk => true;
    }

    public class ErrorVariant : Result<TValue, TError>
    {
        internal ErrorVariant(TError error)
        {
            ErrorValue = error;
        }

        public TError ErrorValue { get; }
        public override bool IsOk => false;
    }
}
namespace HopliteLabs.Result.Core;

public abstract class TraceResult<TValue, TError> : Result<TValue, TError>
{
    public static TraceResult<TValue, TError> Ok(Guid traceId, TValue value)
    {
        return new TraceResultOk<TValue, TError>(traceId, value);
    }

    public static TraceResult<TValue, TError> Err(Guid traceId, TError error)
    {
        return new TraceResultErr<TValue, TError>(traceId, error);
    }

    public abstract Guid TraceId { get; }

    public T Match<T>(Func<Guid, TValue, T> onOk, Func<Guid, TError, T> onErr)
    {
        return this switch
        {
            TraceResultOk<TValue, TError> ok => onOk(ok.TraceId, ok.Value),
            TraceResultErr<TValue, TError> err => onErr(err.TraceId, err.ErrorValue),
            _ => throw new InvalidOperationException("Unknown variant of TraceResult")
        };
    }

    public void Match(Action<Guid, TValue> onOk, Action<Guid, TError> onErr)
    {
        switch (this)
        {
            case TraceResultOk<TValue, TError> ok:
                onOk(ok.TraceId, ok.Value);
                break;
            case TraceResultErr<TValue, TError> err:
                onErr(err.TraceId, err.ErrorValue);
                break;
            default:
                throw new InvalidOperationException("Unknown variant of TraceResult");
        }
    }
}

public sealed class TraceResultOk<TValue, TError> : TraceResult<TValue, TError>
{
    internal TraceResultOk(Guid traceId, TValue value) : base()
    {
        Value = value;
        TraceId = traceId;
    }

    public TValue Value { get; }
    public override Guid TraceId { get; }
    public override bool IsOk => true;
}

public sealed class TraceResultErr<TValue, TError> : TraceResult<TValue, TError>
{
    internal TraceResultErr(Guid traceId, TError error) : base()
    {
        ErrorValue = error;
        TraceId = traceId;
    }

    public TError ErrorValue { get; }
    public override Guid TraceId { get; }
    public override bool IsOk => false;
}
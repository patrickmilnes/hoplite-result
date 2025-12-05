namespace HopliteLabs.Result.Core;

public abstract class TraceResult<TValue, TError> : Result<TValue, TError>
{
    public static TraceResult<TValue, TError> Ok(TValue value, Guid traceId)
    {
        return new TraceResultOk<TValue, TError>(value, traceId);
    }

    public static TraceResult<TValue, TError> Err(TError error, Guid traceId)
    {
        return new TraceResultErr<TValue, TError>(error, traceId);
    }

    public abstract Guid TraceId { get; }

    public new T Match<T>(Func<TValue, Guid, T> onOk, Func<TError, Guid, T> onErr)
    {
        return this switch
        {
            TraceResultOk<TValue, TError> ok => onOk(ok.Value, ok.TraceId),
            TraceResultErr<TValue, TError> err => onErr(err.ErrorValue, err.TraceId),
            _ => throw new InvalidOperationException("Unknown variant of TraceResult")
        };
    }

    public new void Match(Action<TValue, Guid> onOk, Action<TError, Guid> onErr)
    {
        switch (this)
        {
            case TraceResultOk<TValue, TError> ok:
                onOk(ok.Value, ok.TraceId);
                break;
            case TraceResultErr<TValue, TError> err:
                onErr(err.ErrorValue, err.TraceId);
                break;
            default:
                throw new InvalidOperationException("Unknown variant of TraceResult");
        }
    }
}

public sealed class TraceResultOk<TValue, TError> : TraceResult<TValue, TError>
{
    internal TraceResultOk(TValue value, Guid traceId) : base()
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
    internal TraceResultErr(TError error, Guid traceId) : base()
    {
        ErrorValue = error;
        TraceId = traceId;
    }

    public TError ErrorValue { get; }
    public override Guid TraceId { get; }
    public override bool IsOk => false;
}
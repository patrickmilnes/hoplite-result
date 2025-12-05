namespace HopliteLabs.Result.Core;

public abstract class TraceResult<TValue, TError> : Result<TValue, TError>
{
    public static implicit operator TraceResult<TValue, TError>(OkVariant variant) => variant;
    public static implicit operator TraceResult<TValue, TError>(ErrorVariant variant) => variant;

    public static OkVariant Ok(Guid traceId, TValue value)
    {
        return new OkVariant(traceId, value);
    }

    public static ErrorVariant Err(Guid traceId, TError error)
    {
        return new ErrorVariant(traceId, error);
    }

    public new T Match<T>(Func<TValue, Guid, T> onOk, Func<TError, Guid, T> onErr)
    {
        return this switch
        {
            OkVariant ok => onOk(ok.Value, ok.TraceId),
            ErrorVariant err => onErr(err.ErrorValue, err.TraceId),
            _ => throw new InvalidOperationException("Unknow variant of TraceResult")
        };
    }

    public new void Match(Action<TValue, Guid> onOk, Action<TError, Guid> onErr)
    {
        switch (this)
        {
            case OkVariant ok:
                onOk(ok.Value, ok.TraceId);
                break;
            case ErrorVariant err:
                onErr(err.ErrorValue, err.TraceId);
                break;
            default:
                throw new InvalidOperationException("Unknown variant of TraceResult");
        }
    }

    public new class OkVariant : Result<TValue, TError>.OkVariant
    {
        internal OkVariant(Guid traceId, TValue value) : base(value)
        {
            TraceId = traceId;
        }

        public Guid TraceId { get; }
        public override bool IsOk => true;
    }

    public new class ErrorVariant : Result<TValue, TError>.ErrorVariant
    {
        internal ErrorVariant(Guid traceId, TError error) : base(error)
        {
            TraceId = traceId;
        }

        public Guid TraceId { get; }
        public override bool IsOk => false;
    }
}
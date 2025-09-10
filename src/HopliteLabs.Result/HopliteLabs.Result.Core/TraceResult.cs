namespace HopliteLabs.Result.Core;

public class TraceResult<TValue, TError> : Result<TValue, TError>
{
    public override bool IsOk { get; }

    public static OkVariant Ok(Guid traceId, TValue value)
    {
        return new OkVariant(traceId, value);
    }

    public static ErrorVariant Err(Guid traceId, TError error)
    {
        return new ErrorVariant(traceId, error);
    }


    public new class OkVariant : Result<TValue, TError>.OkVariant
    {
        public OkVariant(Guid traceId, TValue value) : base(value)
        {
            TraceId = traceId;
            Value = value;
        }

        public TValue Value { get; }
        public Guid TraceId { get; set; }
        public override bool IsOk => true;
    }

    public new class ErrorVariant : Result<TValue, TError>.ErrorVariant
    {
        public ErrorVariant(Guid traceId, TError error) : base(error)
        {
            TraceId = traceId;
            ErrorValue = error;
        }

        public TError ErrorValue { get; }
        public Guid TraceId { get; set; }
        public override bool IsOk => false;
    }
}
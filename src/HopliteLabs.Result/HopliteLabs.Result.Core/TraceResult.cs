namespace HopliteLabs.Result.Core;

public class TraceResult<T, TS> : Result<T, TS> where TS : IError
{
    public Guid TraceId { get; private set; }

    public TraceResult(bool isOk, Guid traceId, T? value, TS? error) : base(isOk, value, error) => TraceId = traceId;

    public Result<T, TS> ToResult() => this;

    public static TraceResult<T, TS> Ok(Guid traceId, T value) => new (true, traceId, value, default);
    public static TraceResult<T, TS> Err(Guid traceId, TS error) => new (false, traceId, default, error);
}
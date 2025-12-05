using System.Net;

namespace HopliteLabs.Result.Core;

public abstract class ServiceResult<TValue, TError> : Result<TValue, TError>
{
    public static ServiceResult<TValue, TError> Ok(TValue value, HttpStatusCode statusCode)
    {
        return new ServiceResultOk<TValue, TError>(value, statusCode);
    }

    public static ServiceResult<TValue, TError> Err(TError error, HttpStatusCode statusCode)
    {
        return new ServiceResultErr<TValue, TError>(error, statusCode);
    }

    public abstract HttpStatusCode StatusCode { get; }

    public new T Match<T>(Func<TValue, HttpStatusCode, T> onOk, Func<TError, HttpStatusCode, T> onErr)
    {
        return this switch
        {
            ServiceResultOk<TValue, TError> ok => onOk(ok.Value, ok.StatusCode),
            ServiceResultErr<TValue, TError> err => onErr(err.ErrorValue, err.StatusCode),
            _ => throw new InvalidOperationException("Unknown variant of ServiceResult")
        };
    }

    public new void Match(Action<TValue, HttpStatusCode> onOk, Action<TError, HttpStatusCode> onErr)
    {
        switch (this)
        {
            case ServiceResultOk<TValue, TError> ok:
                onOk(ok.Value, ok.StatusCode);
                break;
            case ServiceResultErr<TValue, TError> err:
                onErr(err.ErrorValue, err.StatusCode);
                break;
            default:
                throw new InvalidOperationException("Unknown variant of ServiceResult");
        }
    }
}

public sealed class ServiceResultOk<TValue, TError> : ServiceResult<TValue, TError>
{
    internal ServiceResultOk(TValue value, HttpStatusCode statusCode) : base()
    {
        Value = value;
        StatusCode = statusCode;
    }

    public TValue Value { get; }
    public override HttpStatusCode StatusCode { get; }
    public override bool IsOk => true;
}

public sealed class ServiceResultErr<TValue, TError> : ServiceResult<TValue, TError>
{
    internal ServiceResultErr(TError error, HttpStatusCode statusCode) : base()
    {
        ErrorValue = error;
        StatusCode = statusCode;
    }

    public TError ErrorValue { get; }
    public override HttpStatusCode StatusCode { get; }
    public override bool IsOk => false;
}
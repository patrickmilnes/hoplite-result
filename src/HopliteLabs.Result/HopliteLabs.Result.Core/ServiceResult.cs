using System.Net;

namespace HopliteLabs.Result.Core;

public abstract class ServiceResult<TValue, TError> : Result<TValue, TError>
{
    public static implicit operator ServiceResult<TValue, TError>(OkVariant variant) => variant;
    public static implicit operator ServiceResult<TValue, TError>(ErrorVariant variant) => variant;

    public static OkVariant Ok(TValue value, HttpStatusCode statusCode)
    {
        return new OkVariant(value, statusCode);
    }

    public static ErrorVariant Err(TError error, HttpStatusCode statusCode)
    {
        return new ErrorVariant(error, statusCode);
    }

    public new T Match<T>(Func<TValue, HttpStatusCode, T> onOk, Func<TError, HttpStatusCode, T> onErr)
    {
        return this switch
        {
            OkVariant ok => onOk(ok.Value, ok.StatusCode),
            ErrorVariant err => onErr(err.ErrorValue, err.StatusCode),
            _ => throw new InvalidOperationException("Unknow variant of ServiceResult")
        };
    }

    public new void Match(Action<TValue, HttpStatusCode> onOk, Action<TError, HttpStatusCode> onErr)
    {
        switch (this)
        {
            case OkVariant ok:
                onOk(ok.Value, ok.StatusCode);
                break;
            case ErrorVariant err:
                onErr(err.ErrorValue, err.StatusCode);
                break;
            default:
                throw new InvalidOperationException("Unknown variant of ServiceResult");
        }
    }

    public new class OkVariant : Result<TValue, TError>.OkVariant
    {
        internal OkVariant(TValue value, HttpStatusCode statusCode) : base(value)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
        public override bool IsOk => true;
    }

    public new class ErrorVariant : Result<TValue, TError>.ErrorVariant
    {
        internal ErrorVariant(TError error, HttpStatusCode statusCode) : base(error)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
        public override bool IsOk => false;
    }
}
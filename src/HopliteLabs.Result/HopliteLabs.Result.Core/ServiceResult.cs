using System.Net;

namespace HopliteLabs.Result.Core;

public class ServiceResult<TValue, TError> : Result<TValue, TError>
{
    private readonly object _variant;

    private ServiceResult(object variant, bool isOk)
    {
        _variant = variant;
        IsOk = isOk;
    }
    public override bool IsOk { get; }

    public static implicit operator ServiceResult<TValue, TError>(OkVariant ok) => new ServiceResult<TValue, TError>(ok, true);
    public static implicit operator ServiceResult<TValue, TError>(ErrorVariant err) => new ServiceResult<TValue, TError>(err, false);

    public static OkVariant Ok(TValue value, HttpStatusCode statusCode)
    {
        return new OkVariant(value, statusCode);
    }

    public static ErrorVariant Err(TError error, HttpStatusCode statusCode)
    {
        return new ErrorVariant(error, statusCode);
    }

    public new class OkVariant : Result<TValue, TError>.OkVariant
    {
        internal OkVariant(TValue value, HttpStatusCode statusCode) : base(value)
        {
            Value = value;
            StatusCode = statusCode;
        }

        public TValue Value { get; }
        public HttpStatusCode StatusCode { get; set; }
        public override bool IsOk => true;
    }

    public new class ErrorVariant : Result<TValue, TError>.ErrorVariant
    {
        internal ErrorVariant(TError error, HttpStatusCode statusCode) : base(error)
        {
            ErrorValue = error;
            StatusCode = statusCode;
        }

        public TError ErrorValue { get; }
        public HttpStatusCode StatusCode { get; set; }
        public override bool IsOk => false;
    }
}
using System.Net;

namespace APITesting.Client.Result
{
    public readonly partial struct ApiResult : IEquatable<ApiResult>
    {
        private const string SuccessString = "Success";
        private static readonly ApiError NullDeserializationResult = new("Null deserialization result", default);
        public ApiError Error { get; }
        public HttpStatusCode StatusCode => this.Error.StatusCode;
        public bool IsSuccess => this.StatusCode.IsSuccessfulStatusCode();

        private ApiResult(ApiError error)
    {
        this.Error = error;
    }

        public void ThrowIfFailure()
    {
        if (this.IsSuccess is false)
            throw this.Error.CreateException();
    }
    
        public static ApiError Fail(ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        ThrowHelper.ThrowIfSuccessfulStatusCode(statusCode);
        return new(problemDetails, statusCode);
    }

        public static ApiError Fail(Exception exception, HttpStatusCode statusCode)
    {
        ThrowHelper.ThrowIfSuccessfulStatusCode(statusCode);
        return new(exception, statusCode);
    }

        public static ApiError Fail(string message, HttpStatusCode statusCode)
    {
        ThrowHelper.ThrowIfSuccessfulStatusCode(statusCode);
        return new(message, statusCode);
    }
        public static ApiError Fail(HttpStatusCode statusCode)
    {
        ThrowHelper.ThrowIfSuccessfulStatusCode(statusCode);
        return new(statusCode, null);
    }

        public static ApiResult Success(HttpStatusCode statusCode)
    {
        ThrowHelper.ThrowIfUnsuccessfulStatusCode(statusCode);
        return new ApiError(statusCode, null);
    }
        public static ApiResult<T> Success<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK) where T : notnull
    {
        ThrowHelper.ThrowIfUnsuccessfulStatusCode(statusCode);
        return new(value, statusCode);
    }

        public static implicit operator ApiResult(ApiError error) => new(error);


        public override string ToString() => this.IsSuccess ? SuccessString : this.Error.ToString();

        public bool Equals(ApiResult other, bool ignoreStatusCode) => (IgnoreStatusCode: ignoreStatusCode, this.IsSuccess) switch
        {
            (IgnoreStatusCode: true, IsSuccess: true) => other.IsSuccess,
            (IgnoreStatusCode: false, IsSuccess: true) => other.IsSuccess && this.StatusCode == other.StatusCode,
            (IgnoreStatusCode: _, IsSuccess: false) => this.Error.Equals(other.Error, ignoreStatusCode)
        };

        public bool Equals(ApiResult other)
            => this.Error.Equals(other.Error, ignoreStatusCode: false);
        public bool Equals(HttpStatusCode other)
            => this.StatusCode == other;
        public bool Equals(Exception? other)
            => this.Error.Exception == other;
        public bool Equals(ProblemDetails? other)
            => this.Error.ProblemDetails == other;

        public override bool Equals(object? obj) => obj switch
        {
            HttpStatusCode other => this.Equals(other),
            ApiResult other => this.Equals(other),
            Exception other => this.Equals(other),
            ProblemDetails other => this.Equals(other),
            _ => false,
        };

        public override int GetHashCode() => HashCode.Combine(this.Error, this.StatusCode);
        public static bool operator ==(ApiResult left, ApiResult right) => left.Equals(right);
        public static bool operator !=(ApiResult left, ApiResult right) => !left.Equals(right);

    }
}



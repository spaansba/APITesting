using System.Diagnostics;
using System.Net;

namespace APITesting.Client.Result
{
    public readonly struct ApiError : IEquatable<ApiError>
    {
        private const string DefaultErrorMessage = "An error occurred";
        internal ApiError(HttpStatusCode statusCode, object? data)
    {
        Debug.Assert(data is null or Result.ProblemDetails or System.Exception or string);
        this.data = data;
        this.StatusCode = statusCode;
    }
    
        public Exception CreateException() => this.data switch
        {
            Exception ex => ex,
            _ => new HttpRequestException(this.Message, this.Exception, this.StatusCode),
        };
    
 
        public ApiError(ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        ArgumentNullException.ThrowIfNull(problemDetails);
        ThrowHelper.ThrowIfSuccessfulStatusCode(statusCode);
        this.data = problemDetails;
        this.StatusCode = statusCode;
    }
 
        public ApiError(Exception exception, HttpStatusCode statusCode)
    {
        ArgumentNullException.ThrowIfNull(exception);
        ThrowHelper.ThrowIfSuccessfulStatusCode(statusCode);
        this.data = exception;
        this.StatusCode = statusCode;
    }
        public ApiError(string message, HttpStatusCode statusCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        ThrowHelper.ThrowIfSuccessfulStatusCode(statusCode);
        this.data = message;
        this.StatusCode = statusCode;
    }
 
 
        private readonly object? data;
 
        public HttpStatusCode StatusCode { get; }
        public ProblemDetails? ProblemDetails => this.data as ProblemDetails;
        public Exception? Exception => this.data as Exception;
        public string Message => (HasStatusCode: this.StatusCode != default, Data: this.data) switch
        {
            (HasStatusCode: false, Data: string message) => message,
            (HasStatusCode: false, Data: Exception exception) => exception.Message,
            (HasStatusCode: false, Data: ProblemDetails problemDetails) => problemDetails.Title ?? problemDetails.Detail ?? DefaultErrorMessage,
            (HasStatusCode: false, Data: _) => DefaultErrorMessage,
            (HasStatusCode: true, Data: string message) => $"HTTP {(int)this.StatusCode} ({this.StatusCode.GetReasonPhrase()}){Environment.NewLine}{message}",
            (HasStatusCode: true, Data: Exception exception) => $"HTTP {(int)this.StatusCode} ({this.StatusCode.GetReasonPhrase()}){Environment.NewLine}{exception.Message}",
            (HasStatusCode: true, Data: ProblemDetails problemDetails) =>$"HTTP {(int)this.StatusCode} ({this.StatusCode.GetReasonPhrase()}){Environment.NewLine}{problemDetails.Title}",
            (HasStatusCode: true, Data: _) => $"HTTP {(int)this.StatusCode} ({this.StatusCode.GetReasonPhrase()})",
        };
 
        public override string ToString() => (HasStatusCode: this.StatusCode != default, Data: this.data) switch
        {
            (HasStatusCode: false, Data: string message) => message,
            (HasStatusCode: false, Data: Exception exception) => exception.ToString(), // TODO: Are we okay with Exception.ToString()
            (HasStatusCode: false, Data: ProblemDetails problemDetails) => problemDetails.Title ?? problemDetails.Detail ?? DefaultErrorMessage,
            (HasStatusCode: false, Data: _) => DefaultErrorMessage,
            (HasStatusCode: true, Data: string message) => $"HTTP {(int)this.StatusCode} ({this.StatusCode.GetReasonPhrase()}){Environment.NewLine}{message}",
            (HasStatusCode: true, Data: Exception exception) => $"HTTP {(int)this.StatusCode} ({this.StatusCode.GetReasonPhrase()}){Environment.NewLine}{exception}",// TODO: Are we okay with Exception.ToString()
            (HasStatusCode: true, Data: ProblemDetails problemDetails) =>$"HTTP {(int)this.StatusCode} ({this.StatusCode.GetReasonPhrase()}){Environment.NewLine}{problemDetails.Title}",
            (HasStatusCode: true, Data: _) => $"HTTP {(int)this.StatusCode} ({this.StatusCode.GetReasonPhrase()})",
        };
 
        public bool Equals(ApiError other, bool ignoreStatusCode) => (ignoreStatusCode is false || this.StatusCode == other.StatusCode) && Equals(this.data, other.data);
        public bool Equals(ApiError other) => this.StatusCode == other.StatusCode && Equals(this.data, other.data);
        public bool Equals(HttpStatusCode other) => this.StatusCode == other;
        public bool Equals(ProblemDetails? other) => other is null ? this.StatusCode.IsSuccessfulStatusCode() : this.ProblemDetails == other;
        public bool Equals(Exception? other) => other is null ? this.StatusCode.IsSuccessfulStatusCode() : this.Exception == other;
 
        public override bool Equals(object? obj) => obj switch
        {
            ApiError other => this.Equals(other),
            HttpStatusCode other => this.Equals(other),
            ProblemDetails other => this.Equals(other),
            Exception other => this.Equals(other),
            _ => false,
        };
        public override int GetHashCode() => HashCode.Combine(this.data, this.StatusCode);
        public static bool operator ==(ApiError left, ApiError right) => left.Equals(right);
        public static bool operator !=(ApiError left, ApiError right) => !left.Equals(right);
    }
}
 
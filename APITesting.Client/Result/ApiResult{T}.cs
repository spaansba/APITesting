using System.Diagnostics.CodeAnalysis;
using System.Net;
using APITesting.Client.Result;

namespace APITesting.Client;

public readonly struct ApiResult<T> : IEquatable<ApiResult<T>>
    where T : notnull
{
    public ApiError Error { get; }
    public T? Value { get; }

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess => this.StatusCode.IsSuccessfulStatusCode();

    public HttpStatusCode StatusCode => this.Error.StatusCode;


    private ApiResult(T value)
    {
        ThrowHelper.ThrowIfNull(value);
        this.Value = value;
        this.Error = new (HttpStatusCode.OK, null);
    }
    public ApiResult(T value, HttpStatusCode statusCode)
    {
        ThrowHelper.ThrowIfNull(value);
        ThrowHelper.ThrowIfUnsuccessfulStatusCode(statusCode);
        this.Value = value;
        this.Error = new (statusCode, null);
    }

    public ApiResult(ApiError error)
    {
        this.Error = error;
    }

    public static implicit operator ApiResult<T>(ApiError error) => new(error);
    public static implicit operator ApiResult<T>(T value) => new(value);
    public override string? ToString() => this.IsSuccess ? this.Value.ToString() : this.Error.ToString();

    public bool Equals(ApiResult<T> other, bool ignoreStatusCode) => (this.IsSuccess, IgnoreStatusCode: ignoreStatusCode) switch
    {
        (IsSuccess: true, IgnoreStatusCode: true) => other.IsSuccess && EqualityComparer<T>.Default.Equals(this.Value, other.Value),
        (IsSuccess: true, IgnoreStatusCode: false) => this.StatusCode == other.StatusCode && EqualityComparer<T>.Default.Equals(this.Value, other.Value),
        (IsSuccess: false, IgnoreStatusCode: _) => this.Error.Equals(other.Error, ignoreStatusCode),
    };

    public bool Equals(ApiResult<T> other) => Equals(other, ignoreStatusCode: false);


    public bool Equals(T? other) => EqualityComparer<T>.Default.Equals(this.Value, other);
    public bool Equals(ApiError other, bool ignoreStatusCode) => this.Error.Equals(other, ignoreStatusCode: ignoreStatusCode);
    public bool Equals(ApiError other) => this.Error.Equals(other, ignoreStatusCode: false);
    public bool Equals(Exception? other) => other is null ? this.IsSuccess : this.Error.Equals(other);

    public bool Equals(ProblemDetails? other) => other is null ? this.IsSuccess : this.Error.Equals(other);

    public override bool Equals(object? obj) => obj switch
    {
        ApiResult<T> other => this.Equals(other),
        T other => this.Equals(other),
        Exception other => this.Equals(other),
        ProblemDetails other => this.Equals(other),
        _ => false,
    };

    public override int GetHashCode() => HashCode.Combine(this.Error, this.Value);
    public static bool operator ==(ApiResult<T> left, ApiResult<T> right) => left.Equals(right);
    public static bool operator !=(ApiResult<T> left, ApiResult<T> right) => !left.Equals(right);

    public bool TryGetValue([NotNullWhen(true)] out T? value, out ApiError error)
    {
        value = this.Value;
        error = this.Error;
        return value is not null && this.IsSuccess;
    }
}
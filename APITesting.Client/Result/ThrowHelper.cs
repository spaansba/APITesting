using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;

namespace APITesting.Client.Result;

internal static class ThrowHelper
{
    public static void ThrowIfNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string argumentName = "")
    {
        if (value is null)
            ThrowArgumentNullException(argumentName);
    }
 
    [DoesNotReturn]
    public static void ThrowArgumentNullException(string argumentName)
    {
        throw new ArgumentNullException(argumentName);
    }
 
    public static void ThrowIfSuccessfulStatusCode(HttpStatusCode statusCode, [CallerArgumentExpression(nameof(statusCode))] string argumentName = "")
    {
        if (statusCode.IsSuccessfulStatusCode())
            ThrowSuccessfulStatusCode(statusCode, argumentName);
    }
    public static void ThrowIfUnsuccessfulStatusCode(HttpStatusCode statusCode, [CallerArgumentExpression(nameof(statusCode))] string argumentName = "")
    {
        if (statusCode.IsSuccessfulStatusCode() is false)
            ThrowUnsuccessfulStatusCode(statusCode, argumentName);
    }
    private static void ThrowSuccessfulStatusCode(HttpStatusCode statusCode, string argumentName)
    {
        throw new ArgumentOutOfRangeException(argumentName, statusCode, $"Expected unsuccessful status code; received {statusCode}");
    }
    private static void ThrowUnsuccessfulStatusCode(HttpStatusCode statusCode, string argumentName)
    {
        throw new ArgumentOutOfRangeException(argumentName, statusCode, $"Expected successful status code; received {statusCode}");
    }
}
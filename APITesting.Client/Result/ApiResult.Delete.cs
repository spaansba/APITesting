using System.Text.Json;

namespace APITesting.Client.Result;

internal static partial class ApiResultHttpClientExtensions
{
    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, jsonSerializerOptions, completionOption, cancellationToken);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, jsonSerializerOptions, cancellationToken);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, jsonSerializerOptions, completionOption);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, jsonSerializerOptions);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, completionOption, cancellationToken);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, string? requestUri, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, cancellationToken);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, completionOption);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, string? requestUri)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request);
    }
    
    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, jsonSerializerOptions, completionOption, cancellationToken);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, jsonSerializerOptions, cancellationToken);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, jsonSerializerOptions, completionOption);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, jsonSerializerOptions);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, completionOption, cancellationToken);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, Uri? requestUri, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, cancellationToken);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request, completionOption);
    }

    public static async Task<ApiResult> DeleteApiResultAsync(this HttpClient httpClient, Uri? requestUri)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendApiResultAsync(request);
    }
}

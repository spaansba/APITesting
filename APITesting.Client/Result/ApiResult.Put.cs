using System.Net.Http.Json;
using System.Text.Json;

namespace APITesting.Client;

internal static partial class ApiResultHttpClientExtensions
{
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content: null, cancellationToken), jsonSerializerOptions, cancellationToken);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, string? requestUri, CancellationToken cancellationToken)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content: null, cancellationToken), null, cancellationToken);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content: null, cancellationToken), jsonSerializerOptions, cancellationToken);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, Uri? requestUri, CancellationToken cancellationToken)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content: null, cancellationToken), null, cancellationToken);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content: null, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, string? requestUri)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content: null, CancellationToken.None), null, CancellationToken.None);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content: null, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, Uri? requestUri)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content: null, CancellationToken.None), null, CancellationToken.None);



    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, string? requestUri, HttpContent? content, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content, cancellationToken), jsonSerializerOptions, cancellationToken);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, string? requestUri, HttpContent? content, CancellationToken cancellationToken)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content, cancellationToken), null, cancellationToken);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, Uri? requestUri, HttpContent? content, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content, cancellationToken), jsonSerializerOptions, cancellationToken);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, Uri? requestUri, HttpContent? content, CancellationToken cancellationToken)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content, cancellationToken), null, cancellationToken);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, string? requestUri, HttpContent? content, JsonSerializerOptions? jsonSerializerOptions)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, string? requestUri, HttpContent? content)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content, CancellationToken.None), null, CancellationToken.None);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, Uri? requestUri, HttpContent? content, JsonSerializerOptions? jsonSerializerOptions)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TResponse>(this HttpClient httpClient, Uri? requestUri, HttpContent? content)
        where TResponse : notnull
        => await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, content, CancellationToken.None), null, CancellationToken.None);



    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TContent, TResponse>(this HttpClient httpClient, string? requestUri, TContent content, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
        where TResponse : notnull
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, jsonSerializerOptions);
        return await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, httpContent, cancellationToken), jsonSerializerOptions, cancellationToken);
    }
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TContent, TResponse>(this HttpClient httpClient, string? requestUri, TContent content, CancellationToken cancellationToken)
        where TResponse : notnull
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, options: null);
        return await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, httpContent, cancellationToken), jsonOptions: null, cancellationToken);
    }
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TContent, TResponse>(this HttpClient httpClient, Uri? requestUri, TContent content, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
        where TResponse : notnull
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, jsonSerializerOptions);
        return await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, httpContent, cancellationToken), jsonSerializerOptions, cancellationToken);
    }
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TContent, TResponse>(this HttpClient httpClient, Uri? requestUri, TContent content, CancellationToken cancellationToken)
        where TResponse : notnull
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, options: null);
        return await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, httpContent, cancellationToken), jsonOptions: null, cancellationToken);
    }
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TContent, TResponse>(this HttpClient httpClient, string? requestUri, TContent content, JsonSerializerOptions? jsonSerializerOptions)
        where TResponse : notnull
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, jsonSerializerOptions);
        return await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, httpContent, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    }
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TContent, TResponse>(this HttpClient httpClient, string? requestUri, TContent content)
        where TResponse : notnull
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, options: null);
        return await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, httpContent, CancellationToken.None), jsonOptions: null, CancellationToken.None);
    }
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TContent, TResponse>(this HttpClient httpClient, Uri? requestUri, TContent content, JsonSerializerOptions? jsonSerializerOptions)
        where TResponse : notnull
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, jsonSerializerOptions);
        return await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, httpContent, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    }
    public static async Task<ApiResult<TResponse>> PutApiResultAsync<TContent, TResponse>(this HttpClient httpClient, Uri? requestUri, TContent content)
        where TResponse : notnull
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, options: null);
        return await ApiResult.CreateAsync<TResponse>(await httpClient.PutAsync(requestUri, httpContent, CancellationToken.None), jsonOptions: null, CancellationToken.None);
    }


    public static async Task<ApiResult> PutApiResultAsync<TContent>(this HttpClient httpClient, string? requestUri, TContent content, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, jsonSerializerOptions);
        return await ApiResult.CreateAsync(await httpClient.PutAsync(requestUri, httpContent, cancellationToken), jsonSerializerOptions, cancellationToken);
    }

    public static async Task<ApiResult> PutApiResultAsync<TContent>(this HttpClient httpClient, string? requestUri, TContent content, CancellationToken cancellationToken)
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, options: null);
        return await ApiResult.CreateAsync(await httpClient.PutAsync(requestUri, httpContent, cancellationToken), jsonOptions: null, cancellationToken);
    }
    public static async Task<ApiResult> PutApiResultAsync<TContent>(this HttpClient httpClient, Uri? requestUri, TContent content, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, jsonSerializerOptions);
        return await ApiResult.CreateAsync(await httpClient.PutAsync(requestUri, httpContent, cancellationToken), jsonSerializerOptions, cancellationToken);
    }
    public static async Task<ApiResult> PutApiResultAsync<TContent>(this HttpClient httpClient, Uri? requestUri, TContent content, CancellationToken cancellationToken)
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, options: null);
        return await ApiResult.CreateAsync(await httpClient.PutAsync(requestUri, httpContent, cancellationToken), jsonOptions: null, cancellationToken);
    }
    public static async Task<ApiResult> PutApiResultAsync<TContent>(this HttpClient httpClient, string? requestUri, TContent content, JsonSerializerOptions? jsonSerializerOptions)
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, jsonSerializerOptions);
        return await ApiResult.CreateAsync(await httpClient.PutAsync(requestUri, httpContent, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    }
    public static async Task<ApiResult> PutApiResultAsync<TContent>(this HttpClient httpClient, string? requestUri, TContent content)
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, options: null);
        return await ApiResult.CreateAsync(await httpClient.PutAsync(requestUri, httpContent, CancellationToken.None), jsonOptions: null, CancellationToken.None);
    }
    public static async Task<ApiResult> PutApiResultAsync<TContent>(this HttpClient httpClient, Uri? requestUri, TContent content, JsonSerializerOptions? jsonSerializerOptions)
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, jsonSerializerOptions);
        return await ApiResult.CreateAsync(await httpClient.PutAsync(requestUri, httpContent, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    }
    public static async Task<ApiResult> PutApiResultAsync<TContent>(this HttpClient httpClient, Uri? requestUri, TContent content)
    {
        using var httpContent = JsonContent.Create(content, mediaType: null, options: null);
        return await ApiResult.CreateAsync(await httpClient.PutAsync(requestUri, httpContent, CancellationToken.None), jsonOptions: null, CancellationToken.None);
    }
}

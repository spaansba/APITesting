using System.Net.Http.Json;

namespace APITesting.Client;

internal static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, Uri? requestUri, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendAsync(request, cancellationToken);
    }
    public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string? requestUri, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        return await httpClient.SendAsync(request, cancellationToken);
    }
    
    public static async Task<TResponse?> PatchFromJsonAsync<TRequest, TResponse>(this HttpClient httpClient, string? requestUri, TRequest requestBody, CancellationToken cancellationToken)
    {
        using var content = JsonContent.Create(requestBody);
        using var response = await httpClient.PatchAsync(requestUri, content, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);
        return result;
    }
    
    public static async Task<TResponse?> PutFromJsonAsync<TRequest, TResponse>(this HttpClient httpClient, string? requestUri, TRequest requestBody, CancellationToken cancellationToken)
    {
        using var content = JsonContent.Create(requestBody);
        using var response = await httpClient.PutAsync(requestUri, content, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);
        return result;
    }
    
}

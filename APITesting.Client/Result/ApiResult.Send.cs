using System.Text.Json;

namespace APITesting.Client.Result;

internal static partial class ApiResultHttpClientExtensions
{
    public static async Task<ApiResult> SendApiResultAsync(this HttpClient httpClient, HttpRequestMessage request, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        => await ApiResult.CreateAsync(await httpClient.SendAsync(request, completionOption, cancellationToken), jsonSerializerOptions, cancellationToken);
    public static async Task<ApiResult> SendApiResultAsync(this HttpClient httpClient, HttpRequestMessage request, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
        => await ApiResult.CreateAsync(await httpClient.SendAsync(request, default, cancellationToken), jsonSerializerOptions, cancellationToken);
    public static async Task<ApiResult> SendApiResultAsync(this HttpClient httpClient, HttpRequestMessage request, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption)
        => await ApiResult.CreateAsync(await httpClient.SendAsync(request, completionOption, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);
    public static async Task<ApiResult> SendApiResultAsync(this HttpClient httpClient, HttpRequestMessage request, JsonSerializerOptions? jsonSerializerOptions)
        => await ApiResult.CreateAsync(await httpClient.SendAsync(request, default, CancellationToken.None), jsonSerializerOptions, CancellationToken.None);

    public static async Task<ApiResult> SendApiResultAsync(this HttpClient httpClient, HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        => await ApiResult.CreateAsync(await httpClient.SendAsync(request, completionOption, cancellationToken), jsonOptions: null, cancellationToken);
    public static async Task<ApiResult> SendApiResultAsync(this HttpClient httpClient, HttpRequestMessage request, CancellationToken cancellationToken)
        => await ApiResult.CreateAsync(await httpClient.SendAsync(request, default, cancellationToken), jsonOptions: null, cancellationToken);
    public static async Task<ApiResult> SendApiResultAsync(this HttpClient httpClient, HttpRequestMessage request, HttpCompletionOption completionOption)
        => await ApiResult.CreateAsync(await httpClient.SendAsync(request, completionOption, CancellationToken.None), jsonOptions: null, CancellationToken.None);
    public static async Task<ApiResult> SendApiResultAsync(this HttpClient httpClient, HttpRequestMessage request)
        => await ApiResult.CreateAsync(await httpClient.SendAsync(request, default, CancellationToken.None), jsonOptions: null, CancellationToken.None);
}

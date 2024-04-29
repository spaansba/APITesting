using System.Text.Json;

namespace APITesting.Client.Result
{
    internal static partial class ApiResultHttpClientExtensions
    {
        public static async Task<ApiResult> GetApiResultAsync(this HttpClient httpClient, string? requestUri)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri), null, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, Uri? requestUri)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri), null, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, completionOption), null, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, string? requestUri, CancellationToken cancellationToken)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, cancellationToken), null, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, completionOption), null, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, Uri? requestUri, CancellationToken cancellationToken)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, cancellationToken), null, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, completionOption, cancellationToken), null, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, completionOption, cancellationToken), null, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync(this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri), jsonSerializerOptions, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri), jsonSerializerOptions, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, completionOption), jsonSerializerOptions, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, cancellationToken), jsonSerializerOptions, cancellationToken);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, completionOption), jsonSerializerOptions, CancellationToken.None);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, cancellationToken), jsonSerializerOptions, cancellationToken);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, completionOption, cancellationToken), jsonSerializerOptions, cancellationToken);
        public static async Task<ApiResult> GetApiResultAsync (this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            => await ApiResult.CreateAsync(await httpClient.GetAsync(requestUri, completionOption, cancellationToken), jsonSerializerOptions, cancellationToken);

        public static async Task<ApiResult<T>> GetApiResultAsync<T>(this HttpClient httpClient, string? requestUri)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri), null, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, Uri? requestUri)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri), null, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, completionOption), null, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, string? requestUri, CancellationToken cancellationToken)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, cancellationToken), null, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, completionOption), null, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, Uri? requestUri, CancellationToken cancellationToken)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, cancellationToken), null, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, completionOption, cancellationToken), null, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, completionOption, cancellationToken), null, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T>(this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri), jsonSerializerOptions, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri), jsonSerializerOptions, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, completionOption), jsonSerializerOptions, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, cancellationToken), jsonSerializerOptions, cancellationToken);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, completionOption), jsonSerializerOptions, CancellationToken.None);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, CancellationToken cancellationToken)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, cancellationToken), jsonSerializerOptions, cancellationToken);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, string? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, completionOption, cancellationToken), jsonSerializerOptions, cancellationToken);
        public static async Task<ApiResult<T>> GetApiResultAsync<T> (this HttpClient httpClient, Uri? requestUri, JsonSerializerOptions? jsonSerializerOptions, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            where T : notnull
            => await ApiResult.CreateAsync<T>(await httpClient.GetAsync(requestUri, completionOption, cancellationToken), jsonSerializerOptions, cancellationToken);
    }
}

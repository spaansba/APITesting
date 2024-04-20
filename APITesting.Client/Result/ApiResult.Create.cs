using System.Net.Http.Json;
using System.Text.Json;
using APITesting.Client.Result;

namespace APITesting.Client;

public readonly partial struct ApiResult
{

    public static async Task<ApiResult<T>> CreateAsync<T>(
        HttpResponseMessage response,
        JsonSerializerOptions? jsonOptions,
        CancellationToken cancellationToken
    ) where T : notnull
    {
        try
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>(jsonOptions, cancellationToken) is { } result
                    ? Success(result)
                    : NullDeserializationResult;
            }
            return await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions, cancellationToken) is { } problem
                ? Fail(problem, response.StatusCode)
                :  Fail(response.StatusCode);
        }
        catch (Exception ex)
        {
            return Fail(ex, response.StatusCode);
        }
    }

    public static async Task<ApiResult> CreateAsync(
        HttpResponseMessage response,
        JsonSerializerOptions? jsonOptions,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (response.IsSuccessStatusCode)
                return Success(response.StatusCode);
            return await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions, cancellationToken) is { } problem
                ? Fail(problem, response.StatusCode)
                :  Fail(response.StatusCode);
        }
        catch (Exception ex)
        {
            return Fail(ex, response.StatusCode);
        }
    }
}

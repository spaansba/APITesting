using System.Net.Http.Json;
using APITesting.Client.Result;
using Microsoft.Extensions.DependencyInjection;

namespace APITesting.Client;

public interface ITestClient
{
    public Task<ApiResult<IEnumerable<UserProfileResponse>>> GetAllUsers(CancellationToken cancellationToken = default);
    public Task<ApiResult<UserProfileResponse>> GetUser(int id, CancellationToken cancellationToken = default);
    public Task<ApiResult<UserProfileResponse>> UpdateUser(int id, UserProfileUpdateRequest request, CancellationToken cancellationToken = default);
    public Task<ApiResult<UserProfileResponse>> CreateUser(UserProfileCreateRequest request, CancellationToken cancellationToken = default);
    public Task<ApiResult> DeleteUser(int id, CancellationToken cancellationToken = default);
}

public class TestClient : ITestClient
{
    /// <summary>
    /// Warning: Never Dispose the HTTPClient
    /// </summary>
    private readonly HttpClient client;
    public const string HttpClientName = "TestClient"; 
    public TestClient(HttpClient client)
    {
        this.client = client;
    }
    
    [ActivatorUtilitiesConstructor] //DI frameworks will use this ctor
    public TestClient(IHttpClientFactory clientFactory)
        : this(clientFactory.CreateClient(HttpClientName))
    {

    }

    public TestClient(IHttpClientFactory clientFactory, Func<IHttpClientFactory, HttpClient> createClient)
        : this(createClient(clientFactory))
    {

    }
    
    /// <summary>
    /// Calling API endpoints
    /// </summary>
    
    public async Task<ApiResult<IEnumerable<UserProfileResponse>>> GetAllUsers(CancellationToken cancellationToken = default)
        => await this.client.GetApiResultAsync<IEnumerable<UserProfileResponse>>($"api/users", cancellationToken: cancellationToken);
    public async Task<ApiResult<UserProfileResponse>> GetUser(int id, CancellationToken cancellationToken = default)
        => await this.client.GetApiResultAsync<UserProfileResponse>($"api/user/{id}", cancellationToken: cancellationToken);
    // Patch
    public async Task<ApiResult<UserProfileResponse>> UpdateUser(int id, UserProfileUpdateRequest request, CancellationToken cancellationToken = default)
        => await this.client.PatchApiResultAsync<UserProfileUpdateRequest, UserProfileResponse>($"api/user/{id}", request, cancellationToken: cancellationToken);
    //Put
    public async Task<ApiResult<UserProfileResponse>> CreateUser(UserProfileCreateRequest request, CancellationToken cancellationToken = default)
        => await this.client.PutApiResultAsync<UserProfileCreateRequest, UserProfileResponse>($"api/user", request, cancellationToken);
    // Delete
    public async Task<ApiResult> DeleteUser(int id, CancellationToken cancellationToken = default)
        => await this.client.DeleteApiResultAsync($"api/user/{id}", cancellationToken: cancellationToken);
}
using APITesting.Client.Result;
using APITesting.Contracts;

namespace APITesting.Client
{
    public interface ITestClient
    {
        public Task<ApiResult<IEnumerable<UserProfileResponse>>> GetAllUsers(CancellationToken cancellationToken = default);

        public Task<ApiResult<UserProfileResponse>> GetUser(long id, CancellationToken cancellationToken = default);
        public Task<ApiResult<UserProfileResponse>> UpdateUser(long id, UserProfileUpdateRequest request, CancellationToken cancellationToken = default);
        public Task<ApiResult<UserProfileResponse>> CreateUser(UserProfileCreateRequest request, CancellationToken cancellationToken = default);
        public Task<ApiResult> DeleteUser(long id, CancellationToken cancellationToken = default);

        //Methods to make messaging simpler
        //the same as the other one, except it's static, and accepts the client as a parameter.  It just redirects.
        public static Task<ApiResult<IEnumerable<UserProfileResponse>>> GetAllUsers(ITestClient client) => client.GetAllUsers(CancellationToken.None);
        public static Task<ApiResult<UserProfileResponse>> GetUser(ITestClient client, long id) =>
            client.GetUser(id, CancellationToken.None);
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
    
        /// <summary>
        /// Calling API endpoints
        /// </summary>
    
        public async Task<ApiResult<IEnumerable<UserProfileResponse>>> GetAllUsers(CancellationToken cancellationToken = default)
            => await this.client.GetApiResultAsync<IEnumerable<UserProfileResponse>>($"api/users", cancellationToken: cancellationToken);
        
        public async Task<ApiResult<UserProfileResponse>> GetUser(long id, CancellationToken cancellationToken = default)
            => await this.client.GetApiResultAsync<UserProfileResponse>($"api/user/{id}", cancellationToken: cancellationToken);
        // Patch
        public async Task<ApiResult<UserProfileResponse>> UpdateUser(long id, UserProfileUpdateRequest request, CancellationToken cancellationToken = default)
            => await this.client.PatchApiResultAsync<UserProfileUpdateRequest, UserProfileResponse>($"api/user/{id}", request, cancellationToken: cancellationToken);
        //Put
        public async Task<ApiResult<UserProfileResponse>> CreateUser(UserProfileCreateRequest request, CancellationToken cancellationToken = default)
            => await this.client.PutApiResultAsync<UserProfileCreateRequest, UserProfileResponse>($"api/user", request, cancellationToken);
        // Delete
        public async Task<ApiResult> DeleteUser(long id, CancellationToken cancellationToken = default)
            => await this.client.DeleteApiResultAsync($"api/user/{id}", cancellationToken: cancellationToken);
    }
}
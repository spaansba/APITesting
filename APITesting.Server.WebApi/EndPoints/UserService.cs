using APITesting.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace APITesting.EndPoints;
 
public interface IUserService
{
    public Task<IEnumerable<UserProfileResponse>> GetAllUsers(CancellationToken cancellationToken);
    public Task<UserProfileResponse?> GetUserProfile(int id, CancellationToken cancellationToken);
    public Task<UserProfileResponse?> UpdateUserProfile(int id, UserProfileUpdateRequest request, CancellationToken cancellationToken);
    public Task<UserProfileResponse?> CreateUserProfile(UserProfileCreateRequest request, CancellationToken cancellationToken);
    public Task<bool> DeleteUserProfile(int id, CancellationToken cancellationToken);
}
 
internal sealed class UserService : IUserService
{
    private readonly ILogger<UserService> logger;
 
    private readonly Dictionary<int, UserProfileResponse> users = new();
    private readonly HashSet<string> usernames = new();
    private int nextId = 1;
 
    public UserService(ILogger<UserService> logger)
    {
        this.logger = logger;
    }
    public IEnumerable<UserProfileResponse> GetAllUsers() => this.users.Values.ToList();
 
    public UserProfileResponse? GetUserProfile(int id) => users.GetValueOrDefault(id);
    
    public UserProfileResponse? UpdateUserProfile(int id, UserProfileUpdateRequest request)
    {
        if (this.users.TryGetValue(id, out var existing) is false)
        {
            this.logger.LogWarning("Attempted to update user {UserId}, which doesn't exist", id);
            return null;
        }
        existing = existing with
        {
            DisplayName = request.DisplayName ?? existing.DisplayName,
            FullName = request.FullName ?? existing.FullName,
        };
        this.users[id] = existing;
        this.logger.LogInformation("Updated user {UserId}", id);
        return existing;
    }
 
    public UserProfileResponse? CreateUserProfile(UserProfileCreateRequest request)
    {
        if (this.usernames.Contains(request.Username))
        {
            this.logger.LogWarning("Attempted to create a user with username {Username}, which is already in use", request.Username);
            return null;
        }
        var id = this.nextId++;
        var user = new UserProfileResponse(
            Id: id,
            Username: request.Username,
            FullName: request.FullName,
            DisplayName: request.DisplayName
        );
        this.users.Add(id, user);
        this.usernames.Add(user.Username);
        this.logger.LogInformation("Created user {Username}", request.Username);
        return user;
    }
 
    public bool DeleteUserProfile(int id)
    {
        if (this.users.Remove(id, out var user) is false)
        {
            this.logger.LogWarning("Attempted to delete user {UserId}, which doesn't exist", id);
            return false;
        }
        this.usernames.Remove(user.Username);
        this.logger.LogInformation("Deleted user {Username}", user.Username);
        return true;
    }
 
    Task<IEnumerable<UserProfileResponse>> IUserService.GetAllUsers(CancellationToken cancellationToken) => Task.FromResult(GetAllUsers());
    Task<UserProfileResponse?> IUserService.GetUserProfile(int id, CancellationToken cancellationToken)
        => Task.FromResult(this.GetUserProfile(id));
    Task<UserProfileResponse?> IUserService.UpdateUserProfile(int id, UserProfileUpdateRequest request, CancellationToken cancellationToken)
        => Task.FromResult(this.UpdateUserProfile(id, request));
    Task<UserProfileResponse?> IUserService.CreateUserProfile(UserProfileCreateRequest request, CancellationToken cancellationToken)
        => Task.FromResult(this.CreateUserProfile(request));
    Task<bool> IUserService.DeleteUserProfile(int id, CancellationToken cancellationToken)
        => Task.FromResult(this.DeleteUserProfile(id));
}
 

/// <summary>
/// Extension class to register all Test APIs endpoints
/// </summary>

// Cheatsheet
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/parameter-binding?view=aspnetcore-8.0
//     [FromRoute] - the part of the URL after the domain, but before the ?
//     [FromQuery] - the part of the URL after the ?
//     [FromHeader] - From the HTTP request header
//     [FromBody] - Deserialize the HTTP request body
//     [FromServices] - From DI
//     [AsParameters] and [FromForm] exist, but they aren't used as often

public static class UserEndpoints
{
    public static void AddUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", GetAllUsers);
        app.MapGet("/api/user/{id:int}", GetUser);
        app.MapPut("/api/user", CreateUser);
        app.MapPatch("/api/user/{id:int}", UpdateUser);
        app.MapDelete("/api/user/{id:int}", DeleteUser);
    }
 
    private static async Task<IResult> GetAllUsers(
        [FromServices] IUserService test,
        CancellationToken cancellationToken
    )
    {
        return TypedResults.Ok(await test.GetAllUsers(cancellationToken));
    }
    private static async Task<IResult> GetUser(
        [FromServices] IUserService test,
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        return await test.GetUserProfile(id, cancellationToken) is not { } result ? TypedResults.NotFound() : TypedResults.Ok(result);
    }
 
    private static async Task<IResult> GetId(
        [FromServices] IUserService test,
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        return await test.GetUserProfile(id, cancellationToken) is not { } result ? TypedResults.NotFound() : TypedResults.Ok(result);
    }
    
    private static async Task<IResult> UpdateUser(
        [FromServices] IUserService test,
        [FromRoute] int id,
        [FromBody] UserProfileUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        return await test.UpdateUserProfile(id, request, cancellationToken) is not { } result ? TypedResults.NotFound() : TypedResults.Ok(result);
    }
    private static async Task<IResult> CreateUser(
        [FromServices] IUserService test,
        [FromBody] UserProfileCreateRequest request,
        CancellationToken cancellationToken
    )
    {
        return await test.CreateUserProfile(request, cancellationToken) is not { } result ? TypedResults.Conflict() : TypedResults.Ok(result);
    }
    private static async Task<IResult> DeleteUser(
        [FromServices] IUserService test,
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        return await test.DeleteUserProfile(id, cancellationToken) ? TypedResults.NotFound() : TypedResults.Ok();
    }
}
 
 
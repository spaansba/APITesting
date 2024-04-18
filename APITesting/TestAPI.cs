using Microsoft.AspNetCore.Mvc;

namespace APITesting;

public class UserService
{
    private readonly ILogger<UserService> logger;
    private readonly IConfiguration configuration;
    private string displayName = "John Smith";
    private const string username = "jsmith";

    private UserProfileResponse CreateProfileResponse() => new(1, username, this.displayName);
    
    public UserService(ILogger<UserService> logger, IConfiguration configuration)
    {
        this.logger = logger;
        this.configuration = configuration;
    }
    public Task<IEnumerable<UserProfileResponse>> GetAllUsers()
    {
        this.logger.LogInformation("Executing GetTest method");
        this.logger.LogInformation("My Custom Setting Value - {Setting}", this.configuration["CustomSetting"]);
        return Task.FromResult(Enumerable.Repeat(this.CreateProfileResponse(), 1));
    }
    public Task<UserProfileResponse?> GetUserProfile(int id)
    {
        this.logger.LogInformation("Executing GetTest method");
        this.logger.LogInformation("My Custom Setting Value - {Setting}", this.configuration["CustomSetting"]);
        var profile = id is 1 ? this.CreateProfileResponse() : null;
        return Task.FromResult(profile);
    }
    public Task<UserProfileResponse?> UpdateUserProfile(int id, UserProfileUpdateRequest request)
    {
        this.logger.LogCritical("Executing GetPost method");
        if(id is 1)
            this.displayName = request.DisplayName ?? this.displayName;
        return this.GetUserProfile(id);
    }
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
        /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-8.0#route-constraints
        app.MapGet("/api/users", GetAllUsers);
        app.MapGet("/api/user/{id:int}", GetUser);
        app.MapPost("/api/user/{id:int}", UpdateUser);
    }
    
    private static async Task<IResult> GetAllUsers(
        [FromServices] UserService test
    )
    {
        var result = await test.GetAllUsers();
        return TypedResults.Ok(result);
    }
    private static async Task<IResult> GetUser(
        [FromServices] UserService test,
        [FromRoute] int id
    )
    {
        var result = await test.GetUserProfile(id);
        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }
    
    private static async Task<IResult> UpdateUser(
        [FromServices] UserService test,
        [FromRoute] int id,
        [FromBody] UserProfileUpdateRequest request
    )
    {
        var result = await test.UpdateUserProfile(id, request);
        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }
}
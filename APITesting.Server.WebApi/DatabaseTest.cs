using System.Data;
using APITesting.Contracts;
using Dapper;
using Npgsql;

namespace APITesting;

public class DatabaseTest : BackgroundService, IDatabase
{
    
    private const string ConnectionString = "Host=localhost;Username=postgres;Password=postgres;Database=test_database";
    
    // Make the query strings const, so it doesn't recreate the string each time we execute the query
    // Make sure the string interpolation in a const are compile time constant
    private const string GetAllUsersQuery = $"""
                                             SELECT id AS {nameof(UserProfileResponse.Id)} ,
                                                 username AS {nameof(UserProfileResponse.Username)},
                                                 full_name AS {nameof(UserProfileResponse.FullName)} ,
                                                 display_name AS {nameof(UserProfileResponse.DisplayName)}
                                             FROM users;
                                             """;

    // For SQL queries with ANY user input use Parameters like we do below to combat sql injection attacks
    // https://www.postgresql.org/docs/current/dml-returning.html < returning logic for postgres
    private const string InsertUserQuery = $"""
                                             INSERT INTO users  (username, full_name, display_name) 
                                             VALUES(
                                                 @{ nameof(UserProfileCreateRequest.Username) },
                                                 @{ nameof(UserProfileCreateRequest.FullName) },
                                                 @{ nameof(UserProfileCreateRequest.DisplayName) }
                                             )
                                                 RETURNING id AS {nameof(UserProfileResponse.Id)}, 
                                                 username AS {nameof(UserProfileResponse.Username)}, 
                                                 full_name AS {nameof(UserProfileResponse.FullName)}, 
                                                 display_name AS {nameof(UserProfileResponse.DisplayName)}
                                             """;

    public async Task<UserProfileResponse> Create(UserProfileCreateRequest request)
    {
        await using var connection = await CreateConnection();
        return connection.QuerySingle<UserProfileResponse>(InsertUserQuery, request);
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync(stoppingToken);
        var users = await connection.QueryAsync<UserProfileResponse>(GetAllUsersQuery);
        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }

    private static async Task<NpgsqlConnection> CreateConnection(CancellationToken cancellationToken = default)
    {
        NpgsqlConnection? connection = null;
        try
        {
            connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
        catch
        {
            // try catch because otherwise if OpenAsync throws an exception, then the NpgsqlConnection never gets disposed.
            if(connection is not null)
                await connection.DisposeAsync();
            throw;
        }
    }
    
    public async Task Perform(Func<IDbConnection, CancellationToken, Task> func, CancellationToken cancellationToken)
    {
        await using var connection = await CreateConnection(cancellationToken);
        await func(connection, cancellationToken);
    }
    
    public async Task Perform<TArgument>(
        Func<IDbConnection, TArgument, CancellationToken, Task> func,
        TArgument argument,
        CancellationToken cancellationToken
    )
    {
        await using var connection = await CreateConnection(cancellationToken);
        await func(connection, argument, cancellationToken);
    }

    public async Task Perform(Func<IDbConnection, Task> func)
    {
        await using var connection = await CreateConnection();
        await func(connection);
    }

    public async Task Perform<TArgument>(Func<IDbConnection, TArgument, Task> func, TArgument argument)
    {
        await using var connection = await CreateConnection();
        await func(connection, argument);
    }

    public async Task<TResult> Get<TResult>(Func<IDbConnection, CancellationToken, Task<TResult>> func, CancellationToken cancellationToken)
    {
        await using var connection = await CreateConnection(cancellationToken);
        return await func(connection, cancellationToken);
    }

    public async Task<TResult> Get<TArgument, TResult>(Func<IDbConnection, TArgument, CancellationToken, Task<TResult>> func, TArgument argument, CancellationToken cancellationToken)
    {
        await using var connection = await CreateConnection(cancellationToken);
        return await func(connection, argument, cancellationToken);
    }

    public async Task<TResult> Get<TResult>(Func<IDbConnection, Task<TResult>> func)
    {
        await using var connection = await CreateConnection();
        return await func(connection);
    }

    public async Task<TResult> Get<TArgument, TResult>(Func<IDbConnection, TArgument, Task<TResult>> func, TArgument argument)
    {
        await using var connection = await CreateConnection();
        return await func(connection, argument);
    }
}
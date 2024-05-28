using System.Data;
using APITesting.Contracts;
using Dapper;
using Npgsql.Replication;

namespace APITesting;

public class UserRepository
{
    public UserRepository(IDatabase database)
    {
        this.database = database;
    }

    private readonly IDatabase database; // Ask the DI for database via the CTOR
    public async Task DeleteUser(long userId, CancellationToken cancellationToken)
    {
        await this.database.Perform(Delete, userId, cancellationToken);
        
        static async Task Delete(IDbConnection connection, long userId, CancellationToken cancellationToken)
        {
            await connection.ExecuteAsync(
                "DELETE FROM users WHERE id = @Id", 
                new { Id = userId });
        }
    }
    
    public async Task<UserProfileResponse> AddSingleUser(UserProfileCreateRequest createRequest)
    {
        return await this.database.Get(AddNewUser, createRequest);

        static async Task<UserProfileResponse> AddNewUser(IDbConnection connection, UserProfileCreateRequest createRequest)
        { 
            var x =  await connection.QuerySingleAsync<UserProfileResponse>(InsertUserQuery, createRequest);
            return x;
        }
    }

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




}
using System.Data;
using APITesting.Contracts;
using Dapper;

namespace APITesting;

public class UserRepository
{
    private readonly IDatabase database = new DatabaseTest(); // Assume this is properly initialized
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
            var x =  await connection.QuerySingle<Task<UserProfileResponse>>(InsertUserQuery, createRequest);
            return x;
        }
    }

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
using System.Data;
using Dapper;

namespace APITesting;

public class UserRepository
{
    private IDatabase database; // Assume this is properly initialized
    public async Task DeleteUser(long userId, CancellationToken cancellationToken)
    {
        await this.database.Perform(DoWork, cancellationToken);
        
        async Task DoWork(IDbConnection connection, CancellationToken cancellationToken)
        {
            await connection.ExecuteAsync(
                "DELETE FROM users WHERE id = @Id", 
                new { Id = userId }, 
            cancellationToken: cancellationToken);
        }
    }
}
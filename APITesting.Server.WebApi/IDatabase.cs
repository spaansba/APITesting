using System.Data;

namespace APITesting;


public interface IDatabase
{
    /// <summary>
    /// For methods that don't return a value
    /// </summary>
    /// <param name="func"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Perform(
        Func<IDbConnection, CancellationToken, Task> func, 
        CancellationToken cancellationToken
    );
    
    /// <summary>
    /// For methods that Do return a value
    /// </summary>
    /// <param name="func"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public Task<TResult> Get<TResult>(
        Func<IDbConnection, CancellationToken, Task<TResult>> func, 
        CancellationToken cancellationToken
    );
}
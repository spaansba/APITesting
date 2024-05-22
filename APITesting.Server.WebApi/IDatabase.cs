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
    public Task Perform<TArgument>(
        Func<IDbConnection, TArgument, CancellationToken, Task> func, 
        TArgument argument,
        CancellationToken cancellationToken
    );
    // Without cancellation token
    public Task Perform(
        Func<IDbConnection, Task> func
    );
    public Task Perform<TArgument>(
        Func<IDbConnection, TArgument, Task> func, 
        TArgument argument
    );
    
    // With Transaction
    
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
    public Task<TResult> Get<TArgument, TResult>(
        Func<IDbConnection, TArgument, CancellationToken, Task<TResult>> func, 
        TArgument argument,
        CancellationToken cancellationToken
    );
    // Without cancellation token
    public Task<TResult> Get<TResult>(
        Func<IDbConnection, Task<TResult>> func
    );
    public Task<TResult> Get<TArgument, TResult>(
        Func<IDbConnection, TArgument, Task<TResult>> func, 
        TArgument argument
    );
    
    // With Transaction
    public Task<TResult> Get<TArgument, TResult>(
        Func<IDbConnection, IDbTransaction, TArgument, CancellationToken, Task<TResult>> func, 
        TArgument argument,
        CancellationToken cancellationToken
    );
}
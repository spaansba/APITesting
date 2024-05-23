using System.Data;
using System.Transactions;

namespace APITesting;
public interface IDatabase
{
    /// <summary>
    /// Executes the provided function against a database connection, with the option to use a CancellationToken.
    /// </summary>
    /// <returns>Nothing</returns>
    public Task Perform(
        Func<IDbConnection, CancellationToken, Task> func, 
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Executes the provided function against a database connection and transaction, with the option to use a CancellationToken.
    /// </summary>
    /// <returns>Nothing</returns>
    public Task Perform(
        Func<IDbConnection, IDbTransaction, CancellationToken, Task> func, 
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Executes the provided function against a database connection, with an argument and the option to use a CancellationToken.
    /// </summary>
    /// <returns>Nothing</returns>
    public Task Perform<TArgument>(
        Func<IDbConnection, TArgument, CancellationToken, Task> func, 
        TArgument argument,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Executes the provided function against a database connection and transaction, with an argument and the option to use a CancellationToken.
    /// </summary>
    /// <returns>Nothing</returns>
    public Task Perform<TArgument>(
        Func<IDbConnection, IDbTransaction, TArgument, CancellationToken, Task> func, 
        TArgument argument,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Executes the provided function against a database connection.
    /// </summary>
    /// <param name="func">The function to execute against the database connection.</param>
    /// <returns>Nothing</returns>
    public Task Perform(
        Func<IDbConnection, Task> func
    );

    /// <summary>
    /// Executes the provided function against a database connection and transaction.
    /// </summary>
    /// <returns>Nothing</returns>
    public Task Perform(
        Func<IDbConnection, IDbTransaction, Task> func
    );

    /// <summary>
    /// Executes the provided function against a database connection, with an argument.
    /// </summary>
    /// <returns>Nothing</returns>
    public Task Perform<TArgument>(
        Func<IDbConnection, TArgument, Task> func, 
        TArgument argument
    );

    /// <summary>
    /// Executes the provided function against a database connection and transaction, with an argument.
    /// </summary>
    /// <returns>Nothing</returns>
    public Task Perform<TArgument>(
        Func<IDbConnection, IDbTransaction, TArgument, Task> func, 
        TArgument argument
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
    public Task<TResult> Get<TResult>(
        Func<IDbConnection, IDbTransaction, CancellationToken, Task<TResult>> func, 
        CancellationToken cancellationToken
    );
    public Task<TResult> Get<TArgument, TResult>(
        Func<IDbConnection, TArgument, CancellationToken, Task<TResult>> func, 
        TArgument argument,
        CancellationToken cancellationToken
    );
    public Task<TResult> Get<TArgument, TResult>(
        Func<IDbConnection, IDbTransaction, TArgument, CancellationToken, Task<TResult>> func, 
        TArgument argument,
        CancellationToken cancellationToken
    );
    public Task<TResult> Get<TResult>(
        Func<IDbConnection, Task<TResult>> func
    );
    public Task<TResult> Get<TResult>(
        Func<IDbConnection, IDbTransaction, Task<TResult>> func
    );
    public Task<TResult> Get<TArgument, TResult>(
        Func<IDbConnection, TArgument, Task<TResult>> func, 
        TArgument argument
    );
    public Task<TResult> Get<TArgument, TResult>(
        Func<IDbConnection, IDbTransaction, TArgument, Task<TResult>> func, 
        TArgument argument
    );
}
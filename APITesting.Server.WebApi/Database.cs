using System.Data;
using System.Transactions;
using APITesting.Contracts;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace APITesting;

public class Database : IDatabase
{

    public Database(IOptions<DatabaseOptions> options)
    {
        this.connectionString = options.Value.CreateConnectionString();
    }

    private readonly string connectionString;

    private async Task<NpgsqlConnection> CreateConnection(CancellationToken cancellationToken = default)
    {
        NpgsqlConnection? connection = null;
        try
        {
            connection = new NpgsqlConnection(connectionString);
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

    public async Task Perform(Func<IDbConnection, IDbTransaction, CancellationToken, Task> func, CancellationToken cancellationToken)
    { 
        await using var connection = await CreateConnection(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        await func(connection, transaction, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
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

    public async Task Perform<TArgument>(Func<IDbConnection, IDbTransaction, TArgument, CancellationToken, Task> func, TArgument argument, CancellationToken cancellationToken)
    {
        await using var connection = await CreateConnection(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        await func(connection, transaction, argument, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task Perform(Func<IDbConnection, Task> func)
    {
        await using var connection = await CreateConnection();
        await func(connection);
    }

    public async Task Perform(Func<IDbConnection, IDbTransaction, Task> func)
    {
        await using var connection = await CreateConnection();
        await using var transaction = await connection.BeginTransactionAsync();
        await func(connection, transaction);
        await transaction.CommitAsync();
    }

    public async Task Perform<TArgument>(Func<IDbConnection, TArgument, Task> func, TArgument argument)
    {
        await using var connection = await CreateConnection();
        await func(connection, argument);
    }

    public async Task Perform<TArgument>(Func<IDbConnection, IDbTransaction, TArgument, Task> func, TArgument argument)
    {
        await using var connection = await CreateConnection();
        await using var transaction = await connection.BeginTransactionAsync();
        await func(connection, transaction, argument);
        await transaction.CommitAsync();
    }

    public async Task<TResult> Get<TResult>(Func<IDbConnection, CancellationToken, Task<TResult>> func, CancellationToken cancellationToken)
    {
        await using var connection = await CreateConnection(cancellationToken);
        return await func(connection, cancellationToken);
    }

    public async Task<TResult> Get<TResult>(Func<IDbConnection, IDbTransaction, CancellationToken, Task<TResult>> func, CancellationToken cancellationToken)
    {
        await using var connection = await CreateConnection(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        var result =  await func(connection, transaction, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return result;
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

    public async Task<TResult> Get<TResult>(Func<IDbConnection, IDbTransaction, Task<TResult>> func)
    {
        await using var connection = await CreateConnection();
        await using var transaction = await connection.BeginTransactionAsync();
        var result =  await func(connection, transaction);
        await transaction.CommitAsync();
        return result;
    }

    public async Task<TResult> Get<TArgument, TResult>(Func<IDbConnection, TArgument, Task<TResult>> func, TArgument argument)
    {
        await using var connection = await CreateConnection();
        return await func(connection, argument);
    }

    public async Task<TResult> Get<TArgument, TResult>(Func<IDbConnection, IDbTransaction, TArgument, Task<TResult>> func, TArgument argument)
    {
        await using var connection = await CreateConnection();
        await using var transaction = await connection.BeginTransactionAsync();
        var result =  await func(connection, transaction, argument);
        await transaction.CommitAsync();
        return result;
    }

    public async Task<TResult> Get<TArgument, TResult>(Func<IDbConnection, IDbTransaction, TArgument, CancellationToken, Task<TResult>> func, 
        TArgument argument, CancellationToken cancellationToken)
    {
        await using var connection = await CreateConnection(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        var result =  await func(connection, transaction, argument, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return result;
    }
    
    
    
}


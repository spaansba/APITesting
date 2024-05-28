using System.Reflection;
using DbUp;

namespace APITesting.Server.Migrations;

// The migration project is responsible for getting the database to the desired initial state.
// It is NOT responsible for doing regular queries.

// It creates tables, views, functions, etc.
// It doesn't do any SELECT, UPDATE, INSERT, etc. (unless as part of "getting the database to the desired initial state") 

public static class Program
{

    public static bool RunMigrations(string connectionString, out Exception? error)
    {
        EnsureDatabase.For.PostgresqlDatabase(connectionString);
        var result = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build()
            .PerformUpgrade();
        error = result.Error;
        return result.Successful;
    }
    
    
    private static int Main(string[] args)
    {
        var connectionString = args.FirstOrDefault() ?? throw new InvalidOperationException("Must provide a connection string");
        if (RunMigrations(connectionString, out var exception))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(exception);
        Console.ResetColor();
        return -1;
    }

}


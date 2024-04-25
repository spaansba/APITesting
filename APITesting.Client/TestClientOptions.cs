using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace APITesting.Client;

public sealed class TestClientOptions
{
    public const string Key = "TestClient";
    [Url] //Requires a valid URL or no URL
    [Required] //Makes it so it's only a valid URL
    public string ServerUri { get; set; } = string.Empty; 
}

public sealed class ValidateTestClientOptions : IValidateOptions<TestClientOptions>
{
    public ValidateOptionsResult Validate(string? name, TestClientOptions options)
    {
        if (string.IsNullOrEmpty(options.ServerUri))
            return CreateUriError($"{nameof(TestClientOptions.ServerUri)} is required");
        if(!Uri.TryCreate(options.ServerUri, UriKind.Absolute, out var uri))
            return CreateUriError($"{nameof(TestClientOptions.ServerUri)} is not a valid URI (Provided value: {options.ServerUri})");
        if(uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) || uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
            return ValidateOptionsResult.Success;   
        
        return CreateUriError($"{nameof(TestClientOptions.ServerUri)} does not use http or https (Provided value: {options.ServerUri})");
    }

    private static ValidateOptionsResult CreateUriError(string error)
        => CreateError(nameof(TestClientOptions.ServerUri), error);
    
    
    private static ValidateOptionsResult CreateError(string member, string error)
        => ValidateOptionsResult.Fail($"validation failed for '{nameof(TestClientOptions)}' member: '{member}' with the error: '{error}'");
}
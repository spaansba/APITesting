using Microsoft.Extensions.DependencyInjection;

namespace APITesting.Client;

public class TestClient
{
    /// <summary>
    /// Warning: Never Dispose the HTTPClient
    /// </summary>
    private readonly HttpClient client;
    public const string HttpClientName = "TestClient"; 
    public TestClient(HttpClient client)
    {
        this.client = client;
    }
    
    [ActivatorUtilitiesConstructor] //DI frameworks will use this ctor
    public TestClient(IHttpClientFactory clientFactory)
        : this(clientFactory.CreateClient(HttpClientName))
    {

    }

    public TestClient(IHttpClientFactory clientFactory, Func<IHttpClientFactory, HttpClient> createClient)
        : this(createClient(clientFactory))
    {

    }
}
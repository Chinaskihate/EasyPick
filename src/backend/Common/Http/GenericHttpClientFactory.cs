namespace Http;

public class GenericHttpClientFactory : IHttpClientFactory<GenericHttpClient>
{
    private readonly IDictionary<string, string> _httpClients = new Dictionary<string, string>();

    public GenericHttpClientFactory Register(string name, string baseUrl)
    {
        _httpClients.Add(name, baseUrl);
        return this;
    }

    public GenericHttpClient CreateClient(string name)
    {
        return new GenericHttpClient(_httpClients[name]);
    }
}
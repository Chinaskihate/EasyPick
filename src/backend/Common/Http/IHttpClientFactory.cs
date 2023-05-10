namespace Http;

public interface IHttpClientFactory<out T>
    where T : HttpClient
{
    public T CreateClient(string name);
}
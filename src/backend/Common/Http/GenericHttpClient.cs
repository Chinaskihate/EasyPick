using Http.Extensions;
using System.Text.Json;

namespace Http;

public class GenericHttpClient : HttpClient
{
    public GenericHttpClient(string baseUrl)
    {
        BaseAddress = new Uri(baseUrl);
    }

    public async Task<T> GetFromJsonAsync<T>(
        string query,
        JsonSerializerOptions options = null,
        CancellationToken ct = default)
    {
        var response = await GetAsync(query, ct);
        response.ThrowIfNotSuccess();
        var jsonResponse = await response.Content.ReadAsStringAsync(ct);
        options ??= new JsonSerializerOptions();
        options.PropertyNameCaseInsensitive = true;

        return JsonSerializer.Deserialize<T>(jsonResponse, options);
    }
}
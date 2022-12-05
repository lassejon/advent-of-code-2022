using System.Net;
using System.Text;

namespace AdventOfCode;

using System.Net.Http;

public class InputReader
{
    private readonly HttpClient _client;
    private readonly Uri _baseUrl;
    public InputReader(string baseUrl, params (string, string)[] cookieVars)
    {
        _baseUrl = new Uri(baseUrl);
        
        var cookies = cookieVars.ToCookieContainer(var => var.Item1, var => var.Item2, _baseUrl);
        var clientHandler = new HttpClientHandler { CookieContainer = cookies };
        _client = new HttpClient(clientHandler);
    }

    public async Task<List<string?>> GetInput(string url)
    {
        var input = (await GetInputStream(url));

        var list = new List<string?>();
        using var sr = new StreamReader(input, Encoding.UTF8);
        while (await sr.ReadLineAsync() is { } line)
        {
            list.Add(line);
        }

        return list;
    }
    
    private async Task<Stream> GetInputStream(string url)
    {
        var response = await _client.GetAsync($"{_baseUrl.AbsoluteUri}{url}");
        return await response.Content.ReadAsStreamAsync();
    }
}
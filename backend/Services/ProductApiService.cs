using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

public class ProductApiService
{
  private readonly HttpClient _httpClient;

  public ProductApiService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<string> GetMaleFootwearAsync()
  {
    var request = new HttpRequestMessage
    {
      Method = HttpMethod.Get,
      RequestUri = new Uri("https://ecommerce-api3.p.rapidapi.com/malefootwear"),
      Headers =
            {
                { "x-rapidapi-key", "0a48ec7484msh9bf19e104a9f818p127f17jsn2aa36736af9f" },
                { "x-rapidapi-host", "ecommerce-api3.p.rapidapi.com" },
            },
    };

    using (var response = await _httpClient.SendAsync(request))
    {
      response.EnsureSuccessStatusCode();
      var body = await response.Content.ReadAsStringAsync();
      return body;
    }
  }
}
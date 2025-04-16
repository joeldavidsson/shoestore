using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Models;

namespace Services
{
  public class ExternalApiService(HttpClient httpClient)
  {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Product>> GetProductsFromExternalApi()
    {
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Get,
        RequestUri = new Uri(_httpClient.BaseAddress ?? throw new InvalidOperationException("BaseAddress is not set on the HttpClient."), "malefootwear")
      };

      using var response = await _httpClient.SendAsync(request);
      response.EnsureSuccessStatusCode();
      var json = await response.Content.ReadAsStringAsync();

      // Deserialize the response into a dictionary to handle "Unnamed: 0"
      var products = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json, new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      });

      // Map the response to the Product model
      var mappedProducts = new List<Product>();
      foreach (var product in products ?? [])
      {
        mappedProducts.Add(new Product
        {
          Id = product.TryGetValue("Unnamed: 0", out var idValue) ? int.Parse(idValue?.ToString() ?? "0") : 0,
          Brand = product.TryGetValue("Brand", out var brandValue) ? brandValue?.ToString() : null,
          Description = product.TryGetValue("Description", out var descriptionValue) ? descriptionValue?.ToString() : null,
          Image = product.TryGetValue("Image", out var imageValue) ? imageValue?.ToString() : null,
          Price = product.TryGetValue("Price", out var priceValue) ? priceValue?.ToString()?.Replace("₹", "").Trim() : null
        });
      }

      return mappedProducts;
    }
  }
}
using System.Net.Http;
using System.Threading.Tasks;
using Crewin_Task.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        var response = await _httpClient.GetStringAsync("https://dummyjson.com/products/categories");

        if (!string.IsNullOrEmpty(response))
        {
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }
        return new List<Category>();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        var response = await _httpClient.GetStringAsync($"https://dummyjson.com/products/category/{category}");
        var data = JsonConvert.DeserializeObject<ProductResponse>(response);
        return data.Products;
    }

    public async Task<Product> GetProductByIdAsync(string category, int id)
    {
        // Tüm ürünleri API'den çek
        var response = await _httpClient.GetStringAsync($"https://dummyjson.com/products/category/{category}");
        var productResponse = JsonConvert.DeserializeObject<ProductResponse>(response);

        // Çekilen ürünler listesinden ID'ye göre eşleşeni bul ve döndür
        return productResponse.Products.FirstOrDefault(p => p.Id == id);
    }


}

public class ProductResponse
{
    public List<Product> Products { get; set; }
}

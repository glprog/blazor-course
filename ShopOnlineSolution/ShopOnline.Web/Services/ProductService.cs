using System.Net.Http.Json;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await this.httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("Product");
            return products;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await this.httpClient.GetFromJsonAsync<ProductDto>($"Product/{id}");
            return product;
        }
    }
}

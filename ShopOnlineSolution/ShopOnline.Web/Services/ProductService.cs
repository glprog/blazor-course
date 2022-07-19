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

		public async Task<IEnumerable<ProductCategoryDto>> GetProductCategoriesAsync()
		{
            var productCategories = await this.httpClient.GetFromJsonAsync<IEnumerable<ProductCategoryDto>>($"Product/GetProductCategories");
            return productCategories;
        }

		public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
		{
            var products = await this.httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"Product/{categoryId}/GetProductsByCategoryAsync");
            return products;
        }
	}
}

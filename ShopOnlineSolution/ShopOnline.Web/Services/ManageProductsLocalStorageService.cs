using Blazored.LocalStorage;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IProductService productService;
        private const string PRODUCT_COLLECTION_KEY = "ProductCollection";

        public ManageProductsLocalStorageService(
            ILocalStorageService localStorageService,
            IProductService productService)
        {
            this.localStorageService = localStorageService;
            this.productService = productService;
        }

        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<IEnumerable<ProductDto>>(PRODUCT_COLLECTION_KEY)
                ?? await this.AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(PRODUCT_COLLECTION_KEY);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection = await this.productService.GetAllProductsAsync();

            if (productCollection != null)
            {
                await this.localStorageService.SetItemAsync(PRODUCT_COLLECTION_KEY, productCollection);
            }

            return productCollection;
        }
    }
}

using Blazored.LocalStorage;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IShoppingCartService shoppingCartService;

        const string KEY = "CartItemsCollection";

        public ManageCartItemsLocalStorageService(ILocalStorageService localStorageService,
            IShoppingCartService shoppingCartService)
        {
            this.localStorageService = localStorageService;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IEnumerable<CartItemDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<IEnumerable<CartItemDto>>(KEY)
                ?? await this.AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(KEY);
        }

        public async Task SaveCollection(List<CartItemDto> cartItemsDtos)
        {
            await this.localStorageService.SetItemAsync(KEY, cartItemsDtos);
        }

        private async Task<IEnumerable<CartItemDto>> AddCollection()
        {
            var collection = await this.shoppingCartService.GetItemsAsync(Constants.UserId);

            if (collection != null)
            {
                await this.localStorageService.SetItemAsync(KEY, collection);
            }

            return collection;
        }
    }
}

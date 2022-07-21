using Microsoft.AspNetCore.Components;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ProductDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorage { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }

        public ProductDto Product { get; set; }
        public string ErrorMessage { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                this.ShoppingCartItems = (await this.ManageCartItemsLocalStorage.GetCollection()).ToList();
                //this.Product = await this.ProductService.GetProductByIdAsync(Id);                
                this.Product = await this.GetProductById(Id);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        protected async Task AddToCart_ClickAsync(CartItemToAddDto cartItemToAddDto)
        {
            var cartItem = await this.ShoppingCartService.AddItemAsync(cartItemToAddDto);

            if (cartItem != null)
            {
                this.ShoppingCartItems.Add(cartItem);
                await this.ManageCartItemsLocalStorage.SaveCollection(this.ShoppingCartItems);
            }

            NavigationManager.NavigateTo("/ShoppingCart");
        }

        private async Task<ProductDto> GetProductById(int id)
        {
            var products = await this.ManageProductsLocalStorageService.GetCollection();

            if (products != null)
            {
                return products.SingleOrDefault(p => p.Id == id);
            }

            return null;
        }
    }
}

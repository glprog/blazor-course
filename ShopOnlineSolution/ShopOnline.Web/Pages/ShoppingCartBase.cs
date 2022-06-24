using Microsoft.AspNetCore.Components;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; private set; }

        protected override async  Task OnInitializedAsync()
        {
            try
            {
                this.ShoppingCartItems = await this.ShoppingCartService.GetItemsAsync(Constants.UserId);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

    }
}

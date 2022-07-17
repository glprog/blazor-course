using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class CheckoutBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        protected IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

        protected int TotalQty { get; set; }

        protected string PaymentDescription { get; set; }

        protected decimal PaymentAmount { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.ShoppingCartItems = await this.ShoppingCartService.GetItemsAsync(Constants.UserId);

                if (this.ShoppingCartItems != null)
                {
                    Guid orderGuid = Guid.NewGuid();

                    this.PaymentAmount = this.ShoppingCartItems.Sum(p => p.TotalPrice);
                    this.TotalQty = this.ShoppingCartItems.Sum(p => p.Qty);
                    this.PaymentDescription = $"O_{Constants.UserId}_{orderGuid}";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine(firstRender);
            if (firstRender)
            {
                await this.Js.InvokeVoidAsync("InitPaypal");
            }
        }
    }
}

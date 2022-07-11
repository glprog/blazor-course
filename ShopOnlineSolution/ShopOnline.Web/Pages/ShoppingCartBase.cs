using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; private set; }

        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }


        protected override async  Task OnInitializedAsync()
        {
            try
            {
                this.ShoppingCartItems = (await this.ShoppingCartService.GetItemsAsync(Constants.UserId)).ToList();
                CartChanged();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            if (qty > 0)
            {
                var updateItemDto = new CartItemQtyUpdateDto
                {
                    CartItemId = id,
                    Qty = qty,
                };

                var result = await this.ShoppingCartService.UpdateQty(updateItemDto);
                UpdateItemTotalPrice(result);
            }
            else
            {
                var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    item.Qty = qty;
                    item.TotalPrice = item.Price;
                }
            }
            CartChanged();
            await MakeUpdateQtyButtonVisible(id, false);
        }

        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await this.ShoppingCartService.DeleteItemAsync(id);
            if (cartItemDto != null)
            {
                this.RemoveCartItem(cartItemDto.Id);
            }
            CartChanged();
        }

        private CartItemDto GetCartItem(int id)
        {
            return this.ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = this.GetCartItem(id);
            this.ShoppingCartItems.Remove(cartItemDto);
        }

        private void SetTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(x => x.TotalPrice).ToString("C");
        }

        private void SetTotalQuantity()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(x => x.Qty);
        }

        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);

            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }
        }

        protected async Task UpdateQty_Input(int id)
        {
            await this.MakeUpdateQtyButtonVisible(id, true);
        }

        protected async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
            await this.Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        }

        private void CartChanged()
        {
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
    }
}

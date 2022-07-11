using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;
        public event Action<int> OnShoppingCartChanged;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto)
        {
            var response = await this.httpClient.PostAsJsonAsync("ShoppingCart", cartItemToAddDto);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(CartItemDto);
                }

                return await response.Content.ReadFromJsonAsync<CartItemDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }

        public async Task<CartItemDto> DeleteItemAsync(int id)
        {
            try
            {
                var response = await this.httpClient.DeleteAsync($"ShoppingCart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return default(CartItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDto> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CartItemDto>> GetItemsAsync(int userId)
        {
            var response = await this.httpClient.GetFromJsonAsync<IEnumerable<CartItemDto>>($"ShoppingCart/{userId}/GetItems");
            return response;
        }

        public void RaiseEventOnShoppingCartChanged(int totalQty)
        {
            if (OnShoppingCartChanged != null)
            {
                OnShoppingCartChanged.Invoke(totalQty);
            }
        }

        public async Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var jsonRequest = JsonConvert.SerializeObject(cartItemQtyUpdateDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PatchAsync($"ShoppingCart/{cartItemQtyUpdateDto.CartItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                var item = await response.Content.ReadFromJsonAsync<CartItemDto>();
                return item;
            }

            return null;
        }
    }
}

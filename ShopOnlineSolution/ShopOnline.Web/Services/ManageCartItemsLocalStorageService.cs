using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        public Task<IEnumerable<CartItemDto>> GetCollection()
        {
            throw new NotImplementedException();
        }

        public Task RemoveCollection()
        {
            throw new NotImplementedException();
        }

        public Task SaveCollection(List<CartItemDto> cartItemsDtos)
        {
            throw new NotImplementedException();
        }
    }
}

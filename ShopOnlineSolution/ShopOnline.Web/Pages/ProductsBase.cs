using Microsoft.AspNetCore.Components;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;
using ShopOnline.Web.Shared;

namespace ShopOnline.Web.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductsService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public ShoppingOnlineConfig ShoppingOnlineConfig { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var products = await this.ProductsService.GetAllProductsAsync();
            this.Products = products;

            var shoppingCartItems = await ShoppingCartService.GetItemsAsync(Constants.UserId);
            var totalQty = shoppingCartItems.Sum(i => i.Qty);

            ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            Console.WriteLine(this.ShoppingOnlineConfig.PAYPAL_CLIENT_ID);
        }

        protected IEnumerable<IGrouping<ProductGroupByCategory, ProductDto>> GetGroupedProductsByCategory()
        {
            var products = Products.GroupBy(p => new ProductGroupByCategory { Id = p.CategoryId, Name = p.CategoryName });
            return products;
        }

    }

    public class ProductGroupByCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ProductGroupByCategory category &&
                   Id == category.Id &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}

using Microsoft.AspNetCore.Components;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductsService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var products = await this.ProductsService.GetAllProductsAsync();
            this.Products = products;
        }
    }
}

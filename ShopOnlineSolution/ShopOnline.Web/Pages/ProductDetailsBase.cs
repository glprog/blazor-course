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

        public ProductDto Product { get; set; }
        public string ErrorMessage { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                this.Product = await this.ProductService.GetProductByIdAsync(Id);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }
    }
}

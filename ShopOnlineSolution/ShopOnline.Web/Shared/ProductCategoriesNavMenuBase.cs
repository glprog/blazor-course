using Microsoft.AspNetCore.Components;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Shared
{
	public class ProductCategoriesNavMenuBase : ComponentBase
	{
		[Inject]
		public IProductService ProductService { get; set; }

		public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }

		public string ErrorMessage { get; set; }

		protected override async Task OnInitializedAsync()
		{
			try
			{
				this.ProductCategories = await this.ProductService.GetProductCategoriesAsync();
			}
			catch (Exception e)
			{
				this.ErrorMessage = e.Message;				
			}
		}
	}
}

using Microsoft.AspNetCore.Components;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
	public class ProductsByCategoryBase : ComponentBase
	{
		[Parameter]
		public int CategoryId { get; set; }

		[Inject]
		public IProductService ProductService { get; set; }

		public IEnumerable<ProductDto> Products { get; set; }

		public string CategoryName { get; set; }

		public string ErrorMessage { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			try
			{
				this.Products = await this.ProductService.GetProductsByCategoryAsync(CategoryId);
				if (this.Products != null && this.Products.Count() > 0)
				{
					var product = this.Products.First();
					this.CategoryName = product.CategoryName;

				}
			}
			catch (Exception e)
			{
				this.ErrorMessage = e.Message;
			}
		}
	}
}

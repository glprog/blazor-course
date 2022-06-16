using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;

namespace ShopOnline.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productsRepository;

        public ProductController(IProductRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await this.productsRepository.GetAllProductsAsync();
            var categories = await this.productsRepository.GetAllProductCategoriesAsync();
            var productsDto = products.ConvertToDto(categories);

            return Ok(productsDto);
        }
    }
}

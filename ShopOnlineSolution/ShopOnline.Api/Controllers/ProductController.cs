using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models;

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
            var productsDto = products.ConvertToDto();

            return Ok(productsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductsAsync(int id)
        {
            var product = await this.productsRepository.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var category = await this.productsRepository.GetProductCategoryAsync(product.CategoryId);
            return Ok(product.ConvertToDto());
        }

        [HttpGet]
        [Route(nameof(GetProductCategories))]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
		{
            var productCategories = await this.productsRepository.GetAllProductCategoriesAsync();
            return Ok(productCategories.ConvertToDto());
		}

        [HttpGet]
        [Route("{categoryId}/GetProductsByCategoryAsync")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategoryAsync(int categoryId)
		{
            var products = await this.productsRepository.GetProductsByCategoryAsync(categoryId);
            var categories = await this.productsRepository.GetAllProductCategoriesAsync();

            return Ok(products.ConvertToDto());
		}
    }
}

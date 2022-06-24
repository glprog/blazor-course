using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models;

namespace ShopOnline.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartController(
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }
        
        [HttpGet("{userId}/GetItems")]
        public async Task<ActionResult> GetItemsAsync(int userId)
        {
            var items = await this.shoppingCartRepository.GetItemsAsync(userId);
            var products = await this.productRepository.GetAllProductsAsync();

            return Ok(items.ConvertoDto(products));
        }

        [HttpGet("{id:int}")]
        [ActionName(nameof(GetItemAsync))]
        public async Task<ActionResult> GetItemAsync(int id)
        {
            var item = await this.shoppingCartRepository.GetItemAsync(id);
            var product = await this.productRepository.GetProductAsync(item.ProductId);

            return Ok(item.ConvertoDto(product));
        }

        [HttpPost]
        public async Task<ActionResult> PostItemAsync([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            var item = await this.shoppingCartRepository.AddItemAsync(cartItemToAddDto);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
                var cartItem = await this.shoppingCartRepository.DeleteItemAsync(id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                var product = await this.productRepository.GetProductAsync(cartItem.ProductId);
                var cartItemDto = cartItem.ConvertoDto(product);

                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

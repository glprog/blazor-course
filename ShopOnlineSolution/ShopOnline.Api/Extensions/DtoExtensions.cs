using ShopOnline.Api.Entities;
using ShopOnline.Models;

namespace ShopOnline.Api.Extensions
{
    public static class DtoExtensions
    {
        public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
		{
            return productCategories.Select(c => new ProductCategoryDto { Id = c.Id, Name = c.Name, IconCSS = c.IconCSS });
		}

        public static IEnumerable<ProductDto> ConvertToDto(
            this IEnumerable<Product> products)
        {
            return products.Select(p =>                
                new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CategoryId = p.CategoryId,
                        CategoryName = p.ProductCategory.Name,
                        Price = p.Price,
                        Description = p.Description,
                        ImageURL = p.ImageURL,
                        Qty = p.Qty,                        
                    }
                );
        }

        public static ProductDto ConvertToDto(
            this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = product.ProductCategory.Name,
                Price = product.Price,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Qty = product.Qty
            };
        }

        public static IEnumerable<CartItemDto> ConvertoDto(
            this IEnumerable<CartItem> cartItems,
            IEnumerable<Product> products
            )
        {
            var items = cartItems.Join(products,
                c => c.ProductId,
                p => p.Id,
                (c, p) => new CartItemDto
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    ProductImageURL = p.ImageURL,
                    Price = p.Price,
                    CartId = c.CartId,
                    Qty = c.Qty,
                    TotalPrice = p.Price * c.Qty
                }).ToList();
            return items;
        }

        public static CartItemDto ConvertoDto(
            this CartItem cartItem,
            Product product
            )
        {
            var item = new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = cartItem.CartId,
                Qty = cartItem.Qty,
                TotalPrice = product.Price * cartItem.Qty
            };
            return item;
        }
    }
}

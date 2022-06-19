using ShopOnline.Api.Entities;
using ShopOnline.Models;

namespace ShopOnline.Api.Extensions
{
    public static class DtoExtensions
    {
        public static IEnumerable<ProductDto> ConvertToDto(
            this IEnumerable<Product> products,
            IEnumerable<ProductCategory> categories)
        {
            return products.Join(
                categories,
                p => p.CategoryId,
                c => c.Id,
                (p, c) => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CategoryId = p.CategoryId,
                        CategoryName = c.Name,
                        Price = p.Price,
                        Description = p.Description,
                        ImageURL = p.ImageURL,
                        Qty = p.Qty,
                    }
                );
        }

        public static ProductDto ConvertToDto(
            this Product product,
            ProductCategory category
            )
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = category.Name,
                Price = product.Price,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Qty = product.Qty
            };
        }
    }
}

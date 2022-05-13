using Mango.Services.ProdcutAPI.Models.DTO;

namespace Mango.Services.ProdcutAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> UpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
    }
}

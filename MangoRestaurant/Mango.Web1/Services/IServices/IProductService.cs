namespace Mango.Web1.Services.IServices
{
    using Mango.Services.ProdcutAPI.Models.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public interface IProductService : IBaseService
    {
        Task<T> GetProductAsync<T>();
        Task<T> GetProductById<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDto productDto);
        Task<T> UpdateProductAsync<T>(ProductDto productDto);
        Task<T> DeleteProductAsync<T>(int id);

    }
}

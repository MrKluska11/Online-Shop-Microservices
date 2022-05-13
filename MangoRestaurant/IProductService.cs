using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public interface IProductService
{
    Task<T> GetProductAsync<T>();
    Task<T> GetProductById<T>(int id);
    Task<T> CreateProductAsync<T>();
    Task<T> UpdateProductAsync<T>();
    Task<T> DeleteProductAsync<T>(int id);

}
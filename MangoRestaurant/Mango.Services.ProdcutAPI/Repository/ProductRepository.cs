using AutoMapper;
using Mango.Services.ProdcutAPI.DbContexts;
using Mango.Services.ProdcutAPI.Models;
using Mango.Services.ProdcutAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProdcutAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = _dbContext.Products.FirstOrDefault(x => x.ProductId == productId);

                if(product == null)
                {
                    return false;
                }

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _dbContext.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable<Product> productList = await _dbContext.Products.ToListAsync();  //IEnumerable tylko do wyświetlania danych
            return _mapper.Map<List<ProductDto>>(productList);
        }

        public async Task<ProductDto> UpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);

            _dbContext.Update(productDto);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }


    }
}

using AutoMapper;
using Mango.Services.ProdcutAPI.Models;
using Mango.Services.ProdcutAPI.Models.DTO;

namespace Mango.Services.ProdcutAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();  
            });

            return mappingConfig;
        }
    }
}

using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTOs;

namespace Mango.Services.ProdcutAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<CartHeader, CartHeaderDto>();
                config.CreateMap<CartDetails, CartDetailsDto>();
                config.CreateMap<Cart, CartDto>();
            });

            return mappingConfig;
        }
    }
}

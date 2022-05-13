using Mango.Services.ProdcutAPI.Models.DTO;
using Mango.Web1.Models;
using Mango.Web1.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web1.Controllers

{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new List<ProductDto>();
            var response = await _productService.GetProductAsync<ResponseDto>();

            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                return Ok(list);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate([FromBody]ProductDto model)
        {
            var response = await _productService.CreateProductAsync<ResponseDto>(model);

            if(ModelState.IsValid)
            {
                if (response != null && response.IsSuccess)
                {
                    return Created("", model);
                }
            }

            return NotFound();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit([FromBody] ProductDto model, [FromRoute]int id)
        {
            var response = await _productService.GetProductById<ResponseDto>(id);

                if (response != null && response.IsSuccess)
                {
                ProductDto model1 = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return Created("", model1);
                }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> ProductDelete([FromBody] ProductDto model)
        {
            if(ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId);

                if (response != null && response.IsSuccess)
                {
                    return NoContent();
                }
               
            }
            return NotFound();

        }
    }
}

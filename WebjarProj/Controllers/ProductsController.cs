using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebjarProj.Models;
using WebjarProj.Models.Requests;
using WebjarProj.Models.Responses;
using WebjarProj.Services.Interfaces;
using WebjarProj.Utility;

namespace WebjarProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public ProductsController(IMapper mapper, IProductService productService, IConfiguration configuration)
        {
            _mapper = mapper;
            _productService = productService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateProduct(ProductRequest request,
         [FromQuery] List<int>? featureIds = null)
        {
            try
            {
                // Map the request model to Product entity
                var product = _mapper.Map<Product>(request);

                // Sets the price to mapped model
                switch (request.PriceType)
                {
                    case Models.Requests.PriceType.CONSTANT:
                        if (!decimal.TryParse(request.Price, out decimal x))
                        {
                            return BadRequest(new ResultDTO()
                            {
                                Message = "Price should be number as string.",
                            });
                        }
                        product.Price = request.Price;
                        break;

                    case Models.Requests.PriceType.FORMULA:
                        // Get the $DOLLAR value from AppSettings.json
                        decimal dollarValue = _configuration.GetValue<decimal>("AppSettings:$DOLLAR");
                        string price = Helper.CalculatePriceFromFormula(request.Price, dollarValue);
                        product.Price = price;
                        break;

                    default:
                        return BadRequest(new ResultDTO()
                        {
                            Message = "PriceType value not supported.",
                        });
                }

                if (decimal.Parse(product.Price) < 0)
                {
                    return BadRequest(new ResultDTO()
                    {
                        Message = "Price value can't be lower than zero.",
                    });
                }

                if (request.DiscountAmount > decimal.Parse(product.Price))
                {
                    return BadRequest(new ResultDTO()
                    {
                        Message = "Discount value can't be higher than Price value.",
                    });
                }

                if (request.DiscountAmount is null && request.DiscountExpireAt is not null)
                {
                    return BadRequest(new ResultDTO()
                    {
                        Message = "DiscountAmount can't be null.",
                    });
                }

                // Call the ProductService to create the product
                await _productService.CreateProductAsync(product, featureIds);


                // Return a success response

                var ـresponse = new ResultDTO
                {
                    Success = true,
                    Message = "Product created successfully."
                };
                return CreatedAtAction(nameof(CreateProduct), ـresponse);
            }
            catch (Exception e)
            {
                var _response = new ResultDTO()
                {
                    Message = $"{e.HResult}: {e.Message}",
                };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<ProductsResponse>> GetAllProducts(
            [FromQuery] string? name = null,
            [FromQuery] string? priceType = null,
            [FromQuery] List<int>? featureIds = null,
            [FromQuery] bool withDiscounts = false,
            [FromQuery] bool sortByPrice = true)
        {
            try
            {
                var result = await _productService.GetAllProductsAsync(name, priceType, featureIds, withDiscounts, sortByPrice);
                var customProduct = _mapper.Map<List<CustomProductResult>>(result);

                var _response = new ProductsResponse()
                {
                    Success = true,
                    Message = "Products retrieved successfuly.",
                    Products = customProduct
                };
                return Ok(_response);
            }
            catch (Exception e)
            {
                var _response = new ResultDTO()
                {
                    Message = $"{e.HResult}: {e.Message}",
                };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
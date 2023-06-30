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
        private readonly IAddonService _addonService;
        private readonly IConfiguration _configuration;

        public ProductsController(
            IMapper mapper,
            IProductService productService,
            IAddonService addonService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _productService = productService;
            _addonService = addonService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateProduct(
            ProductRequest request,
            [FromQuery] List<int>? featureIds = null)
        {
            try
            {
                // Map the request model to Product entity
                var product = _mapper.Map<Product>(request);

                string Price;
                // Sets the price to mapped model
                switch (request.PriceType)
                {
                    case "CONSTANT":
                        if (!decimal.TryParse(request.Price, out decimal x))
                        {
                            return BadRequest(new ResultDTO()
                            {
                                Message = "Price should be number as string.",
                            });
                        }
                        Price = request.Price;
                        break;

                    case "FORMULA":
                        // Get the $DOLLAR value from AppSettings.json
                        decimal dollarValue = _configuration.GetValue<decimal>("AppSettings:$DOLLAR");
                        Price = Helper.CalculatePriceFromFormula(request.Price, dollarValue);
                        break;

                    default:
                        return BadRequest(new ResultDTO()
                        {
                            Message = "PriceType value not supported.",
                        });
                }

                if (decimal.Parse(Price) < 0)
                {
                    return BadRequest(new ResultDTO()
                    {
                        Message = "Price value can't be lower than zero.",
                    });
                }

                if (request.DiscountAmount > decimal.Parse(Price))
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

                // Get the $DOLLAR value from AppSettings.json
                decimal dollarValue = _configuration.GetValue<decimal>("AppSettings:$DOLLAR");

                // Set FinalPrice by Discount and Formula (If exists)
                foreach (var product in customProduct)
                {
                    product.FinalPrice = await Helper.CalculateProductFinalPrice(product, dollarValue);
                }

                var _response = new ProductsResponse()
                {
                    Success = true,
                    Message = "Products retrieved successfuly.",
                    Products = customProduct,
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

        [HttpPut("{productId}")]
        public async Task<ActionResult<ResultDTO>> UpdateProduct(
            int productId,
            [FromBody] ProductRequest request,
            [FromQuery] List<int>? featureIds = null)
        {
            try
            {
                var Product = _mapper.Map<Product>(request);
                Product.ProductId = productId;

                await _productService.UpdateProductAsync(Product, featureIds);
                var _response = new ResultDTO()
                {
                    Success = true,
                    Message = $"Product updated successfuly",
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

        [HttpGet("{productId}")]
        public async Task<ActionResult<SingleProductResponse>> GetProductById(
            int productId,
            [FromQuery] List<int>? addonIds = null)
        {
            try
            {
                List<Addon> addons = new();
                var result = await _productService.GetProductByIdAsync(productId);
                var customProduct = _mapper.Map<CustomProductResult>(result);

                // Get the $DOLLAR value from AppSettings.json
                decimal dollarValue = _configuration.GetValue<decimal>("AppSettings:$DOLLAR");

                // Set FinalPrice by Discount and Formula (If exists)
                customProduct.FinalPrice = await Helper.CalculateProductFinalPrice(customProduct, dollarValue);

                // Calculate the price of addons
                if (addonIds is not null && addonIds.Any())
                {
                    addons = await _addonService.GetAddonByIdsAsync(addonIds);
                    foreach (var addon in addons){
                        customProduct.FinalPrice += addon.Price;
                    }
                }
                var _response = new SingleProductResponse()
                {
                    Success = true,
                    Message = "Product retrieved successfuly.",
                    Product = customProduct,
                    addons = addons
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

        [HttpDelete("{productId}")]
        public async Task<ActionResult<ResultDTO>> DeleteProduct(int productId)
        {
            try
            {
                await _productService.DeleteProductByIdAsync(productId);
                var _response = new ResultDTO()
                {
                    Success = true,
                    Message = "Product and its Dependencies removed Successfuly."
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
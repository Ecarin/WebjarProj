using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebjarProj.Models;
using WebjarProj.Models.Requests;
using WebjarProj.Models.Responses;
using WebjarProj.Services.Interfaces;

namespace WebjarProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFeatureService _featureService;

        public FeaturesController(IMapper mapper, IFeatureService featureService)
        {
            _mapper = mapper;
            _featureService = featureService;
        }

        /// <summary>
        /// Create some Features before creating a Product.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateFeature(CreateFeatureRequest request)
        {
            try
            {
                // Mapping request to Feature
                var feature = _mapper.Map<Feature>(request);

                // Creating a new Feature in db
                await _featureService.CreateFeatureAsync(feature);

                var ـresponse = new ResultDTO
                {
                    Success = true,
                    Message = "Feature created successfully."
                };
                return CreatedAtAction(nameof(CreateFeature), ـresponse);
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

        /// <summary>
        /// Get all Features.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<FeaturesResponse>> GetAllFeatures()
        {
            try
            {
                // Gets All Features from db
                var features = await _featureService.GetAllFeaturesAsync();
                if (features is null || features.Count == 0) // Nothing found
                {
                    var _response = new FeaturesResponse
                    {
                        Success = true,
                        Message = "Features not found.",
                    };
                    return Ok(_response);
                }
                else // Result found
                {
                    var _response = new FeaturesResponse
                    {
                        Success = true,
                        Message = "Features retrieved successfully.",
                        Features = features
                    };
                    return Ok(_response);
                }
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

        /// <summary>
        /// Get a Feature by its Id.
        /// </summary>
        /// <param name="featureId">unique Id of Feature</param>
        /// <returns></returns>
        [HttpGet("{featureId}")]
        public async Task<ActionResult<SingleFeatureResponse>> GetFeatureById(int featureId)
        {
            try
            {
                // Getting Feature from db by Id
                var feature = await _featureService.GetFeatureByIdAsync(featureId);
                if (feature is null) // Nothing found
                {
                    var _response = new SingleFeatureResponse
                    {
                        Success = true,
                        Message = "Feature not found."
                    };
                    return Ok(_response);
                }
                else // Result found
                {
                    var _response = new SingleFeatureResponse
                    {
                        Success = true,
                        Message = "Feature retrieved successfully.",
                        Feature = feature
                    };
                    return Ok(_response);
                }
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

        /// <summary>
        /// Update Feature Name and Value.
        /// </summary>
        /// <param name="featureId">unique Id of Feature</param>
        /// <returns></returns>
        [HttpPut("{featureId}")]
        public async Task<ActionResult<ResultDTO>> UpdateFeature(int featureId, UpdateFeatureRequest request)
        {
            try
            {
                // Mapping request to Feature
                var feature = _mapper.Map<Feature>(request);
                feature.FeatureId = featureId; // Because our request doesn't have Id so we have to map it Manually

                // Update Feature to db
                await _featureService.UpdateFeatureAsync(feature);

                var _response = new ResultDTO
                {
                    Success = true,
                    Message = "Feature updated successfully."
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

        /// <summary>
        /// Delete a Feature.
        /// </summary>
        /// <param name="featureId">unique Id of Feature</param>
        /// <returns></returns>
        [HttpDelete("{featureId}")]
        public async Task<ActionResult<ResultDTO>> DeleteFeature(int featureId)
        {
            try
            {
                // Delete Feature from db
                await _featureService.DeleteFeatureAsync(featureId);

                var _response = new ResultDTO
                {
                    Success = true,
                    Message = "Feature deleted successfully."
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

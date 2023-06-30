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

        [HttpGet("{id}")]
        public async Task<ActionResult<SingleFeatureResponse>> GetFeatureById(int id)
        {
            try
            {
                var feature = await _featureService.GetFeatureByIdAsync(id);
                if (feature is null)
                {
                    var _response = new SingleFeatureResponse
                    {
                        Success = true,
                        Message = "Feature not found."
                    };
                    return Ok(_response);
                }
                else
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

        [HttpGet]
        public async Task<ActionResult<FeaturesResponse>> GetAllFeatures()
        {
            try
            {
                var features = await _featureService.GetAllFeaturesAsync();
                if (features is null || features.Count == 0)
                {
                    var _response = new FeaturesResponse
                    {
                        Success = true,
                        Message = "Features not found.",
                    };
                    return Ok(_response);
                }
                else
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

        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateFeature(CreateFeatureRequest request)
        {
            try
            {
                var feature = _mapper.Map<Feature>(request);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ResultDTO>> UpdateFeature(int id, UpdateFeatureRequest request)
        {
            try
            {
                var feature = _mapper.Map<Feature>(request);
                feature.FeatureId = id;

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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultDTO>> DeleteFeature(int id)
        {
            try
            {
                await _featureService.DeleteFeatureAsync(id);

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

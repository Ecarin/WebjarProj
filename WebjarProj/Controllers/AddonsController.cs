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
    public class AddonsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAddonService _addonService;

        public AddonsController(IMapper mapper, IAddonService addonService)
        {
            _mapper = mapper;
            _addonService = addonService;
        }

        /// <summary>
        /// Addons are [optional] to create.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateAddon(CreateAddonRequest request)
        {
            try
            {
                // Mapping request to Addon
                var addon = _mapper.Map<Addon>(request);
                
                // Inserting Addon to db
                await _addonService.CreateAddonAsync(addon);

                // returning response
                var ـresponse = new ResultDTO
                {
                    Success = true,
                    Message = "Addon created successfully."
                };
                return CreatedAtAction(nameof(CreateAddon), ـresponse);
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
        /// Get all Addons.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<AddonsResponse>> GetAllAddons()
        {
            try
            {
                // Getting Addons from db
                var addons = await _addonService.GetAllAddonsAsync();

                if (addons is null || addons.Count == 0) // No Addons found
                {
                    var _response = new AddonsResponse
                    {
                        Success = true,
                        Message = "Addons not found.",
                    };
                    return Ok(_response);
                }
                else // Some Addons found
                {
                    var _response = new AddonsResponse
                    {
                        Success = true,
                        Message = "Addons retrieved successfully.",
                        Addons = addons
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
        /// Get Addon by Id.
        /// </summary>
        /// <param name="addonId">unique Id of Addon</param>
        /// <returns></returns>
        [HttpGet("{addonId}")]
        public async Task<ActionResult<SingleAddonResponse>> GetAddonById(int addonId)
        {
            try
            {
                // Getting Addon from db
                var addon = await _addonService.GetAddonByIdAsync(addonId);
                if (addon is null) // No Addon found
                {
                    var _response = new SingleAddonResponse
                    {
                        Success = true,
                        Message = "Addon not found."
                    };
                    return Ok(_response);
                }
                else // Result found
                {
                    var _response = new SingleAddonResponse
                    {
                        Success = true,
                        Message = "Addon retrieved successfully.",
                        Addon = addon
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
        /// Update Addon Name and Price.
        /// </summary>
        /// <param name="addonId">unique Id of Addon</param>
        /// <returns></returns>
        [HttpPut("{addonId}")]
        public async Task<ActionResult<ResultDTO>> UpdateAddon(int addonId, UpdateAddonRequest request)
        {
            try
            {
                // Mapping request to Addon
                var addon = _mapper.Map<Addon>(request);
                addon.Id = addonId; // Because our request didn't have Id we have to map it manually

                // Update Addon
                await _addonService.UpdateAddonAsync(addon);

                var _response = new ResultDTO
                {
                    Success = true,
                    Message = "Addon updated successfully."
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
        /// Delete an Addon.
        /// </summary>
        /// <param name="addonId">unique Id of Addon</param>
        /// <returns></returns>
        [HttpDelete("{addonId}")]
        public async Task<ActionResult<ResultDTO>> DeleteAddon(int addonId)
        {
            try
            {
                // Deleting Addon from db
                await _addonService.DeleteAddonAsync(addonId);

                var _response = new ResultDTO
                {
                    Success = true,
                    Message = "Addon deleted successfully."
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

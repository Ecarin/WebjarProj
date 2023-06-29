using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebjarProj.Models;
using WebjarProj.Models.Requests;
using WebjarProj.Models.Responses;
using WebjarProj.Services;
using WebjarProj.Services.Implementations;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<SingleAddonResponse>> GetAddonById(int id)
        {
            try
            {
                var addon = await _addonService.GetAddonByIdAsync(id);
                if (addon is null)
                {
                    var _response = new SingleAddonResponse
                    {
                        Success = true,
                        Message = "Addon not found."
                    };
                    return Ok(_response);
                }
                else
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

        [HttpGet]
        public async Task<ActionResult<AddonsResponse>> GetAllAddons()
        {
            try
            {
                var addons = await _addonService.GetAllAddonsAsync();
                if (addons is null || addons.Count == 0)
                {
                    var _response = new AddonsResponse
                    {
                        Success = true,
                        Message = "Addons not found.",
                    };
                    return Ok(_response);
                }
                else
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

        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateAddon(CreateAddonRequest request)
        {
            try
            {
                var addon = _mapper.Map<Addon>(request);
                await _addonService.CreateAddonAsync(addon);

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

        [HttpPut("{id}")]
        public async Task<ActionResult<ResultDTO>> UpdateAddon(int id, UpdateAddonRequest request)
        {
            try
            {
                var addon = _mapper.Map<Addon>(request);
                addon.Id = id;

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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultDTO>> DeleteAddon(int id)
        {
            try
            {
                await _addonService.DeleteAddonAsync(id);

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

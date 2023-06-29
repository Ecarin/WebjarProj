using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebjarProj.Models;
using WebjarProj.Models.Responses;
using WebjarProj.Services;
using WebjarProj.Services.Implementations;

namespace WebjarProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddonsController : ControllerBase
    {
        private readonly IAddonService _addonService;

        public AddonsController(IAddonService addonService)
        {
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
                var _response = new SingleAddonResponse()
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
                var _response = new AddonsResponse()
                {
                    Message = $"{e.HResult}: {e.Message}",
                };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateAddon(Addon addon)
        {
            await _addonService.CreateAddonAsync(addon);

            var response = new ResultDTO
            {
                Success = true,
                Message = "Addon created successfully."
            };

            return CreatedAtAction(nameof(GetAddonById), new { id = addon.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResultDTO>> UpdateAddon(int id, Addon addon)
        {
            if (id != addon.Id)
                return BadRequest();

            await _addonService.UpdateAddonAsync(addon);

            var response = new ResultDTO
            {
                Success = true,
                Message = "Addon updated successfully."
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultDTO>> DeleteAddon(int id)
        {
            await _addonService.DeleteAddonAsync(id);

            var response = new ResultDTO
            {
                Success = true,
                Message = "Addon deleted successfully."
            };

            return Ok(response);
        }
    }
}

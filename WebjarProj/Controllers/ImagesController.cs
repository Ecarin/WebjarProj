using Microsoft.AspNetCore.Mvc;
using WebjarProj.Models;
using WebjarProj.Models.Requests;
using WebjarProj.Models.Responses;

namespace WebjarProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ImageResponse>> PostPhoto([FromBody] ImageRequest request)
        {
            try
            {
                if (request is null)
                    return BadRequest("No image provided.");

                if (request.Base64Image.Length == 0)
                    return BadRequest("Image is empty.");

                // Decode the base64-encoded photo string.
                var bytes = Convert.FromBase64String(request.Base64Image);

                // Generate a unique filename for the photo.
                var filename = $"{Guid.NewGuid()}.png";
                string path = $"SavedImages";

                // Create directory if not exists
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Save the image to disk.
                await System.IO.File.WriteAllBytesAsync($"{path}//{filename}", bytes);

                // Return a response containing path.
                var _response = new ImageResponse()
                {
                    Success = true,
                    Message = "Image saved successfuly.",
                    ImagePath = $"{path}//{filename}",
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
using Microsoft.AspNetCore.Mvc;
using WWTBA.Core.DTOs;

namespace WWTBA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}


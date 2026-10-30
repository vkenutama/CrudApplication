using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected string? CurrentUserId
        => User.FindFirstValue(ClaimTypes.NameIdentifier);

    protected IActionResult ApiResponse<T>(T? data, string message = "Success")
    {
        return Ok(new
        {
            Success = true,
            Message = message,
            Data = data
        });
    }

    protected IActionResult ApiError(string error, int statusCode = StatusCodes.Status400BadRequest)
    {
        return StatusCode(statusCode, new
        {
            Success = false,
            Error = error
        });
    }
}
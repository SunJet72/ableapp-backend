using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{

    public RouteController()
    {
        
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Yay!");
    }
}

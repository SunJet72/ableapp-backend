using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Model;
using TodoApi.Model.HttpClasses;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private RouteService routeService;

    public RouteController(RouteService rS)
    {
        routeService = rS;
    }
    
    [HttpPost("route")]
    public async Task<IActionResult> GetRouteAsync([FromBody] RouteRequest routeRequest)
    {
        if(routeRequest == null){
            return BadRequest();
        }
        Way? way = await routeService.GetRouteAsync(routeRequest.Start, routeRequest.End);
        if(way==null){
            return BadRequest();
        }
        return Ok(way);
    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Model;
using TodoApi.Model.HttpClasses;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private RouteService routeService;
    private IReportService reportService;

    public ApiController(RouteService rS, IReportService repSer)
    {
        routeService = rS;
        reportService = repSer;
    }
    
    [HttpPost("route")]
    public async Task<IActionResult> GetRouteAsync([FromBody] RouteRequest routeRequest)
    {
        if(routeRequest == null){
            return BadRequest();
        }
        Way? way = await routeService.GetRouteAsync(
            routeRequest.Start, routeRequest.End, routeRequest.SandState, routeRequest.GravelState);
        if(way==null){
            return BadRequest();
        }
        return Ok(way);
    }

    [HttpPost("addreport")]
    public IActionResult AddReport([FromBody] ReportRequest repRec)
    {
        if(repRec == null){
            return BadRequest();
        }
        reportService.AddReport(repRec.Adress);
        return Ok("Report reported");
    }

    [HttpPost("getreport")]
    public IActionResult GetReport([FromBody] ReportRequest repRec)
    {
        if(repRec == null){
            return BadRequest();
        }
        return Ok(reportService.GetReport(repRec.Adress));
    }
}

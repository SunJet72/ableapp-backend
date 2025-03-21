using TodoApi.Model;

namespace TodoApi.Services;

public class RouteService
{
    private readonly string baseUrl = "https://graphhopper.com/api/1/route";

    private HttpClient httpClient;

    public RouteService()
    {
        httpClient = new HttpClient();
    }

    public Way GetRoute(Point start, Point end)
    {
        return new Way();
    }
}
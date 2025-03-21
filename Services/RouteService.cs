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

    public async Task<Way> GetRouteAsync(Point start, Point end)
    {
        return new Way();
    }
}
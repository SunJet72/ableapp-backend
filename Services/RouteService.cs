using System.Net;
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

    public async Task<Way?> GetRouteAsync(Point start, Point end)
    {
        string staticParams = "?instructions=true&vehicle=foot&key=LijBPDQGfu7Iiq80w3HzwB4RUDJbMbhs6BU0dEnn";
        HttpResponseMessage response = await httpClient.GetAsync(baseUrl + "?" + staticParams +
            "&point=" + start + "&point=" + end);
        if(response.StatusCode != HttpStatusCode.OK)
        {
            Console.WriteLine("Status code: " + response.StatusCode);
            return null;
        }

        GraphHopperRoute? json = await response.Content.ReadFromJsonAsync<GraphHopperRoute>();
        if(json == null)
        {
            Console.WriteLine("Json empty");
            return null;
        }
        List<Way> ways = [];
        foreach(JsonPath jsonPath in json.paths)
        {
            Way way = new();
            foreach(Instruction instruction in jsonPath.instructions)
            {
                if(instruction.distance <= 0) break;
                Model.Path path = new();
                path.Duration = instruction.time;
                
                for(int i = instruction.interval[0] - 1; i < instruction.interval[1]; i++)
                {
                    Point p = new() { 
                        Lat = jsonPath.points.coordinates[i][0], 
                        Lon = jsonPath.points.coordinates[i][1],
                        Height = jsonPath.points.coordinates[i][2] 
                        };
                    path.Points.Add(p);
                }
                way.Paths.Add(path);
            }
            ways.Add(way);
        }    
        return ways.FirstOrDefault();
    }
}
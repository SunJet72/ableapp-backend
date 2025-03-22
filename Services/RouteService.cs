using System.Net;
using System.Text.Json;
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
        string staticParams = "details=surface&details=time&elevation=true&points_encoded=false&instructions=false&vehicle=foot&key=LijBPDQGfu7Iiq80w3HzwB4RUDJbMbhs6BU0dEnn";
        HttpResponseMessage response = await httpClient.GetAsync(baseUrl + "?" + staticParams +
            "&point=" + start + "&point=" + end);
        if(response.StatusCode != HttpStatusCode.OK)
        {
            Console.WriteLine("Status code: " + response.StatusCode);
            return null;
        }
        Console.WriteLine("Response:\n" + await response.Content.ReadAsStringAsync());
        GraphHopperRoute? json = null;
        try 
        {
            json = await response.Content.ReadFromJsonAsync<GraphHopperRoute>();
        }catch(Exception e){
            Console.WriteLine("Failed to read:" + e.Message);
        }

        if(json == null)
        {
            Console.WriteLine("Json empty");
            return null;
        }
        List<Way> ways = [];
        foreach(JsonPath jsonPath in json.paths)
        {
            Way way = new();
            foreach(List<object> surface in jsonPath.details.surface)
            {
                Model.Path path = new();

                int from = ((JsonElement)surface[0]).GetInt32();
                int to = ((JsonElement)surface[1]).GetInt32();
                path.Terrain = ((JsonElement)surface[2]).GetString() switch
                {
                    "gravel" => TerrainTypeEnum.Gravel,
                    "sand" => TerrainTypeEnum.Sand,
                    _ => TerrainTypeEnum.Normal
                };

                path.Duration = jsonPath.details.time.Where(t => t[0] >= from && t[1] <= to).Select(t => t[2]).Sum();
                for(int i = from; i < to; i++)
                {
                    try
                    {
                        Point p = new() { 
                            Lat = jsonPath.points.coordinates[i][0], 
                            Lon = jsonPath.points.coordinates[i][1],
                            Height = jsonPath.points.coordinates[i][2] 
                            };
                        path.Points.Add(p);
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message + "\nIndex: " + i);
                    }
                }
                way.Paths.Add(path);
                way.Distance = jsonPath.distance;
            }
            ways.Add(way);
        }    
        return ways.FirstOrDefault();
    }
}
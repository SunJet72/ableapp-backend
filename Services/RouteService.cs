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
        //string staticParams = "details=surface&details=time&elevation=true&points_encoded=false&instructions=false&vehicle=foot&key=LijBPDQGfu7Iiq80w3HzwB4RUDJbMbhs6BU0dEnn";
        // string jsonBody = """
        //     {
        //         "vehicle": "foot",
        //         "algorithm": "alternative_route",
        //         "instructions": false,
        //         "locale": "en",
        //         "elevation": true,
        //         "turn_costs": false,
        //         "points_encoded": false,
        //         "ch.disable": true,
        //         "alternative_route.max_paths": 3,
        //         "points": [
        //             [
        //         """ + start +
        //         " ],[" + end + "] ]," +
        //         """    
        //         "details": [
        //             "surface",
        //             "time",
        //             "road_class"
        //         ]
        //     }
        //     """;

        GraphHopperRequest request = new()
        {
            points = [[start.Lon, start.Lat], [end.Lon, end.Lat]]
        };
        
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(baseUrl + "?key=LijBPDQGfu7Iiq80w3HzwB4RUDJbMbhs6BU0dEnn", request);
        if(response.StatusCode != HttpStatusCode.OK)
        {
            Console.WriteLine("Status code: " + response.StatusCode);
            return null;
        }
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
            List<List<object>> steps = jsonPath.details.road_class.Where(c => ((JsonElement)c[2]).GetString() == "steps").ToList();

            int stepsCount = 0;
            foreach(List<object> surface in jsonPath.details.surface)
            {
                Model.Path path = new();

                int stepsFrom = -1, stepsTo = -1;
                if(stepsCount < steps.Count())
                {
                    stepsFrom = ((JsonElement)steps[stepsCount][0]).GetInt32();
                    stepsTo = ((JsonElement)steps[stepsCount][1]).GetInt32();
                }

                int from = ((JsonElement)surface[0]).GetInt32();
                int to = ((JsonElement)surface[1]).GetInt32();
                TerrainTypeEnum terrain = ((JsonElement)surface[2]).GetString() switch
                            {
                                "gravel" => TerrainTypeEnum.Gravel,
                                "cobblestone" => TerrainTypeEnum.Sand,
                                _ => TerrainTypeEnum.Normal
                            };

                for(int i = from; i <= to; i++)
                {
                    // try
                    // {
                        Point p = new() { 
                            Lat = jsonPath.points.coordinates[i][1], 
                            Lon = jsonPath.points.coordinates[i][0],
                            Height = jsonPath.points.coordinates[i][2] 
                            };
                        path.Points.Add(p);
                        if(stepsFrom != from && i == stepsFrom && i != from && i != to) 
                        {
                            path.Terrain = terrain;
                            path.IsSteps = false;
                            path.Duration = jsonPath.details.time.Where(t => t[0] >= i - path.Points.Count() + 1 && t[1] <= i).Select(t => t[2]).Sum();
                            path.Distance = jsonPath.details.distance.Where(t => t[0] >= i - path.Points.Count() + 1 && t[1] <= i).Select(t => t[2]).Sum();
                            way.Paths.Add(path);
                            stepsCount++;
                            if(stepsCount < steps.Count())
                            {
                                stepsFrom = ((JsonElement)steps[stepsCount][0]).GetInt32();
                                stepsTo = ((JsonElement)steps[stepsCount][1]).GetInt32();
                            }
                            path = new();
                            path.Points.Add(p);                       }
                        if (stepsTo != to && i == stepsTo && i != to){
                            path.Terrain = terrain;
                            path.IsSteps = true;
                            path.Duration = jsonPath.details.time.Where(t => t[0] >= i - path.Points.Count() + 1 && t[1] <= i).Select(t => t[2]).Sum();
                            path.Distance = jsonPath.details.distance.Where(t => t[0] >= i - path.Points.Count() + 1 && t[1] <= i).Select(t => t[2]).Sum();
                            way.Paths.Add(path);
                            stepsCount++;
                            if(stepsCount < steps.Count())
                            {
                                stepsFrom = ((JsonElement)steps[stepsCount][0]).GetInt32();
                                stepsTo = ((JsonElement)steps[stepsCount][1]).GetInt32();
                            }
                            path = new();
                            path.Points.Add(p);
                        }
                    // } catch (Exception e)
                    // {
                    //     Console.WriteLine(e.Message + "\nIndex: " + i);
                    // }
                    
                }
                if(path.Points.Count() > 1)
                {
                    path.Duration = jsonPath.details.time.Where(t => t[0] >= to - path.Points.Count() + 1 && t[1] <= to).Select(t => t[2]).Sum();
                    path.Distance = jsonPath.details.distance.Where(t => t[0] >= to - path.Points.Count() + 1 && t[1] <= to).Select(t => t[2]).Sum();
                    
                    if(stepsFrom == from) {
                        path.IsSteps = true;
                        stepsCount++;
                    }
                    way.Paths.Add(path);
                }
                
            }
            way.Distance = jsonPath.distance;
            way.Duration = jsonPath.time;
            ways.Add(way);
        }    
        return ways.FirstOrDefault();
    }
}
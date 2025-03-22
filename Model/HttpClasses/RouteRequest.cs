using System.Text.Json.Serialization;

namespace TodoApi.Model.HttpClasses;

public class RouteRequest
{
    [JsonPropertyName("start")]
    public Point Start {get;set;}
    [JsonPropertyName("end")]
    public Point End {get;set;}    
    

    [JsonPropertyName("stairsCount")]
    public StairsEnum StairsCount {get; set;}
    [JsonPropertyName("sandState")]
    public TerrainEnum SandState {get; set;}
    [JsonPropertyName("gravelState")]
    public TerrainEnum GravelState {get; set;}
}
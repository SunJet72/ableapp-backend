using System.Text.Json.Serialization;

namespace TodoApi.Model.HttpClasses;

public class RouteRequest
{
    [JsonPropertyName("start")]
    public Point Start {get;set;}
    [JsonPropertyName("end")]
    public Point End {get;set;}    
}
using System.Text.Json.Serialization;

namespace TodoApi.Model;

public class Way
{
    [JsonPropertyName("paths")]
    public List<Path> Paths { get; set; } = [];
}
using System.Text.Json.Serialization;

namespace TodoApi.Model;

public class Path
{
    public List<Point> Points { get; set; } = [];
    public TerrainTypeEnum Terrain { get; set; } = TerrainTypeEnum.Normal;
    public int Duration { get; set; } = 0;
    
    [JsonIgnore]
    public double Distance { get; set; } = 0;


    [JsonPropertyName("is_steps")]
    public bool IsSteps { get; set; } = false;
    
    [JsonIgnore]
    public int StepsCount { get; set; } = 0;
}
namespace TodoApi.Model;

public class Path
{
    public List<Point> Points { get; set; } = [];
    public string Terrain { get; set; } = "asphalt";
    public int StepsCount { get; set; } = 0;
    public int Duration { get; set; } = 0;
}
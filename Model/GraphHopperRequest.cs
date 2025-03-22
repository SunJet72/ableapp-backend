// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;

public class CustomModel
{
    [JsonPropertyName("speed")]
    public List<Speed> speed { get; set; }
}

public class GraphHopperRequest
{
    [JsonPropertyName("vehicle")]
    public string vehicle { get; set; } = "foot";

    [JsonPropertyName("algorithm")]
    public string algorithm { get; set; } = "alternative_route";

    [JsonPropertyName("instructions")]
    public bool instructions { get; set; } = false;

    [JsonPropertyName("elevation")]
    public bool elevation { get; set; } = true;

    [JsonPropertyName("points_encoded")]
    public bool points_encoded { get; set; } = false;

    [JsonPropertyName("ch.disable")]
    public bool chdisable { get; set; } = true;

    [JsonPropertyName("alternative_route.max_paths")]
    public int alternative_routemax_paths { get; set; } = 3;

    [JsonPropertyName("details")]
    public List<string> details { get; set; } = ["surface","time", "road_class"];

    [JsonPropertyName("points")]
    public List<List<double>> points { get; set; }

    [JsonIgnore]
    public CustomModel custom_model { get; set; } = null;
}

public class Speed
{
    [JsonPropertyName("if")]
    public string @if { get; set; }

    [JsonPropertyName("multiply_by")]
    public string multiply_by { get; set; }
}


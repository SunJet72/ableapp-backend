using System.Text.Json.Serialization;

namespace TodoApi.Model;

public class Details
{
}

public class Hints
{
    [JsonPropertyName("visited_nodes.sum")]
    public int visited_nodessum { get; set; }

    [JsonPropertyName("visited_nodes.average")]
    public double visited_nodesaverage { get; set; }
}

public class Info
{
    [JsonPropertyName("copyrights")]
    public List<string> copyrights { get; set; }

    [JsonPropertyName("took")]
    public int took { get; set; }

    [JsonPropertyName("road_data_timestamp")]
    public DateTime road_data_timestamp { get; set; }
}

public class Instruction
{
    [JsonPropertyName("distance")]
    public double distance { get; set; }

    [JsonPropertyName("heading")]
    public double heading { get; set; }

    [JsonPropertyName("sign")]
    public int sign { get; set; }

    [JsonPropertyName("interval")]
    public List<int> interval { get; set; }

    [JsonPropertyName("text")]
    public string text { get; set; }

    [JsonPropertyName("time")]
    public int time { get; set; }

    [JsonPropertyName("street_name")]
    public string street_name { get; set; }

    [JsonPropertyName("last_heading")]
    public double? last_heading { get; set; }
}

public class JsonPath
{
    [JsonPropertyName("distance")]
    public double distance { get; set; }

    [JsonPropertyName("weight")]
    public double weight { get; set; }

    [JsonPropertyName("time")]
    public int time { get; set; }

    [JsonPropertyName("transfers")]
    public int transfers { get; set; }

    [JsonPropertyName("points_encoded")]
    public bool points_encoded { get; set; }

    [JsonPropertyName("bbox")]
    public List<double> bbox { get; set; }

    [JsonPropertyName("points")]
    public Points points { get; set; }

    [JsonPropertyName("instructions")]
    public List<Instruction> instructions { get; set; }

    [JsonPropertyName("legs")]
    public List<object> legs { get; set; }

    [JsonPropertyName("details")]
    public Details details { get; set; }

    [JsonPropertyName("ascend")]
    public double ascend { get; set; }

    [JsonPropertyName("descend")]
    public double descend { get; set; }

    [JsonPropertyName("snapped_waypoints")]
    public SnappedWaypoints snapped_waypoints { get; set; }
}

public class Points
{
    [JsonPropertyName("type")]
    public string type { get; set; }

    [JsonPropertyName("coordinates")]
    public List<List<double>> coordinates { get; set; }
}

public class GraphHopperRoute
{
    [JsonPropertyName("hints")]
    public Hints hints { get; set; }

    [JsonPropertyName("info")]
    public Info info { get; set; }

    [JsonPropertyName("paths")]
    public List<JsonPath> paths { get; set; }
}

public class SnappedWaypoints
{
    [JsonPropertyName("type")]
    public string type { get; set; }

    [JsonPropertyName("coordinates")]
    public List<List<double>> coordinates { get; set; }
}


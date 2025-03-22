using System.Text.Json.Serialization;

namespace TodoApi.Model;

public class Point
{
    [JsonIgnore]
    public int Id {get; set;}
    public double Lat {get;set;}
    public double Lon {get;set;}
    [JsonIgnore]
    public double Height { get; set;}

    public override string ToString()
    {
        return Lon.ToString() + "," + Lat.ToString();
    }
}
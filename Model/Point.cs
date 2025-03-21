using System.Text.Json.Serialization;

namespace TodoApi.Model;

public class Point
{
    public double Lat {get;set;}
    public double Lon {get;set;}
    [JsonIgnore]
    public double Height { get; set;}

    public override string ToString()
    {
        return Lat.ToString() + "%2C" + Lon.ToString();
    }
}
using System.Text.Json.Serialization;

namespace TodoApi.Model.HttpClasses;

public class ReportRequest
{
    [JsonPropertyName("adress")]
    public string Adress {get;set;}    
}
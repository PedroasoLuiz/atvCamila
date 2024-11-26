namespace atvCamila.Models
{
    public class Permissions
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int id { get; set; }
        public string? description { get; set; }
    }
}

namespace atvCamila.Models
{
    public class Groups
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public DateTime date { get; set; }
        public int fk_systems { get; set; }
        public int fk_permissions { get; set; }
    }
}

namespace atvCamila.Models
{
    public class Users
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int id { get; set; }
        public string? name { get; set; }
        public string? fkUser { get; set; }
        public string? email { get; set; }
        public int fk_groups { get; set; }
    }
}

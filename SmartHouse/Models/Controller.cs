using PropertyChanged;
using Newtonsoft.Json;

namespace SmartHouse.Models
{
    public class Controller
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

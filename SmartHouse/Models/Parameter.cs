using PropertyChanged;
using Newtonsoft.Json;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Parameter
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}

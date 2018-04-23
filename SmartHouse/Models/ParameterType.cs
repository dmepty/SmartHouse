using PropertyChanged;
using Newtonsoft.Json;


namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ParameterType
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

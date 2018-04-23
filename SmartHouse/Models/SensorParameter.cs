using PropertyChanged;
using Newtonsoft.Json;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class SensorParameter
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("id_parameter")]
        public int IdParameter { get; set; }

        [JsonProperty("name_parameter")]
        public string NameParameter { get; set; }

        [JsonProperty("id_sensor")]
        public int IdSensor { get; set; }

        [JsonProperty("id_type_parameter")]
        public int IdTypeParameter { get; set; }

        [JsonProperty("name_type_parameter")]
        public string NameTypeParameter { get; set; }
    }
}

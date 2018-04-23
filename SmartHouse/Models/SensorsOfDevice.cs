using PropertyChanged;
using Newtonsoft.Json;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class SensorsOfDevice
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("id_template_device")]
        public int IdTemplateDevice { get; set; }

        [JsonProperty("code_parameter")]
        public string CodeParameter { get; set; }

        [JsonProperty("name_sensor")]
        public string NameSensor { get; set; }

        [JsonProperty("name_parameter")]
        public string NameParameter { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id_in_device")]
        public int IdInDevice { get; set; }

        [JsonProperty("id_sensor")]
        public int IdSensor { get; set; }

        [JsonProperty("id_parameter")]
        public int IdParameter { get; set; }

    }
}

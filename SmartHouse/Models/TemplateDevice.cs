using PropertyChanged;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class TemplateDevice
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id_controllers")]
        public int IdControllers { get; set; }

        [JsonProperty("id_template_firmware")]
        public int IdTemplateFirmware { get; set; }



        public TemplateFirmware TemplatesFirmwares { get; set; }
        public Controller Controllers { get; set; }
        public ObservableCollection<SensorsOfDevice> SensorsOfDevices { get; set; } = new ObservableCollection<SensorsOfDevice>();



        public bool IsEdit { get; set; }
        public bool IsNewSensor { get; set; }
    }
}

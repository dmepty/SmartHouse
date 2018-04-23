using System.Collections.Generic;

using PropertyChanged;
using Newtonsoft.Json;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Device
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("id_template_firmware")]
        public int IdTemplateFirmware { get; set; }

        [JsonProperty("id_template")]
        public int IdTemplateDevice { get; set; }

        [JsonProperty("template_device_name")]
        public string TemplateDeviceName { get; set; }

        [JsonProperty("code_device")]
        public string CodeDevice { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }



        public TemplateFirmware TemplatesFirmwares { get; set; }
        public TemplateDevice TemplateDevice { get; set; }
        public List<SensorsOfDevice> SensorsOfDevices { get; set; } = new List<SensorsOfDevice>();

        

        public bool IsNew { get; set; }
        public bool IsEdit { get; set; }
    }
}

using System;
using PropertyChanged;
using Newtonsoft.Json;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Firmware
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }

        [JsonProperty("text_template")]
        public string TextTemplate { get; set; }

        [JsonProperty("date_modification")]
        public DateTime DateModification { get; set; }
    }
}

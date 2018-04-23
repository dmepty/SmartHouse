using System;
using PropertyChanged;
using Newtonsoft.Json;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class TemplateFirmware
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("text_template")]
        public string TextTemplate { get; set; }

        [JsonProperty("date_modification")]
        public DateTime DateModification { get; set; }


        public bool IsEdit { get; set; }
    }
}

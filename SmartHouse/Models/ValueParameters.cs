using System;
using Newtonsoft.Json;
using PropertyChanged;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    class ValueParameters
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("id_parameter")]
        public int IdParameter { get; set; }

        [JsonProperty("id_device")]
        public int IdDevice { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("code_parameter")]
        public string CodeParameter { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}

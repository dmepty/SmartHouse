using System.Collections.ObjectModel;
using PropertyChanged;
using Newtonsoft.Json;

namespace SmartHouse.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Sensor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }



        public ObservableCollection<SensorParameter> SensorParameters { get; set; } = new ObservableCollection<SensorParameter>();

        public bool IsNew { get; set; }
        public bool IsEdit { get; set; }
        public bool IsNewParameter { get; set; }
    }
}

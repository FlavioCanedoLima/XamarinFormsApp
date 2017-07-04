using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsApp.Models
{
    public class FacebookProfile
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "cover")]
        public Cover Cover { get; set; }
        [JsonProperty(PropertyName = "picture")]
        public Picture Picture { get; set; }
    }

    public class Cover
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "offset_y")]
        public int Offset_y { get; set; }
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }
    }

    public class Picture
    {
        [JsonProperty(PropertyName = "data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }
        [JsonProperty(PropertyName = "is_silhouette")]
        public bool Is_silhouette { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
    }
}

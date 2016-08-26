using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents.Models
{

    public class Speaker : ModelBase
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("blog")]
        public string Blog { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("webiste")]
        public string Webiste { get; set; }

        [JsonProperty("titile")]
        public string Titile { get; set; }

        [JsonProperty("biography")]
        public string Biography { get; set; }
    }

}

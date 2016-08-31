using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents.Models
{
    public class Feedback : ModelBase
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }
}

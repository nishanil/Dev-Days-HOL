using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvents.Server.DataObjects
{
    public class Feedback : EntityData
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
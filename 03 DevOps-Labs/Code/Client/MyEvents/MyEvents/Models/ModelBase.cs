using Microsoft.WindowsAzure.MobileServices;
using MyEvents.Common;
using Newtonsoft.Json;
using System;

namespace MyEvents.Models
{
    public class ModelBase : ObservableObject
    {
        public ModelBase()
        {
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }


        [Version]
        public string Version { get; set; }
    }
}

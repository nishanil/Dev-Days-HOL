using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvents.Server.DataObjects
{
    public class Session : EntityData
    {
        public DateTime End { get; set; }

        public DateTime Start { get; set; }

        public string Abstract { get; set; }

        public string Title { get; set; }

        public string Presenter { get; set; }

        public string Biography { get; set; }

        public string Image { get; set; }

        public string Avatar { get; set; }

        public string Room { get; set; }
    }
}
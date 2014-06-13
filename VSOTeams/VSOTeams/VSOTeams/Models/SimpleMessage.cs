using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VSOTeams.Models
{
    public class SimpleRoomMessage
    {
        public string PostedByDisplayName { get; set; }
        public string Content { get; set; }
        public ImageSource MessageTypeURI { get; set; }
        public DateTime postedTime { get; set; }
        public string Url { get; set; }

        public TeamRoomMessage message { get; set; }
    }
}

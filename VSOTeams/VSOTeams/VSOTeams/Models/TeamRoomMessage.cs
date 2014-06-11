using System;
using System.Collections.Generic;
using System.Text;

namespace VSOTeams.Models
{
    public class TeamRoomMessage
    {
        public class PostedBy
        {
            public string id { get; set; }
            public string displayName { get; set; }
            public string url { get; set; }
            public string imageUrl { get; set; }
        }


        public int id { get; set; }
        public string content { get; set; }
        public string messageType { get; set; }
        public string postedTime { get; set; }
        public int postedRoomId { get; set; }
        public PostedBy postedBy { get; set; }
    }

    public class TeamRoomMessages
    {
        public int count { get; set; }
        public List<TeamRoomMessage> value { get; set; }
    }
}

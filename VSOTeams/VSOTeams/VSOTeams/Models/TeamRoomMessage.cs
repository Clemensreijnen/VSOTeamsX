using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VSOTeams.Models
{
    public class TeamRoomMessage
    {
        public int id { get; set; }
        public Content content { get; set; }
        public string messageType { get; set; }
        public DateTime postedTime { get; set; }
        public int postedRoomId { get; set; }
        public PostedBy postedBy { get; set; }

        public override string ToString()
        {
            return content.ToString();
        }

        public class PostedBy
        {
            public string id { get; set; }
            public string displayName { get; set; }
            public string url { get; set; }
            public string imageUrl { get; set; }
        }

        public class Content
        {
            public class System : Content
            {
                public string content { get; set; }
                public override string ToString()
                {
                    return content;
                }
            }

            public class Normal : Content
            {
                public string content { get; set; }
                public override string ToString()
                {
                    return content;
                }
            }

            public class Notification : Content
            {
                public class CheckinData
                {
                    public class Owner
                    {
                        public string name { get; set; }
                        public string displayName { get; set; }
                    }

                    public class Committer
                    {
                        public string name { get; set; }
                        public string displayName { get; set; }
                    }

                    public class Changes
                    {
                        public int add { get; set; }
                        public int delete { get; set; }
                        public int edit { get; set; }
                    }

                    public int changeSetNumber { get; set; }
                    public string comment { get; set; }
                    public Owner owner { get; set; }
                    public Committer committer { get; set; }
                    public Changes changes { get; set; }

                }

                public class CheckinEvent : Notification
                {
                    public CheckinData data { get; set; }
                    public override string ToString()
                    {
                        var result = data.comment;
                        return result;
                    }
                }


                public class BuildCompletedData
                {
                    public class BuildDefinition
                    {
                        public string name { get; set; }
                        public string uri { get; set; }
                    }

                    public class BuildReason
                    {
                        public string reason { get; set; }
                        public string requestedBy { get; set; }
                        public string requestedFor { get; set; }
                    }

                    public string buildNumber { get; set; }
                    public string buildUri { get; set; }
                    public BuildDefinition buildDefinition { get; set; }
                    public string buildStatus { get; set; }
                    public int buildStatusId { get; set; }
                    public BuildReason buildReason { get; set; }
                }

                public class BuildCompletedEvent : Notification
                {
                    public BuildCompletedData data { get; set; }
                    public override string ToString()
                    {
                        var result = String.Format("{0} triggered {1}", data.buildReason.requestedFor, data.buildDefinition.name);
                        return result;
                    }
                }

                public class WorkItemChangedData
                {
                    public int id { get; set; }
                    public int rev { get; set; }
                    public string workItemType { get; set; }
                    public string title { get; set; }
                    public string changedBy { get; set; }
                    public bool isNewWorkItem { get; set; }
                    public string stateChangedValue { get; set; }
                }

                public class WorkItemChangedEvent : Notification
                {
                    public WorkItemChangedData data { get; set; }
                    public override string ToString()
                    {
                        return data.title;
                    }
                }

                public string type { get; set; }
                public int id { get; set; }
                public string title { get; set; }
                public string url { get; set; }

                public override string ToString()
                {
                    return title;
                }
            }
        }
    }

    public class TeamRoomMessages
    {
        public int count { get; set; }
        public List<TeamRoomMessage> value { get; set; }
    }
}

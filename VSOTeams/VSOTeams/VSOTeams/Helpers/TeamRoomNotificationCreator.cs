using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Models;

namespace VSOTeams.Helpers
{
    public class TeamRoomNotificationCreator : CustomCreationConverter<TeamRoomMessage.Content.Notification>
    {
        public override TeamRoomMessage.Content.Notification Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.Equals(typeof(TeamRoomMessage.Content.Notification)))
                return true;
            return false;
        }

        public TeamRoomMessage.Content.Notification Create(Type objectType, JObject jsonObject)
        {
            var type = jsonObject["type"];
            switch (type.ToString())
            {
                case "BuildCompletedEvent":
                    return new TeamRoomMessage.Content.Notification.BuildCompletedEvent();
                case "CheckinEvent":
                    return new TeamRoomMessage.Content.Notification.CheckinEvent();
                case "WorkItemChangedEvent":
                    return new TeamRoomMessage.Content.Notification.WorkItemChangedEvent();
                default:
                    return new TeamRoomMessage.Content.Notification();
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Create(objectType, jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }
    }

   
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Models;

namespace VSOTeams.Helpers
{
    public class TeamRoomMessageCreator : CustomCreationConverter<TeamRoomMessage>
    {
        public override TeamRoomMessage Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.Equals(typeof(TeamRoomMessage)))
                return true;
            return false;
        }

        public TeamRoomMessage Create(Type objectType, JObject jsonObject)
        {
            var messagetype = jsonObject["messageType"].ToString();
            var contentstring = jsonObject["content"].ToString();
            jsonObject.Remove("content");
            var result = new TeamRoomMessage();

            switch (messagetype)
            {
                case "system":
                    result.content = new TeamRoomMessage.Content.System() { content = contentstring };
                    break;

                case "normal":
                    result.content = new TeamRoomMessage.Content.Normal() { content = contentstring };
                    break;

                case "notification":
                    result.content = JsonConvert.DeserializeObject<TeamRoomMessage.Content.Notification>(contentstring, new TeamRoomNotificationCreator());
                    break;

                default:
                    break;
            }
            return result;
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

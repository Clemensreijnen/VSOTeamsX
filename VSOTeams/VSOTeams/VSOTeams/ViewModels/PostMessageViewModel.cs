using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using VSOTeams.Helpers;
using VSOTeams.Models;
using Xamarin.Forms;

namespace VSOTeams.ViewModels
{
    class PostMessageViewModel : BaseViewModel
    {
        private TeamRoom _room ;
        public PostMessageViewModel(TeamRoom room)
        {
            _room = room;
        }
        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set { message = value; OnPropertyChanged("Message"); }
        }

        private Command post;
        public Command Post
        {
            get { return post ?? (post= new Command(ExecutePostCommand)); }
        }

        private async void ExecutePostCommand()
        {
            string uriString = String.Format("/DefaultCollection/_apis/Chat/rooms/{0}/messages", _room.id);

            var messageRequestPOSTData =
                        new MessageRequest()
                        {
                                Content = message
                        };

             HttpContent content = new StringContent(
                    JsonConvert.SerializeObject(messageRequestPOSTData),
                    Encoding.UTF8,
                    "application/json");

            var responseBody = await HttpClientHelper.PostToVSO(uriString, content);

        }
    }
    public class MessageRequest
    {
        public string Content { get; set; }
    }


}

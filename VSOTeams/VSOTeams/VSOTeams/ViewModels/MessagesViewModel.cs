using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Helpers;
using VSOTeams.Models;
using Xamarin.Forms;

namespace VSOTeams.ViewModels
{
    public class MessagesViewModel: BaseViewModel
    {
        private TeamRoom room = null;
        public ObservableCollection<SimpleRoomMessage> TeamRoomMessages { get; set; }

        public MessagesViewModel(TeamRoom roomRequest)
        {
            Title = "Messages";
            MessageToShow = false;
            room = roomRequest;
            TeamRoomMessages = new ObservableCollection<SimpleRoomMessage>();
        }

        private Command loadTeamRoomMessagesCommand;

        public Command LoadTeamRoomMessagesCommand
        {
            get { return loadTeamRoomMessagesCommand ?? (loadTeamRoomMessagesCommand = new Command(GetTeamRoomMessages)); }
        }

        private async void GetTeamRoomMessages()
        {
             if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TeamRoomMessages.Clear();

                string uriString = String.Format("/DefaultCollection/_apis/Chat/rooms/{0}/messages",room.id);
                var responseBody = await HttpClientHelper.RequestVSO(uriString);

                var teamroommessagebaselist = JsonConvert.DeserializeObject<TeamRoomMessages>(responseBody, new Helpers.TeamRoomMessageCreator());
                if (teamroommessagebaselist.count == 0)
                {
                    MessageToShow = true;
                    MessageToShowText = "No activity in the teamroom.";
                }
                IEnumerable<TeamRoomMessage> messages = teamroommessagebaselist.value;
                var BuildCompletedEventImage = new Image { Source = new FileImageSource { File = "buildcompletedevent.png" } };
                var BuildCompletedEventImageBig = new Image { Source = new FileImageSource { File = "buildcompletedevent1.png" } };
                var workitemchangedeventImage = new Image { Source = new FileImageSource { File = "workitemchangedevent.png" } };
                var workitemchangedeventImageBig = new Image { Source = new FileImageSource { File = "workitemchangedevent1.png" } };
                var checkineventImage = new Image { Source = new FileImageSource { File = "checkinevent.png" } };
                var checkineventImageBig = new Image { Source = new FileImageSource { File = "checkinevent1.png" } }; 
                
                foreach (var item in messages)
                {
                    SimpleRoomMessage sm = new SimpleRoomMessage();
                    sm.message = item;
                    sm.Content = item.content.ToString();

                    sm.PostedByDisplayName = item.postedBy.displayName;
                    sm.PostedByImageUrl = item.postedBy.imageUrl;
                    
                    if (item.content is TeamRoomMessage.Content.System)
                    {
                        //enter leave messages, don't show them
                    }

                    if(item.content is TeamRoomMessage.Content.Normal)
                    {
#if __ANDROID__
                        ImageSource imgResult = new FileImageSource { File = "Badge.png" } ;
#else
                        ImageSource imgResult = await VSOTeams.Helpers.FileHelper.DownloadImage(new Uri(sm.PostedByImageUrl), item.postedBy.id + ".png");
#endif
                        sm.PostedByImageSource = imgResult;
                        sm.MessageTypeURI = imgResult;
                        sm.MessageTypeURIBig = imgResult;
                        TeamRoomMessages.Add(sm);
                    }
                    if(item.content is TeamRoomMessage.Content.Notification.BuildCompletedEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.BuildCompletedEvent)item.content;
                        sm.MessageTypeURI = BuildCompletedEventImage.Source;
                        sm.MessageTypeURIBig = BuildCompletedEventImageBig.Source;
                        sm.Url = content.url;
                        TeamRoomMessages.Add(sm);
                    }

                    if (item.content is TeamRoomMessage.Content.Notification.WorkItemChangedEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.WorkItemChangedEvent)item.content;
                        sm.MessageTypeURI = workitemchangedeventImage.Source;
                        sm.MessageTypeURIBig = workitemchangedeventImageBig.Source;
                        sm.Url = content.url;
                        TeamRoomMessages.Add(sm);
                    }

                    if (item.content is TeamRoomMessage.Content.Notification.CheckinEvent)
                    {
                        var content = (TeamRoomMessage.Content.Notification.CheckinEvent)item.content;
                        sm.MessageTypeURI = checkineventImage.Source;
                        sm.MessageTypeURIBig = checkineventImageBig.Source;
                        sm.Url = content.url;
                        TeamRoomMessages.Add(sm);
                    }

                   
                }


            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", "Unable to load Visual Studio Online projects.", "OK", null);
            }

            IsBusy = false; 
        }

    }
}

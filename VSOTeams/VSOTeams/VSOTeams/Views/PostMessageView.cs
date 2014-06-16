using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Models;
using VSOTeams.ViewModels;
using Xamarin.Forms;

namespace VSOTeams.Views
{
    public class PostMessageView : BaseView
    {
        private PostMessageViewModel ViewModel
        {
            get { return BindingContext as PostMessageViewModel; }
        }

        public PostMessageView(TeamRoom room)
        {
            BindingContext = new PostMessageViewModel(room);
            Padding = new Thickness(20);
            Title = "Post message";

            Label header = new Label
            {
                Text = room.name,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center,
            };

            
            Entry messageInput = new Entry { Placeholder = "Message to the room" };
            messageInput.HeightRequest = 80;
            messageInput.SetBinding(Entry.TextProperty, "Message");

            Button postMesageToRoom = new Button
            {
                Text = "Post Message",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                Command = ViewModel.Post
            };


            this.Content = new StackLayout
            {
                Children = 
                {
                    header,
                    messageInput, 
                    postMesageToRoom
                }
            };
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Helpers;
using VSOTeams.Models;
using VSOTeams.ViewModels;
using Xamarin.Forms;

namespace VSOTeams.Views
{
    public class RoomMessagesView : BaseView
    {
        private MessagesViewModel ViewModel
        {
            get { return BindingContext as MessagesViewModel; }
        }
        public RoomMessagesView(TeamRoom room)
        {
            BindingContext = new MessagesViewModel(room);

            var add = new ToolbarItem
            {
                Command = ViewModel.LoadTeamRoomMessagesCommand,
                Icon = "add.png",
                Name = "Add",
                Priority = 0
            };
            ToolbarItems.Add(add);

            Label header = new Label
            {
                Text = room.name,
                Font = Font.SystemFontOfSize(36),
                HorizontalOptions = LayoutOptions.Center
            };

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 8, 0, 8)
            };

            var activity = new ActivityIndicator
            {
                Color = Helpers.Color.DarkBlue.ToFormsColor(),
                IsEnabled = true
            };

            activity.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
            activity.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

            stack.Children.Add(activity);

            ListView listView = new ListView();
            listView.ItemsSource = ViewModel.TeamRoomMessages;
            listView.ItemTemplate = new DataTemplate(typeof(MessageCell));

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;

                var message = listView.SelectedItem as SimpleRoomMessage;
                if (message.Url == "")
                    return;

                this.Navigation.PushAsync(new MessagesView(message));
                listView.SelectedItem = null;
            };

            stack.Children.Add(header);
            stack.Children.Add(listView);

            Content = stack;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || ViewModel.IsBusy)
                return;


            ViewModel.LoadTeamRoomMessagesCommand.Execute(null);

        }
    }
}

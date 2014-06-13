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


            Label header = new Label
            {
                Text = room.name,
                Font = Font.SystemFontOfSize(60),
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

            //listView.ItemTapped += (sender, args) =>
            //{
            //    if (listView.SelectedItem == null)
            //        return;
            //    var room = listView.SelectedItem as TeamRoom;
            //    this.Navigation.PushAsync(new RoomMessagesView(room));
            //    listView.SelectedItem = null;
            //};

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

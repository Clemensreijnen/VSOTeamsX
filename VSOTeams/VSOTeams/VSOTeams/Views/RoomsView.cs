using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Helpers;
using VSOTeams.Models;
using VSOTeams.ViewModels;
using Xamarin.Forms;

namespace VSOTeams.Views
{
    public class RoomsView : BaseView
    {
        private RoomsViewModel ViewModel
        {
            get { return BindingContext as RoomsViewModel; }
        }

        public RoomsView()
        {
            BindingContext = new RoomsViewModel();

            Label header = new Label
            {
                Text = "Teamrooms",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
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
            listView.ItemsSource = ViewModel.TeamRooms;
            listView.ItemTemplate = new DataTemplate(typeof(RoomsCell));

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                var room = listView.SelectedItem as TeamRoom;
                this.Navigation.PushAsync(new RoomMessagesView(room));
                listView.SelectedItem = null;
            };

            stack.Children.Add(header);
            stack.Children.Add(listView);
                  
            Content = stack;

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null ||  ViewModel.IsBusy )
                return;

            ViewModel.LoadTeamRoomCommand.Execute(null);

        }

    }
}

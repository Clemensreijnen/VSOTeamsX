using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Helpers;
using VSOTeams.Models;
using VSOTeams.ViewModels;
using Xamarin.Forms;

namespace VSOTeams.Views
{
    public class ProjectMembersView : BaseView
    {
        private ProjectMembersViewModel ViewModel
        {
            get { return BindingContext as ProjectMembersViewModel; }
        }

        public ProjectMembersView(Project prj)
        {
            BindingContext = new ProjectMembersViewModel(prj);

            Label header = new Label
            {
                Text = prj.Name,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center
            };

            Label screenMessage = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                TextColor = Xamarin.Forms.Color.Red,
                HorizontalOptions = LayoutOptions.Center
            };

            screenMessage.SetBinding(Label.TextProperty, "MessageToShowText");
            screenMessage.SetBinding(Label.IsVisibleProperty, "MessageToShow");


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
            listView.ItemsSource = ViewModel.TeamMembers;
            listView.ItemTemplate = new DataTemplate(typeof(TeamMemberCell));

            //listView.ItemTapped += (sender, args) =>
            //{
            //    if (listView.SelectedItem == null)
            //        return;

            //    var message = listView.SelectedItem as SimpleRoomMessage;
            //    this.Navigation.PushAsync(new MessagesView(message));
            //    listView.SelectedItem = null;
            //};

            stack.Children.Add(header);
            stack.Children.Add(screenMessage);
            stack.Children.Add(listView);

            Content = stack;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || ViewModel.IsBusy)
                return;


            ViewModel.LoadTeamMembersCommand.Execute(null);

        }
    }
}

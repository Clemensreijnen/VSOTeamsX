using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Helpers;
using VSOTeams.Models;
using VSOTeams.ViewModels;
using Xamarin.Forms;

namespace VSOTeams.Views
{
    public class ProjectsView : BaseView
    {
        private ProjectsViewModel ViewModel
        {
            get { return BindingContext as ProjectsViewModel; }
        }

        public ProjectsView()
        {
            BindingContext = new ProjectsViewModel();

            Label header = new Label
            {
                Text = "Projects",
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
            listView.ItemsSource = ViewModel.Projects;
            listView.ItemTemplate = new DataTemplate(typeof(ProjectCell));

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                var prj = listView.SelectedItem as Project;
                this.Navigation.PushAsync(new ProjectMembersView(prj));
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

            ViewModel.LoadProjectsCommand.Execute(null);

        }
    }
}

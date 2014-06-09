using System;
using Xamarin.Forms;
using System.Collections.Generic;
using VSOTeams.ViewModels;
using VSOTeams.Models;
using VSOTeams.Helpers;

namespace VSOTeams.Views
{
    public class HomeMasterView : BaseView
    {
        public Action<MenuType> PageSelectionChanged;
        private Page pageSelection;
        private MenuType menuType = MenuType.About;
        public Page PageSelection
        {
            get { return pageSelection; }
            set
            {
                pageSelection = value;
                if (PageSelectionChanged != null)
                    PageSelectionChanged(menuType);
            }
        }
        private AboutView about;
        private TeamsView teams;
        private RoomsView rooms;
        private ProjectsView projects;
        private LoginView login;
        public HomeMasterView(HomeViewModel viewModel)
        {
            this.Icon = "slideout.png";
            BindingContext = viewModel;

            var layout = new StackLayout { Spacing = 0 };

            var label = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                Content = new Label
                {
                    Text = "MENU",
                    Font = Font.SystemFontOfSize(NamedSize.Medium)
                }
            };

            layout.Children.Add(label);

            var listView = new ListView();

            var cell = new DataTemplate(typeof(ListImageCell));

            cell.SetBinding(TextCell.TextProperty, HomeViewModel.TitlePropertyName);
            cell.SetBinding(ImageCell.ImageSourceProperty, "Icon");

            listView.ItemTemplate = cell;

            listView.ItemsSource = viewModel.MenuItems;
            if (about == null)
                about = new AboutView();

            PageSelection = about;
            //Change to the correct page
            listView.ItemSelected += (sender, args) =>
            {
                var menuItem = listView.SelectedItem as HomeMenuItem;
                menuType = menuItem.MenuType;
                switch (menuItem.MenuType)
                {
                    case MenuType.About:
                        if (about == null)
                            about = new AboutView();

                        PageSelection = about;
                        break;
                    case MenuType.Teams:
                        if (teams == null)
                            teams = new TeamsView();

                        PageSelection = teams;
                        break;
                    case MenuType.Rooms:
                        if (rooms == null)
                            rooms = new RoomsView();

                        PageSelection = rooms;
                        break;
                    case MenuType.Projects:
                        if (projects == null)
                            projects = new ProjectsView();

                        PageSelection = projects;
                        break;
                }
            };

            LoginInfo _credentials = new LoginInfo();
            App.IsLoggedIn = _credentials.CanLogin().Result;

            if (App.IsLoggedIn)
            {
                listView.SelectedItem = viewModel.MenuItems[0];
            }
            else
            {
                if (login == null)
                    login = new LoginView();

                PageSelection = login;
            }


            layout.Children.Add(listView);

            Content = layout;
        }
    }
}

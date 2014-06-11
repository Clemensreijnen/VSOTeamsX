using System;
using Xamarin.Forms;
using System.Collections.Generic;
using VSOTeams.ViewModels;
using VSOTeams.Models;
using VSOTeams.Helpers;
using System.Reflection;

namespace VSOTeams.Views
{
    public class HomeView : BaseView
    {

        public HomeView()
        {
            Label header = new Label
            {
                Text = "VSO Teams",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center
            };

            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        new ImageCell
                        {
                            ImageSource =  "People.png",
                            Text = "Teams",
                            Detail = "All my teams",
                            Command = new Command(async () => 
                                    await Navigation.PushAsync(new TeamsView()))
                            
                        },
                        new ImageCell
                        {
                            ImageSource =  "Login-Door.png",
                            Text = "Rooms",
                            Detail = "Availalbe teamrooms",
                            Command = new Command(async () => 
                                    await Navigation.PushAsync(new RoomsView()))
                        },
                    }
                }
            };
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    header,
                    tableView
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoginInfo credentials = new LoginInfo();

            if (credentials.Account == "" || credentials.UserName == "" || credentials.Password == "")
            {
                this.Navigation.PushAsync(new LoginView());
            }
        }
    }
}

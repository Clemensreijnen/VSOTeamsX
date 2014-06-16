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
        LoginInfo credentials;
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
                Intent = TableIntent.Menu,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        new ImageCell
                        {
                            ImageSource =  "refresh.png",
                            Text = "Teammembers",
                            Detail = "My teammembers in projects",
                            Command = new Command(async () => 
                                    await Navigation.PushAsync(new ProjectsView()))
                            
                        },
                        new ImageCell
                        {
                            ImageSource =  "about.png",
                            Text = "Rooms",
                            Detail = "Availalbe teamrooms",
                            Command = new Command(async () => 
                                    await Navigation.PushAsync(new RoomsView()))
                        },
                    },
                     new TableSection
                    {
                        new ImageCell
                        {
                            ImageSource =  "Login.png",
                            Text = "Credentials",
                            Detail = "Set credentials and VSO account",
                            Command = new Command(async () => 
                                    await Navigation.PushAsync(new LoginView(credentials)))
                        }
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            credentials = await LoginInfo.GetCredentials();
            
            if (string.IsNullOrEmpty(credentials.Account) || string.IsNullOrEmpty(credentials.UserName )|| string.IsNullOrEmpty(credentials.Password ))
            {
                await this.Navigation.PushAsync(new LoginView(credentials));
            }
        }
    }
}

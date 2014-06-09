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
            Command<Type> navigateCommand = 
                new Command<Type>(async (Type pageType) =>
                {
                    // Get all the constructors of the page type.
                    IEnumerable<ConstructorInfo> constructors = 
                            pageType.GetTypeInfo().DeclaredConstructors;

                    foreach (ConstructorInfo constructor in constructors)
                    {
                        // Check if the constructor has no parameters.
                        if (constructor.GetParameters().Length == 0)
                        {
                            // If so, instantiate it, and navigate to it.
                            Page page = (Page)constructor.Invoke(null);
                            await this.Navigation.PushAsync(page);
                            break;
                        }
                    }
                });

            Label header = new Label
            {
                Text = "VSO Teams",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };

            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("")
                {
                    new TableSection("")
                    {
                        new ImageCell
                        {
                            ImageSource =  "about.png",
                            Text = "Teams",
                            Detail = "All my teams",
                            Command = navigateCommand,
                            CommandParameter = typeof(TeamsView)
                        },
                        new ImageCell
                        {
                            ImageSource =  "about.png",
                            Text = "Rooms",
                            Detail = "Availalbe teamrooms",
                            Command = navigateCommand,
                            CommandParameter = typeof(RoomsView)
                        },
                        new ImageCell
                        {
                            ImageSource =  "about.png",
                            Text = "Projects",
                            Detail = "My projects",
                            Command = navigateCommand,
                            CommandParameter = typeof(ProjectsView)
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
            
    }
}

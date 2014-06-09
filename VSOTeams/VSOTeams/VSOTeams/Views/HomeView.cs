using System;
using Xamarin.Forms;
using System.Collections.Generic;
using VSOTeams.ViewModels;
using VSOTeams.Models;
using VSOTeams.Helpers;

namespace VSOTeams.Views
{
    public class HomeView : MasterDetailPage
    {
        private HomeViewModel ViewModel
        {
            get { return BindingContext as HomeViewModel; }
        }
        HomeMasterView master;
        private Dictionary<MenuType, NavigationPage> pages;

        public HomeView()
        {
            pages = new Dictionary<MenuType, NavigationPage>();
            BindingContext = new HomeViewModel();

            Master = master = new HomeMasterView(ViewModel);

            var homeNav = new NavigationPage(master.PageSelection)
            {
                Tint = Helpers.Color.DarkBlue.ToFormsColor()
            };
            Detail = homeNav;

            pages.Add(MenuType.Teams, homeNav);

            master.PageSelectionChanged = (menuType) =>
            {
                NavigationPage newPage;
                if (pages.ContainsKey(menuType))
                {
                    newPage = pages[menuType];
                }
                else
                {
                    newPage = new NavigationPage(master.PageSelection)
                    {
                        Tint = Helpers.Color.DarkBlue.ToFormsColor()
                    };
                    pages.Add(menuType, newPage);
                }
                Detail = newPage;
                Detail.Title = master.PageSelection.Title;
                IsPresented = false;
            };

            this.Icon = "slideout.png";
        }

    }


  
}

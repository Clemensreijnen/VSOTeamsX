using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSOTeams.Helpers;
using VSOTeams.Views;
using Xamarin.Forms;

namespace VSOTeams
{
	public class App
	{
        public static bool IsLoggedIn { get; set; }
       
        private static Page homeView;
        public static Page RootPage
        {

            get { return homeView ?? (homeView = new NavigationPage(new HomeView())); }
        }
	}
}

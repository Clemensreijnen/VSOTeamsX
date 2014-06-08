using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSOTeams.Views;
using Xamarin.Forms;

namespace VSOTeams
{
	public class App
	{
        private static Page homeView;
        public static Page RootPage
        {
            get { return homeView ?? (homeView = new HomeView()); }
        }
	}
}

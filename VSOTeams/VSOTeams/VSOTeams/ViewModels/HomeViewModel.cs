using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using VSOTeams.Models;

namespace VSOTeams.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<HomeMenuItem> MenuItems { get; set; }
        public HomeViewModel()
        {
            Title = "VSO Teams";

            MenuItems = new ObservableCollection<HomeMenuItem>();
            MenuItems.Add(new HomeMenuItem
            {
                Id = 0,
                Title = "Teams",
                MenuType = MenuType.Teams,
                Icon = "about.png"
            });
            MenuItems.Add(new HomeMenuItem
            {
                Id = 1,
                Title = "Rooms",
                MenuType = MenuType.Rooms,
                Icon = "blog.png"
            });
            MenuItems.Add(new HomeMenuItem
            {
                Id = 2,
                Title = "Projects",
                MenuType = MenuType.Projects,
                Icon = "twitternav.png"
            });
            MenuItems.Add(new HomeMenuItem
            {
                Id = 3,
                Title = "About",
                MenuType = MenuType.About,
                Icon = "about.png"
            });
        }
    }
}

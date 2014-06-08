using System;
using System.Collections.Generic;
using System.Text;

namespace VSOTeams.Models
{
    public enum MenuType
    {
        About,
        Teams,
        Rooms,
        Projects
    }

    public class HomeMenuItem
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.Teams;
        }
        public string Icon { get; set; }
        public MenuType MenuType { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Id { get; set; }
    }
}

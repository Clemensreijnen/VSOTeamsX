using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace VSOTeams.Models
{
    public class Teams
    {
        public ObservableCollection<Team> value { get; set; }
    }

    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string IdentityUrl { get; set; }
        public string MyProperty { get; set; }
        public ObservableCollection<TeamMember> value { get; set; }
        public Project project { get; set; }


    }
}

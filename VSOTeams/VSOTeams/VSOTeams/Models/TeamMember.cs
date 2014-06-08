using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VSOTeams.Models
{
    public class TeamMembers
    {
        public ObservableCollection<TeamMember> value { get; set; }
    }

    public class TeamMember
    {
        public string Id { get; set; }
        public string DisplayName{ get; set; }
        public string UniqueName{ get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        
        //public  ImageFile { get; set; }


    }
}

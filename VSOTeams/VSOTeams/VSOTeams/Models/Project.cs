using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VSOTeams.Models
{
    public class Projects
    {
        public ObservableCollection<Project> value { get; set; }
    }
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public ObservableCollection<Team> Teams { get; set; }

    }
}

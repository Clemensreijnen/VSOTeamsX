using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VSOTeams.Helpers;
using VSOTeams.Models;
using Xamarin.Forms;

namespace VSOTeams.ViewModels
{
    public class ProjectsViewModel : BaseViewModel
    {
        public ObservableCollection<Project> Projects { get; set; }

        public ProjectsViewModel()
        {
            Title = "Projects";
            Projects = new ObservableCollection<Project>();
        }
        
        private Project selectedProject;
        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }

        private Command loadProjectsCommand;

        public Command LoadProjectsCommand
        {
            get { return loadProjectsCommand ?? (loadProjectsCommand = new Command(async () => await ExecuteLoadProjectsCommand())); }
        }

        private async Task ExecuteLoadProjectsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                string uriString = "/DefaultCollection/_apis/projects/";
                var responseBody = await HttpClientHelper.RequestVSO(uriString);

                Projects allProjects = JsonConvert.DeserializeObject<Projects>(responseBody);

                var projectImage = new Image { Source = new FileImageSource { File = "prj.png" } };
                foreach (var prj in allProjects.value)
                {
                    prj.ImageUri = projectImage.Source;
                    Projects.Add(prj);
                }
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", "Unable to load Visual Studio Online projects.", "OK", null);
            }

            IsBusy = false;
        }
    }
}

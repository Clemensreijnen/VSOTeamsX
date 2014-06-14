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
        public ProjectsViewModel()
        {
            Title = "Projects";
        }

        private ObservableCollection<Project> projects = new ObservableCollection<Project>();

        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set { projects = value; OnPropertyChanged("Projects"); }
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
                HttpClientHelper helper = new HttpClientHelper();
                var _credentials = await LoginInfo.GetCredentials();

                HttpClient _httpClient = helper.CreateHttpClient(_credentials);

                string uriString = string.Format("https://{0}.visualstudio.com/DefaultCollection/_apis/projects/", _credentials.Account);
                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Projects allProjects = JsonConvert.DeserializeObject<Projects>(responseBody);
                projects = allProjects.value;
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

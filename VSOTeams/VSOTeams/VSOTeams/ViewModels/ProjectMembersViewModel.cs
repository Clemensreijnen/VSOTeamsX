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
    public class ProjectMembersViewModel : BaseViewModel
    {
        private Project prj = null;
        public ObservableCollection<TeamMember> TeamMembers { get; set; }

        public ProjectMembersViewModel(Project prjRequest)
        {
            Title = "TeamMembers";
            TeamMembers = new ObservableCollection<TeamMember>();
            prj = prjRequest;
        }

        private TeamMember selectedTeamMember;
        public TeamMember SelectedTeamMember
        {
            get { return selectedTeamMember; }
            set
            {
                selectedTeamMember = value;
                OnPropertyChanged("SelectedTeamMember");
            }
        }

        private Command loadTeamMembersCommand;

        public Command LoadTeamMembersCommand
        {
            get { return loadTeamMembersCommand ?? (loadTeamMembersCommand = new Command(async () => await ExecuteLoadTeamMembersCommand())); }
        }

        private async Task ExecuteLoadTeamMembersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                string uriString = string.Format("/DefaultCollection/_apis/projects/{0}/teams", prj.Id);
                var responseBody = await HttpClientHelper.RequestVSO(uriString);

                Teams prjTeams = JsonConvert.DeserializeObject<Teams>(responseBody);

                foreach (var team in prjTeams.value)
                {
                    var uriStringTeams = string.Format("/DefaultCollection/_apis/projects/{0}/teams/{1}/members", prj.Id, team.Id);
                    var responseBodyTeams = await HttpClientHelper.RequestVSO(uriStringTeams);

                    TeamMembers tms = JsonConvert.DeserializeObject<TeamMembers>(responseBodyTeams);
                    foreach (TeamMember item in tms.value)
                    {
                        if(TeamMembers.Contains(item) == false)
                        {
#if __ANDROID__
                        ImageSource imgResult = new FileImageSource { File = "Badge.png" } ;
#else
                            ImageSource imgResult = await VSOTeams.Helpers.FileHelper.DownloadImage(new Uri(item.ImageUrl), item.Id + ".png");
#endif
                            item.ImageSource = imgResult;
                            TeamMembers.Add(item);
                        }
                    }
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

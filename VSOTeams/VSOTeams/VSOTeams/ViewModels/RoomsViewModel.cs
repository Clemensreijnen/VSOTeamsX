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
    public class RoomsViewModel : BaseViewModel
    {
        public RoomsViewModel()
        {
            Title = "TeamRooms";
        }
        private ObservableCollection<TeamRoom> teamRooms = new ObservableCollection<TeamRoom>();

        public ObservableCollection<TeamRoom> TeamRooms
        {
            get { return teamRooms; }
            set { teamRooms = value; OnPropertyChanged("TeamRooms"); }
        }

        private Project selectedTeamRoom;
        public Project SelectedTeamRoom
        {
            get { return selectedTeamRoom; }
            set
            {
                selectedTeamRoom = value;
                OnPropertyChanged("SelectedTeamRoom");
            }
        }

        private Command loadTeamRoomsCommand;

        public Command LoadTeamRoomCommand
        {
            get { return loadTeamRoomsCommand ?? (loadTeamRoomsCommand = new Command(async () => await ExecuteLoadTeamRoomsCommand())); }
        }

        private async Task ExecuteLoadTeamRoomsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                HttpClientHelper helper = new HttpClientHelper();
                LoginInfo _credentials = new LoginInfo();

                HttpClient _httpClient = helper.CreateHttpClient(_credentials);

                string uriString = string.Format("https://{0}.visualstudio.com/DefaultCollection/_apis/Chat/rooms", _credentials.Account);
                Uri resourceAddress = new Uri(uriString);

                HttpResponseMessage response = await _httpClient.GetAsync(resourceAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                TeamRooms allTeamRooms = JsonConvert.DeserializeObject<TeamRooms>(responseBody);
                teamRooms = allTeamRooms.value;
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

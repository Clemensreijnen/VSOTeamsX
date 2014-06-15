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
        public ObservableCollection<TeamRoom> TeamRooms { get; set; }

        public RoomsViewModel()
        {
            Title = "TeamRooms";
            TeamRooms = new ObservableCollection<TeamRoom>();
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
            get { return loadTeamRoomsCommand ?? (loadTeamRoomsCommand = new Command(ExecuteLoadTeamRoomsCommand)); }
        }

        private async void ExecuteLoadTeamRoomsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TeamRooms.Clear();

                string uriString = "/DefaultCollection/_apis/Chat/rooms";
                var responseBody = await HttpClientHelper.RequestVSO(uriString);

                TeamRooms allTeamRooms = JsonConvert.DeserializeObject<TeamRooms>(responseBody);
               
                var roomsImage = new Image { Source = new FileImageSource { File = "room.png" } };
                foreach (var room in allTeamRooms.value)
                {
                    room.ImageUri = roomsImage.Source;
                    room.lastActivity = String.Format("Last activity on: {0:ddd, MMM d}", Convert.ToDateTime(room.lastActivity));              
                    TeamRooms.Add(room);
                }

            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", "Unable to load Visual Studio Online Teamrooms.", "OK", null);
            }

            IsBusy = false;
        }
    }
}

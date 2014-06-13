using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Models;
using VSOTeams.ViewModels;

namespace VSOTeams.Views
{
    public class RoomMessagesView : BaseView
    {
        private MessagesViewModel ViewModel
        {
            get { return BindingContext as MessagesViewModel; }
        }
        public RoomMessagesView(TeamRoom room)
        {

            BindingContext = new MessagesViewModel(room);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || ViewModel.IsBusy)
                return;


            ViewModel.LoadTeamRoomMessagesCommand.Execute(null);

        }
    }
}

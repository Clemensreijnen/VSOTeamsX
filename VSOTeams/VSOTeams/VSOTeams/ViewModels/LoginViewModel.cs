using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using VSOTeams.Helpers;
using VSOTeams.Views;

namespace VSOTeams.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(LoginInfo _credentials)
        {
            Title = "Login";
            credentials = _credentials;
            Saved = true;
        }

        private LoginInfo credentials;
        public LoginInfo Credentials
        {
            get 
            {
                return credentials;  
            }
            set { credentials = value; OnPropertyChanged("Credentials"); }
        }

        private Command logMeIn;
        public Command LogMeIn
        {
            get { return logMeIn ?? (logMeIn = new Command(ExecuteLogMeInCommand)); }
        }

        private void ExecuteLogMeInCommand()
        {
            LoginInfo.SaveCredentials(credentials.Account, credentials.UserName, credentials.Password);
            Saved = false;
        }
    }
}

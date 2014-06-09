using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using VSOTeams.Helpers;

namespace VSOTeams.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";
            //Icon = "blog.png";
        }

        private LoginInfo credentials = new LoginInfo();
        public LoginInfo Credentials
        {
            get { return credentials;  }
            set { credentials = value; OnPropertyChanged("Credentials"); }
        }

        private Command logMeIn;
        public Command LogMeIn
        {
            get { return logMeIn ?? (logMeIn = new Command(async () => await ExecuteLogMeInCommand())); }
        }

        private async Task ExecuteLogMeInCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var credentialsOK = await credentials.CanLogin();
                if (credentialsOK == true)
                {
                    // de hele boel laden

                }
               
            }
            catch (Exception ex)
            {
               
            }

            IsBusy = false;
        }
    }
}

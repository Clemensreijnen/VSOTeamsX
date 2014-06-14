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
            IsNotConnected = true;
            Title = "Login";
            ScreenMessage = "";
            credentials = _credentials;
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
            get { return logMeIn ?? (logMeIn = new Command(async () => await ExecuteLogMeInCommand())); }
        }

        private async Task ExecuteLogMeInCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsNotConnected = false;
            ScreenMessage = "";

            try
            {

                HttpClientHelper helper = new HttpClientHelper();
                HttpClient _httpClient = helper.CreateHttpClient(credentials);

                var credentialsOK = await helper.IsValideCredential(credentials);
                if (credentialsOK == true)
                {
                    LoginInfo.SaveCredentials(credentials.Account, credentials.UserName, credentials.Password);

                    App.IsLoggedIn = true;
                    IsBusy = false;
                    IsNotConnected = false;
                    ScreenMessage = "Connected to VSO.";
                } 
                else
                {
                    IsBusy = false;
                    IsNotConnected = true;
                    ScreenMessage = "Not connected, check credentials.";
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                IsNotConnected = true;
                ScreenMessage = string.Format("Unable to connect to VSO. {0}", ex.InnerException);


            }
        }

        

    }
}

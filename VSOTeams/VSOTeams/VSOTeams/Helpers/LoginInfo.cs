
using PCLStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VSOTeams.Helpers
{
    public class LoginInfo : INotifyPropertyChanged
    {
        private static LoginInfo _credentials = new LoginInfo();

        string _account = "" ;
        string _password = "";
        string _username = "";

        public string Account
        {
            get
            {
                return _account; 
            }
            set
            {
                if (_account.Equals(value, StringComparison.Ordinal))
                {
                    return;
                }
                _account = value ?? string.Empty;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get 
            {
                return _username; 
            }
            set
            {
                if (_username.Equals(value, StringComparison.Ordinal))
                {
                    return;
                }

                _username = value ?? string.Empty;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get 
            {
                return _password; 
            }
            set
            {
                if (_password.Equals(value, StringComparison.Ordinal))
                {
                    return;
                }
                _password = value ?? string.Empty;
                OnPropertyChanged();
            }
        }



        internal async static Task<LoginInfo> GetCredentials()
        {
            if (_credentials == null)
                _credentials = new LoginInfo();

            try
            {
                var credentials = await LoadCredentialsIfExsist();

                if (credentials != null)
                {
                    _credentials.Account = credentials[0];
                    _credentials.UserName = credentials[1];
                    _credentials.Password = credentials[2];
                }
                return _credentials;
            }
            catch (Exception)
            {
                _credentials.Account = "";
                _credentials.UserName = "";
                _credentials.Password = "";
                return _credentials;
            }
        }

      
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Logic that determines if the user has entered the proper login information.
        ///   Will set the .LoginButtonColour to a Light Blue if the user can login.
        /// </summary>
        /// <returns><c>true</c> if the user can login; otherwise, <c>false</c>.</returns>
        

        public async static void SaveCredentials(string account, string userName, string password)
        {
            string credentialsString = string.Format("{0}, {1}, {2}", account, userName, password);
                IFile saveCredentials = await FileHelper.GetOrCreateFileFromLocalFolder("credentials.txt");
                await saveCredentials.WriteAllTextAsync(credentialsString);
        }

        public async static Task<string[]> LoadCredentialsIfExsist()
        {
            var saveCredentials = await FileHelper.CheckIfFileExsistsInLocalFolder("credentials.txt");
            if(saveCredentials == true)
            {
                IFile file = await FileHelper.GetOrCreateFileFromLocalFolder("credentials.txt");
                string settingsString = file.ReadAllTextAsync().Result;
                var credentials = settingsString.Split(new char[] {','});
                return credentials;
            }
            else
            {
                return null;
            }
         }


        /// <summary>
        ///   Helper method that will raise the PropertyChanged event when a property is changed.
        /// </summary>
        /// <param name="propertyName">
        ///   Name of the property that was updated. If null then [CallerMemberName] will set it to the name of the
        ///   member that invoked it.
        /// </param>
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}

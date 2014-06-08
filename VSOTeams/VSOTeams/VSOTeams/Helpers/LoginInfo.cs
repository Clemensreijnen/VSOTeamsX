using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace VSOTeams.Helpers
{
    class LoginInfo : INotifyPropertyChanged
    {
        string _account = string.Empty;
        string _password = string.Empty;
        string _username = string.Empty;

        public string Account
        {
            get { return _account; }
            set
            {
                if (_account.Equals(value, StringComparison.Ordinal))
                {
                    return;
                }
                _account = value ?? string.Empty;
                OnPropertyChanged();

                CanLogin();
            }
        }
        public string UserName
        {
            get { return _username; }
            set
            {
                if (_username.Equals(value, StringComparison.Ordinal))
                {
                    return;
                }
                _username = value ?? string.Empty;
                OnPropertyChanged();

                CanLogin();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password.Equals(value, StringComparison.Ordinal))
                {
                    return;
                }
                _password = value ?? string.Empty;
                OnPropertyChanged();

                CanLogin();
            }
        }



      
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Logic that determines if the user has entered the proper login information.
        ///   Will set the .LoginButtonColour to a Light Blue if the user can login.
        /// </summary>
        /// <returns><c>true</c> if the user can login; otherwise, <c>false</c>.</returns>
        public bool CanLogin()
        {
            bool login = !string.IsNullOrWhiteSpace(_account) && !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password);
            return login;
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

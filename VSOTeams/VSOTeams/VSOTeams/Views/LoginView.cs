using System;
using VSOTeams.Helpers;
using VSOTeams.ViewModels;
using Xamarin.Forms;

namespace VSOTeams.Views
{
    public class LoginView : BaseView
    {
        private LoginViewModel ViewModel
        {
            get { return BindingContext as LoginViewModel; }
        }

        public LoginView()
        {
            Padding = new Thickness(20);
            Title = "Login";
            BindingContext = new LoginViewModel();

            Entry accountInput = new Entry { Placeholder = "VSO Account" };
            accountInput.SetBinding(Entry.TextProperty, "Credentials.Account");
            

            Entry loginInput = new Entry { Placeholder = "User Name" };
            loginInput.SetBinding(Entry.TextProperty, "Credentials.UserName");
            

            Entry passwordInput = new Entry { IsPassword = true, Placeholder = "Password" };
            passwordInput.SetBinding(Entry.TextProperty, "Credentials.Password");
        

            Button loginButton = new Button
            {
                Text = "Login",
                BorderRadius = 5,
                TextColor = Helpers.Color.Blue.ToFormsColor(),
                BackgroundColor = Helpers.Color.DarkBlue.ToFormsColor(),
                Command = ViewModel.LogMeIn
            };

            var stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                          {
                              accountInput,
                              loginInput,
                              passwordInput,
                              loginButton
                          },
                Spacing = 10,
            };

            var activity = new ActivityIndicator
            {
                Color = Helpers.Color.DarkBlue.ToFormsColor(),
                IsEnabled = true
            };
            activity.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
            activity.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

            stack.Children.Add(activity);

            Content = stack;
        }

        
    }
}

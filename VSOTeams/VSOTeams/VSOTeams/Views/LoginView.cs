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

        public LoginView(LoginInfo credentials)
        {
            Padding = new Thickness(20);
            Title = "Credentials";
            BindingContext = new LoginViewModel(credentials);

            
            Label header = new Label
            {
                Text = "VSO Credentials",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center
            };

            Label message = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                TextColor = Xamarin.Forms.Color.Red,
                HorizontalOptions = LayoutOptions.Center
            };
            message.SetBinding(Label.TextProperty, "ScreenMessage");

            Entry accountInput = new Entry { Placeholder = "VSO Account" };
            accountInput.SetBinding(Entry.TextProperty, "Credentials.Account");
            

            Entry loginInput = new Entry { Placeholder = "User Name" };
            loginInput.SetBinding(Entry.TextProperty, "Credentials.UserName");
            

            Entry passwordInput = new Entry { IsPassword = true, Placeholder = "Password" };
            passwordInput.SetBinding(Entry.TextProperty, "Credentials.Password");
        

            Button loginButton = new Button
            {
                Text = "Save",
                BorderRadius = 0,
                Command = ViewModel.LogMeIn
            };

            loginButton.SetBinding(Button.IsEnabledProperty, "Saved");

            var stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Children =
                          {
                              header,
                              message,
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

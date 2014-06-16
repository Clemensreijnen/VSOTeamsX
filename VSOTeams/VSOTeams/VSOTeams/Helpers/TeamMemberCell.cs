using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace VSOTeams.Helpers
{
    class TeamMemberCell : ViewCell
    {
        public TeamMemberCell()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };

            image.SetBinding(Image.SourceProperty, new Binding("ImageSource"));
            image.WidthRequest = image.HeightRequest = 150;

            var nameLayout = CreateNameLayout();

            var viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { image, nameLayout }
            };
            Height = 75;
            View = viewLayout;
        }

        static StackLayout CreateNameLayout()
        {
            var nameLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };
            nameLabel.SetBinding(Label.TextProperty, "DisplayName");
            //nameLabel.LineBreakMode = LineBreakMode.;

            var twitterLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Micro),
                TextColor = Color.Blue.ToFormsColor()
            };
            twitterLabel.SetBinding(Label.TextProperty, "UniqueName");


            var nameLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { nameLabel, twitterLabel }
            };
            return nameLayout;
        }
    }
}

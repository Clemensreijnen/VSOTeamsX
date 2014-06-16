using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VSOTeams.Helpers
{
 class MessageCell : ViewCell
    {
     public MessageCell()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };

            image.SetBinding(Image.SourceProperty, new Binding("MessageTypeURI"));
            image.WidthRequest = image.HeightRequest = 60;
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
                Font = Font.SystemFontOfSize(NamedSize.Small)
            };
            nameLabel.SetBinding(Label.TextProperty, "Content");
            //nameLabel.LineBreakMode = LineBreakMode.;

            var twitterLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Micro),
                TextColor = Color.Blue.ToFormsColor()
            };
            twitterLabel.SetBinding(Label.TextProperty, "PostedByDisplayName");

    
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

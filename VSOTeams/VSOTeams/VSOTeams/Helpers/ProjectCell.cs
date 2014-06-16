using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VSOTeams.Helpers
{
    class ProjectCell : ViewCell
    {
        public ProjectCell()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            image.SetBinding(Image.SourceProperty, new Binding("ImageUri"));
            image.WidthRequest = image.HeightRequest = 40;

            var nameLayout = CreateNameLayout();

            var viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { image, nameLayout }
            };
            
            View = viewLayout;
        }

        static StackLayout CreateNameLayout()
        {

            var nameLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Medium)
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");
            nameLabel.LineBreakMode = LineBreakMode.TailTruncation;

            var twitterLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Micro),
                TextColor = Color.Blue.ToFormsColor()
            };
            twitterLabel.SetBinding(Label.TextProperty, "Description");

    
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

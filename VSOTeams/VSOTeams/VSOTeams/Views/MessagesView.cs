using System;
using System.Collections.Generic;
using System.Text;
using VSOTeams.Models;
using Xamarin.Forms;

namespace VSOTeams.Views
{
    public class MessagesView : BaseView
    {
        public MessagesView(SimpleRoomMessage item)
        {
            BindingContext = item;

            Label header = new Label
            {
                Text = "Teamroom message",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center
            };

            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            StackLayout stack = new StackLayout();
            var messageTime = new Label
            {
                Text = item.message.postedTime.ToShortDateString() + " " + item.message.postedTime.ToShortTimeString(),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.Gray
            };

            var messageText = new Label
            {
                Text = item.Content,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BackgroundColor = Color.Green,
                TextColor = Color.White,
                HeightRequest = 100
            };

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Source = item.MessageTypeURI,
                WidthRequest = HeightRequest = 120

            };

            if (item.message.content is TeamRoomMessage.Content.Normal)
            {
                var name = new Label
                {
                    Text = item.PostedByDisplayName,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    LineBreakMode = LineBreakMode.WordWrap
                };

                var messageHeader = new StackLayout()
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.End,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Start,
                    Children = { image, name }
                };

                stack.Children.Add(messageHeader);
                stack.Children.Add(messageTime);
                stack.Children.Add(messageText);
                stack.VerticalOptions = LayoutOptions.CenterAndExpand;
   
            }
            if (item.message.content is TeamRoomMessage.Content.Notification.BuildCompletedEvent)
            {

                var name = new Label
                {
                    Text = "Build event",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    LineBreakMode = LineBreakMode.WordWrap
                };

                var messageHeader = new StackLayout()
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.End,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Start,
                    Children = { image, name }
                };

                var cnt = (TeamRoomMessage.Content.Notification.BuildCompletedEvent)item.message.content;

                var buildDetails = new Label
                {
                    Text = cnt.data.buildNumber + " " + cnt.data.buildStatus,
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    LineBreakMode = LineBreakMode.WordWrap
                };
                
                var buildresultImage = new Image();

                switch (cnt.data.buildStatus)
	            {
                    case "Succeeded":
                        buildresultImage.Source = "succes.png";
                        break;
                    case "Failed":
                        buildresultImage.Source = "Failed.png";
                        break;
                    case "PartiallySucceeded":
                        buildresultImage.Source = "part.png";
                        break;
		            default:
                        buildresultImage.Source = "Stopped.png";
                        break;
                };

                var messageDetails = new StackLayout
                {
                    Children = 
                    {
                        buildresultImage,
                        buildDetails
                    },
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.EndAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    HeightRequest = 50

                };

                stack.Children.Add(messageHeader);
                stack.Children.Add(messageTime);
                stack.Children.Add(messageText);
                stack.Children.Add(messageDetails);
                stack.VerticalOptions = LayoutOptions.StartAndExpand;
            }

            if (item.message.content is TeamRoomMessage.Content.Notification.CheckinEvent)
            {
                var name = new Label
                {
                    Text = "Changeset event",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    LineBreakMode = LineBreakMode.WordWrap
                };

                var messageHeader = new StackLayout()
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.End,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Start,
                    Children = { image, name }
                };

                var cnt = (TeamRoomMessage.Content.Notification.CheckinEvent)item.message.content;
                var changesetDetails = new Label
                {
                    Text = "Changeset " + cnt.data.changeSetNumber + " by " + cnt.data.committer.displayName,
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    TextColor = Color.Gray,
                    LineBreakMode = LineBreakMode.WordWrap
                };

                var changesStack = new StackLayout()
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.End,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Start,
                };

                if (cnt.data.changes.add != 0)
                {
                    var addLabel = new Label
                    {
                        Text = cnt.data.changes.add.ToString(),
                        BackgroundColor = Color.Green,
                        TextColor = Color.White,
                        Font = Font.SystemFontOfSize(NamedSize.Medium),
                        WidthRequest = 30,
                        XAlign = TextAlignment.Center
                    };

                    string labelText;
                    if(cnt.data.changes.add == 1)
                        labelText = "file added";
                    else
                        labelText = "files added";

                    var addLabeltext = new Label
                    {
                        Text = labelText,
                        TextColor = Color.Green,
                        Font = Font.SystemFontOfSize(NamedSize.Medium)
                    };
                    changesStack.Children.Add(addLabel);
                    changesStack.Children.Add(addLabeltext);
                }

                if (cnt.data.changes.delete != 0)
                {
                    var deleteLabel = new Label
                    {
                        Text = cnt.data.changes.delete.ToString(),
                        BackgroundColor = Color.Red,
                        TextColor = Color.White,
                        Font = Font.SystemFontOfSize(NamedSize.Medium),
                        WidthRequest = 30,
                        XAlign = TextAlignment.Center

                    };

                    string labelText;
                    if (cnt.data.changes.delete == 1)
                        labelText = "file deleted";
                    else
                        labelText = "files delete";

                    var deleteLabeltext = new Label
                    {
                        Text = labelText,
                        TextColor = Color.Red,
                        Font = Font.SystemFontOfSize(NamedSize.Medium)
                    };
                    changesStack.Children.Add(deleteLabel);
                    changesStack.Children.Add(deleteLabeltext);
                }


                if (cnt.data.changes.edit != 0)
                {
                    var editLabel = new Label
                    {
                        Text = cnt.data.changes.edit.ToString(),
                        BackgroundColor = Color.Blue,
                        TextColor = Color.White,
                        Font = Font.SystemFontOfSize(NamedSize.Medium),
                        WidthRequest = 30,
                        XAlign = TextAlignment.Center

                    };

                    string labelText;
                    if (cnt.data.changes.edit == 1)
                        labelText = "file edited";
                    else
                        labelText = "files edited";

                    var editLabeltext = new Label
                    {
                        Text = labelText,
                        TextColor = Color.Blue,
                        Font = Font.SystemFontOfSize(NamedSize.Medium),
                        
                    };
                    changesStack.Children.Add(editLabel);
                    changesStack.Children.Add(editLabeltext);
                }

                stack.Children.Add(messageHeader);
                stack.Children.Add(messageTime);
                stack.Children.Add(changesetDetails);
                stack.Children.Add(messageText);
                stack.Children.Add(changesStack);
                stack.VerticalOptions = LayoutOptions.CenterAndExpand;
            }

            if (item.message.content is TeamRoomMessage.Content.Notification.WorkItemChangedEvent)
            {
                var name = new Label
                {
                    Text = "WorkItem event",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    LineBreakMode = LineBreakMode.WordWrap
                };

                var messageHeader = new StackLayout()
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.End,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Start,
                    Children = { image, name }
                };

                var cnt = (TeamRoomMessage.Content.Notification.WorkItemChangedEvent)item.message.content;
                var changeText = new Label
                {
                    Text = cnt.title + " " + cnt.data.stateChangedValue + " by " + cnt.data.changedBy,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    LineBreakMode = LineBreakMode.WordWrap
                };

                stack.Children.Add(messageHeader);
                stack.Children.Add(messageTime);
                stack.Children.Add(messageText);
                stack.Children.Add(changeText);
                stack.VerticalOptions = LayoutOptions.CenterAndExpand;
   
            }


            this.Content = new StackLayout
            {
                Children = 
                    {
                        header,
                        stack
                    },
            };







        }
    }
}


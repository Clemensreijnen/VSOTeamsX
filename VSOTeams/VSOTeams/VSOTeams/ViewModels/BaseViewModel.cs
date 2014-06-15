using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VSOTeams.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public BaseViewModel()
        {
        }

        private string title = string.Empty;
        public const string TitlePropertyName = "Title";

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value, TitlePropertyName); }
        }

        private string subTitle = string.Empty;
        /// <summary>
        /// Gets or sets the "Subtitle" property
        /// </summary>
        public const string SubtitlePropertyName = "Subtitle";
        public string Subtitle
        {
            get { return subTitle; }
            set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
        }

        private string icon = null;
        /// <summary>
        /// Gets or sets the "Icon" of the viewmodel
        /// </summary>
        public const string IconPropertyName = "Icon";
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value, IconPropertyName); }
        }

        private bool isBusy;
        /// <summary>
        /// Gets or sets if the view is busy.
        /// </summary>
        public const string IsBusyPropertyName = "IsBusy";
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value, IsBusyPropertyName); }
        }

        private bool saved;
        /// <summary>
        /// Gets or sets if the view is busy.
        /// </summary>
        public const string SavedPropertyName = "Saved";
        public bool Saved
        {
            get { return saved; }
            set { SetProperty(ref saved, value, SavedPropertyName); }
        }

        private string screenMessage;
        /// <summary>
        /// Gets or sets if the view is busy.
        /// </summary>
        public const string ScreenMessagePropertyName = "ScreenMessage";
        public string ScreenMessage
        {
            get { return screenMessage; }
            set { SetProperty(ref screenMessage, value, ScreenMessagePropertyName); }
        }


        //MessageToShow
        private bool messageToShow = true;
        public const string MessageToShowPropertyName = "MessageToShow";
        public bool MessageToShow
        {
            get { return messageToShow; }
            set { SetProperty(ref messageToShow, value, MessageToShowPropertyName); }
        }

        //MessageToShow
        private string messageToShowText = "";
        public const string MessageToShowTextPropertyName = "MessageToShowText";
        public string MessageToShowText
        {
            get { return messageToShowText; }
            set { SetProperty(ref messageToShowText, value, MessageToShowTextPropertyName); }
        }

        protected void SetProperty<T>(
            ref T backingStore, T value,
            string propertyName,
            Action onChanged = null,
            Action<T> onChanging = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            if (onChanging != null)
                onChanging(value);

            OnPropertyChanging(propertyName);

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanging implementation
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

        public void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging == null)
                return;

            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }


        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

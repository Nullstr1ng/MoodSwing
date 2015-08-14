using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoodSwing.Model
{
    public class Model_TrackDetails : INotifyPropertyChanged
    {
        private string _Artist = string.Empty;
        public string Artist
        {
            get { return _Artist; }
            set
            {
                if (_Artist != value)
                {
                    _Artist = value;
                    RaisePropertyChanged("Artist");
                }
            }
        }

        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        private string _Album = string.Empty;
        public string Album
        {
            get { return _Album; }
            set
            {
                if (_Album != value)
                {
                    _Album = value;
                    RaisePropertyChanged("Album");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

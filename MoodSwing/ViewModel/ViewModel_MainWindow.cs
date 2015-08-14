/*
 * Wrapper_Spotify
 * author: Jayson Ragasa
 * date: Aug. 14, 2015
 */

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using MoodSwing.Effects;
using MoodSwing.Model;
using MoodSwing.Wrappers;
using System.Diagnostics;

namespace MoodSwing.ViewModel
{
    public class ViewModel_MainWindow : INotifyPropertyChanged
    {
        #region vars

        #endregion

        #region properties
        private Model_TrackDetails _TrackDetails = new Model_TrackDetails();
        public Model_TrackDetails TrackDetails
        {
            get { return _TrackDetails; }
            set
            {
                if (_TrackDetails != value)
                {
                    _TrackDetails = value;
                    RaisePropertyChanged("TrackDetails");
                }
            }
        }

        private int _SelectedEffect = 0;
        public int SelectedEffect
        {
            get { return _SelectedEffect; }
            set
            {
                if (_SelectedEffect != value)
                {
                    _SelectedEffect = value;
                    RaisePropertyChanged("SelectedEffect");
                }
            }
        }

        private string _Pattern = string.Empty;
        public string Pattern
        {
            get { return _Pattern; }
            set
            {
                if (_Pattern != value)
                {
                    _Pattern = value;
                    RaisePropertyChanged("Pattern");
                }
            }
        }
        #endregion

        #region commands
        public ICommand Command_Update { get; internal set; }
        #endregion

        #region ctor
        public ViewModel_MainWindow()
        {
            if (!DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow))
            {
                this.TrackDetails.Title = "Initializing Spotify ...";
                if (Wrapper_Spotify.Init())
                {
                    Wrapper_Skype.Init();

                    Updater.I.Start();
                    StatusEffects.I.Start();

                    Wrapper_Spotify.TrackChanged += Wrapper_Spotify_TrackChanged;

                    Command_Update = new RelayCommand(Commnad_Update_Click);

                    this.Pattern = Properties.Settings.Default.Pattern;
                    this.SelectedEffect = Properties.Settings.Default.SelectedEffect;
                }
                else
                {
                    this.TrackDetails.Title = "Make sure you're running Spotify. Please restart this app";
                    Process.GetCurrentProcess().Kill();
                }
            }
        }
        #endregion

        #region subs events
        void Wrapper_Spotify_TrackChanged(object sender, object Track)
        {
            this.TrackDetails.Artist = Wrapper_Spotify.CurrentTrack.Artist;
            this.TrackDetails.Title = Wrapper_Spotify.CurrentTrack.Title;
            this.TrackDetails.Album = Wrapper_Spotify.CurrentTrack.Album;

            UpdatePattern();
        }
        #endregion

        #region command methods
        public void Commnad_Update_Click(object o)
        {
            StatusEffects.I.UpdateEffect(this.SelectedEffect);

            UpdatePattern(true);
        }
        #endregion

        #region methods
        public void UpdatePattern(bool save = false)
        {
            string status = this.Pattern.Replace("%artist%", this.TrackDetails.Artist)
                                        .Replace("%title%", this.TrackDetails.Title)
                                        .Replace("%album%", this.TrackDetails.Album);

            StatusEffects.I.UpdateTrackDetails(status);

            if (save)
            {
                Properties.Settings.Default.SelectedEffect = this.SelectedEffect;
                Properties.Settings.Default.Pattern = this.Pattern;
                Properties.Settings.Default.Save();
            }
        }

        public void Dispose()
        {
            Updater.I.Stahp();
            Wrapper_Spotify.Dispose();
            Wrapper_Skype.Dispose();
        }
        #endregion

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

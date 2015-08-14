/*
 * Wrapper_Spotify
 * author: Jayson Ragasa
 * date: Aug. 14, 2015
 */

using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using JariZ;
using MoodSwing.Model;

namespace MoodSwing.Wrappers
{
    public delegate void OnTrackChanged(object sender, object Track);
    public class Wrapper_Spotify
    {
        #region delegated events
        public static event OnTrackChanged TrackChanged;
        #endregion

        #region vars
        static SpotifyAPI       _API;
        static Responses.CFID   _cfid;
        static Responses.Status _Current_Status;
        static Timer            _tmr;
        #endregion

        #region properties
        private static Model_TrackDetails _CurrentTrack = new Model_TrackDetails();
        public static Model_TrackDetails CurrentTrack
        {
            get { return _CurrentTrack; }
            set
            {
                if (_CurrentTrack.Album != value.Album || _CurrentTrack.Artist != value.Artist || _CurrentTrack.Title != value.Title)
                {
                    _CurrentTrack = value;
                }
            }
        }
        #endregion

        #region ctor
        public Wrapper_Spotify()
        {
            
        }
        #endregion

        #region subscribed events
        static void _tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            _tmr.Stop();

            CheckStatus();
            RequestCurrentTrack();
        }
        #endregion

        #region methods
        public static bool Init()
        {
            bool ret = false;

            try
            {
                _API = new SpotifyAPI(SpotifyAPI.GetOAuth(), "127.0.0.1");
                //It's required to get the contents of API.CFID before doing anything, even if you're not intending to do anything with the CFID
                _cfid = _API.CFID;

                if (_cfid.error != null)
                {
                    throw new Exception(string.Format("Spotify returned a error {0} (0x{1})", _cfid.error.message, _cfid.error.type));
                }

                _Current_Status = _API.Status;

                if (_cfid.error != null)
                {
                    throw new Exception(string.Format("Spotify returned a error {0} (0x{1})", _cfid.error.message, _cfid.error.type));
                }

                _tmr = new Timer()
                {
                    Interval = 5000, Enabled = true
                };
                _tmr.Elapsed += _tmr_Elapsed;
                _tmr.Start();

                ret = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                ret = false;
            }

            return ret;
        }

        public static void CheckStatus()
        {
            _Current_Status = _API.Status;
        }

        public static Model_TrackDetails RequestCurrentTrack()
        {
            Model_TrackDetails ret = new Model_TrackDetails();

            CheckStatus();

            if (_cfid.error == null)
            {
                if (_Current_Status.track != null)
                {
                    if (_Current_Status.track.track_type != "ad")
                    {
                        ret.Title = _Current_Status.track.track_resource.name;
                        ret.Artist = _Current_Status.track.artist_resource.name;
                        ret.Album = _Current_Status.track.album_resource.name;
                    }
                    else
                    {
                        ret.Title = "Playing Ad";
                        ret.Artist = null;
                        ret.Album = null;
                    }
                }
                else
                {
                    ret.Title = "Not listening to any music";
                }

                if (CurrentTrack.Album != ret.Album || CurrentTrack.Artist != ret.Artist || CurrentTrack.Title != ret.Title)
                {
                    if (TrackChanged != null)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            TrackChanged(typeof(Wrapper_Spotify), CurrentTrack);
                        }));
                    }

                    CurrentTrack = ret;
                }

                if (!_tmr.Enabled) _tmr.Start();
            }
            else
            {
                Debug.WriteLine(string.Format("Spotify returned a error {0} (0x{1})", _cfid.error.message, _cfid.error.type));
            }

            return ret;
        }

        public static void Dispose()
        {
            _API = null;
            _cfid = null;
            _tmr.Stop();
        }
        #endregion
    }
}

/*
 * Wrapper_Skype
 * author: Jayson Ragasa
 * date: Aug. 14, 2015
 */

using SKYPE4COMLib;
using System;
using System.Diagnostics;

namespace MoodSwing.Wrappers
{
    public class Wrapper_Skype
    {
        #region vars
        static Skype _skype = null;
        #endregion

        #region properties

        #endregion

        #region ctors
        public Wrapper_Skype()
        {
            
        }
        #endregion

        #region subscribed events

        #endregion

        #region methods
        public static void Init()
        {
            _skype = new Skype();
        }

        public static void ChangeMood(string mood)
        {
            if (_skype == null) Init();

            try
            {
                _skype.CurrentUserProfile.MoodText = mood;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void Dispose()
        {
            _skype = null;
        }
        #endregion
    }
}

/*
 * Wrapper_Skype
 * author: Jayson Ragasa
 * date: Aug. 14, 2015
 */

using SKYPE4COMLib;

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

            _skype.CurrentUserProfile.MoodText = mood;
        }

        public static void Dispose()
        {
            _skype = null;
        }
        #endregion
    }
}

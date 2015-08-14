using System.Timers;
using MoodSwing.Model;
using MoodSwing.Wrappers;

namespace MoodSwing.Effects
{
    public class StatusEffects
    {
        Timer _tmr;
        int _type = 0;

        private string _Status = string.Empty;
        public string Status
        {
            get { return _Status; }
            set
            {
                if(_Status != value)
                {
                    _Status = value;
                }
            }
        }

        private int _TimerDuration = 2500;
        public int TimerDuration
        {
            get { return _TimerDuration; }
            set
            {
                if (_TimerDuration != value)
                {
                    _TimerDuration = value;
                }
            }
        }

        public StatusEffects()
        {
            _tmr = new Timer()
            {
                Interval = this.TimerDuration
            };
            _tmr.Elapsed += _tmr_Elapsed;

            Start();
        }
        static StatusEffects _i = new StatusEffects();
        public static StatusEffects I { get { return _i; } }

        void _tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            switch (this._type)
            {
                case 0: // STATIC
                    Updater.I.EnQ(this.Status, 1);
                    break;
                case 1: // SCROLL
                    Scroll();
                    break;
                case 2: // DANCER
                    Dancer();
                    break;
            }
        }

        public void Start()
        {
            _tmr.Start();
        }

        public void UpdateEffect(int type)
        {
            scroll_index = 0;
            dancer_index = 0;

            this._type = type;
        }

        public void UpdateTrackDetails(string status)
        {
            this.Status = status;
        }

        int scroll_index = 0;
        public void Scroll()
        {
            string new_stat = string.Empty;

            new_stat = this.Status.Substring(this.scroll_index) + this.Status.Substring(0, this.scroll_index);
            Updater.I.EnQ(new_stat, 1);

            this.scroll_index++;
            if (scroll_index >= this.Status.Length) scroll_index = 0;
        }

        int dancer_index = 0;
        string[] dancer_array = {
                                    "♫ (>'-')>", "<('-'<) ♫", "♪ ^(' - ')^ ♫"
                                };
        public void Dancer()
        {
            if (dancer_index >= dancer_array.Length) dancer_index = 0;

            Updater.I.EnQ(dancer_array[dancer_index] + " " + this.Status, 1);

            dancer_index++;
            
        }
    }
}

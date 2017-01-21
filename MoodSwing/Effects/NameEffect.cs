using System.Timers;

namespace MoodSwing.Effects
{
    public class NameEffect
    {
        Timer _tmr;
        int _type = 0;

        private string _Status = string.Empty;
        public string Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                }
            }
        }

        private int _TimerDuration = 7000;
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

        public NameEffect()
        {
            _tmr = new Timer()
            {
                Interval = this.TimerDuration
            };
            _tmr.Elapsed += _tmr_Elapsed;

            Start();
        }
        static NameEffect _i = new NameEffect();
        public static NameEffect I { get { return _i; } }

        void _tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            switch (this._type)
            {
                case 0: // STATIC
                    Updater.I.EnQ(this.Status + "|2", 1);
                    break;
                case 1: // SCROLL
                    Scroll();
                    break;
                case 2: // DANCER
                    Dancer();
                    break;
                case 3: // NOTES DANCE
                    DanceNotes();
                    break;
            }
        }

        public void Start()
        {
            _tmr.Start();
        }

        public void UpdateEffect(int type)
        {
            anim_index = 0;

            this._type = type;
        }

        public void UpdateTrackDetails(string status)
        {
            this.Status = status;
        }

        int anim_index = 0;
        public void Scroll()
        {
            string new_stat = string.Empty;

            new_stat = this.Status.Substring(this.anim_index) + this.Status.Substring(0, this.anim_index);
            Updater.I.EnQ(new_stat + "|2", 1);

            this.anim_index++;
            if (anim_index >= this.Status.Length) anim_index = 0;
        }

        string[] dancer_array = {
                                    "♫ (>'-')>", "<('-'<) ♫", "♪ ^(' - ')^ ♫"
                                };
        public void Dancer()
        {
            if (anim_index >= dancer_array.Length) anim_index = 0;

            Updater.I.EnQ(this.Status + " " + dancer_array[anim_index] + "|2", 1);

            anim_index++;

        }

        string[] notes_array = {
                                    "♪_", "♫", "_♪", "..", "♫♫", "♫♪", "♪♪"
                                };
        public void DanceNotes()
        {
            if (anim_index >= notes_array.Length) anim_index = 0;

            Updater.I.EnQ(this.Status + " " + notes_array[anim_index] + "|2", 1);

            anim_index++;

        }
    }
}

/*
 * Updater
 * author: Jayson Ragasa
 * date: Aug. 14, 2015
 */

using System;
using System.Collections;
using System.ComponentModel;

namespace MoodSwing
{
    public class Updater
    {
        #region vars
        BackgroundWorker bgWorker;
        Queue _q;
        #endregion

        #region properties

        #endregion

        #region ctors
        public Updater()
        {
            this._q = new Queue();

            this.bgWorker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            this.bgWorker.DoWork += bgWorker_DoWork;
            this.bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            this.bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
        }
        static Updater _i = new Updater();
        public static Updater I { get { return _i; } }
        #endregion

        #region subscribed events
        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!this.bgWorker.CancellationPending)
            {
                //bgWorker.ReportProgress(0, this._q.Dequeue());

                if (this._q.Count > 0)
                {
                    string dq = (string)this._q.Dequeue();

                    int update_section = 1;

                    if(dq.Contains("|"))
                    {
                        update_section = Convert.ToInt16(dq.Substring(dq.IndexOf("|") + 1));
                    }

                    switch (update_section)
                    {
                        case 1:
                            Wrappers.Wrapper_Skype.ChangeMood(dq);

                            break;
                        case 2:
                            Wrappers.Wrapper_Skype.ChangeName(dq.Substring(0, dq.IndexOf("|")));
                            break;
                    }
                }

                if (this._q.Count == 0) { break; }
            }
        }
        #endregion

        #region methods
        public void EnQ(string q, int type)
        {
            this._q.Enqueue(q);

            if (!this.bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync(type);
            }
        }

        public void Start()
        {
            this.bgWorker.RunWorkerAsync();
        }

        public void Stahp()
        {
            this.bgWorker.CancelAsync();
        }
        #endregion
    }
}

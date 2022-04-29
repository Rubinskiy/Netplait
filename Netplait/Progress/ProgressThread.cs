using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Netplait.Progress
{
    class ProgressThread
    {
        private static Thread th = new Thread(new ThreadStart(showProgressForm));
        public void startProgress()
        {
            th = new Thread(new ThreadStart(showProgressForm));
            th.Name = "Processing request...";
            th.Start();
        }

        private static void showProgressForm()
        {
            try
            {
                Progress.ProgressRequest ProgressForm = new Progress.ProgressRequest();
                ProgressForm.ShowDialog();
            }
            catch (Exception ex) { }
        }

        public void stopProgress()
        {
            try
            {
                th.Abort();
                th = null;
            }
            catch (Exception ex) { }
        }
    }
}

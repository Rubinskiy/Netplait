using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Netplait.Core.MenuHelpers;

namespace Netplait.Core.Loaders
{
    class Loading
    {
        private static Thread th = new Thread(new ThreadStart(Show));
        public void Start()
        {
            th = new Thread(new ThreadStart(Show));
            th.Name = "Loading...";
            th.Start();
        }

        private static void Show()
        {
            try
            {
                Loader ProgressForm = new Loader();
                ProgressForm.ShowDialog();
            }
            catch (Exception ex) { }
        }

        public void Stop()
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

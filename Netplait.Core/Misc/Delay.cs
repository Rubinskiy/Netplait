using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Core.Misc
{
    public class Delay
    {
        public static void Call(int time, Action action)
        {
            Timer t = new Timer();
            t.Interval = time;
            t.Tick += new EventHandler((obj, ev) =>
            {
                action();

                t.Stop();
                t.Enabled = false;
                t.Dispose();
            });
            t.Start();
        }
    }
}

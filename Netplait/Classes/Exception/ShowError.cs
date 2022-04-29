using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netplait.Classes.Exception
{
    public class ShowError
    {
        public static void Show(string title, string message)
        {            
            Classes.Exception.ExceptionBox err = new Classes.Exception.ExceptionBox(title, message);
            err.ShowDialog();
        }
    }
}

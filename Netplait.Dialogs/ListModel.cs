using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Netplait.Dialogs
{
    class ListModel
    {
        public string Name = "";
        public Image Picture = null;

        public ListModel(string name, Image picture)
        {
            Name = name;
            Picture = picture;
        }
    }
}

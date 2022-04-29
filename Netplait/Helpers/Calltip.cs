using ScintillaNET;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netplait.Helpers
{
    public class Calltip
    {
        public static void InitCallTip(ScintillaNET.Scintilla TextArea, Color DarkFore, Color LightFore, bool Darktheme)
        {
            if(Darktheme == true) {
                TextArea.Styles[Style.CallTip].SizeF = 8.25f;
                TextArea.Styles[Style.CallTip].ForeColor = DarkFore;
                TextArea.Styles[Style.CallTip].BackColor = Color.FromArgb(66, 66, 69);
            }
            else{
                TextArea.Styles[Style.CallTip].SizeF = 8.25f;
                TextArea.Styles[Style.CallTip].ForeColor = LightFore;
                TextArea.Styles[Style.CallTip].BackColor = Color.FromArgb(246, 246, 246);
            }
        }
    }
}

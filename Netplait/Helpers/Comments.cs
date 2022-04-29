using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNET;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Netplait.Helpers
{
    public class Comments
    {
        //NOT WORKING??????????
        //NOT WORKING??????????
        //NOT WORKING??????????
        //NOT WORKING??????????
        //NOT WORKING??????????
        //NOT WORKING??????????
        //NOT WORKING??????????
        //NOT WORKING??????????
        //replace 'Comment line' and 'Uncomment line' with 'Comment' and 'Uncomment'

        private static void Replace(Scintilla TextArea, int num, string text)
        {
            TextArea.TargetStart = TextArea.Lines[num].Position;
            TextArea.TargetEnd = TextArea.Lines[num].EndPosition;
            TextArea.ReplaceTarget(text);
        }

        public static void Comment(Scintilla TextArea, TabControl tb)
        {
            TextArea.TargetFromSelection();
            int lstart = TextArea.LineFromPosition(TextArea.TargetStart);
            int lend = TextArea.LineFromPosition(TextArea.TargetEnd);
            string pattern = tb.SelectedTab.Text.ToString();

            //PYTHON
            if (FileTypes.FileExtensionHandler.IsPythonFile(pattern))
            {
                for (int i = lstart; i <= lend; i++)
                    Replace(TextArea, i, "# " + TextArea.Lines[i].Text);
            }
            //HTML
            else if (FileTypes.FileExtensionHandler.IsHTMLFile(pattern))
            {
                for (int i = lstart; i <= lend; i++)
                    Replace(TextArea, i, "<!-- " + TextArea.Lines[i].Text + " -->");
            }
            //CSS
            else if (FileTypes.FileExtensionHandler.IsCSSFile(pattern))
            {
                for (int i = lstart; i <= lend; i++)
                    Replace(TextArea, i, "/* " + TextArea.Lines[i].Text + " */");
            }
            //Js
            else if (FileTypes.FileExtensionHandler.IsJSFile(pattern))
            {
                for (int i = lstart; i <= lend; i++)
                    Replace(TextArea, i, "// " + TextArea.Lines[i].Text);
            }
            //Blext
            else
            {
                //NULL
            }
        }

        public static void Uncomment(Scintilla TextArea, TabControl tb)
        {
            string pattern = tb.SelectedTab.Text.ToString();
            string curLine = TextArea.Lines[TextArea.CurrentLine].Text;

            TextArea.TargetFromSelection();
            int lstart = TextArea.LineFromPosition(TextArea.TargetStart);
            int lend = TextArea.LineFromPosition(TextArea.TargetEnd);

            //PYTHON
            if (FileTypes.FileExtensionHandler.IsPythonFile(pattern))
            {
                for (int i = lstart; i <= lend; i++)
                    Replace(TextArea, i, TextArea.Lines[i].Text.Replace("#", ""));
            }
            //HTML
            else if (FileTypes.FileExtensionHandler.IsHTMLFile(pattern))
            {
                for (int i = lstart; i <= lend; i++)
                    Replace(TextArea, i, TextArea.Lines[i].Text.Replace("<!--", "").Replace("-->", ""));
            }
            //CSS
            else if (FileTypes.FileExtensionHandler.IsCSSFile(pattern))
            {
                for (int i = lstart; i <= lend; i++)
                    Replace(TextArea, i, TextArea.Lines[i].Text.Replace("*/", "").Replace("/*", ""));
            }
            //Js
            else if (FileTypes.FileExtensionHandler.IsJSFile(pattern))
            {
                for (int i = lstart; i <= lend; i++)
                    Replace(TextArea, i, TextArea.Lines[i].Text.Replace("//", ""));
            }
            //Blext
            else
            {
                //NULL
            }
        }
    }
}
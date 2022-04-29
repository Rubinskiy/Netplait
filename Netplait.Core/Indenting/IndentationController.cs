using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Netplait.Core.Indenting
{
    public class IndentationController
    {
        /// <summary>
        /// Indent regex
        /// </summary>
        public static Regex indentLevel = new Regex("^[\\s]*");
        public static Regex indenter = new Regex("{\\s$");

        /// <summary>
        /// Outdent words
        /// </summary>
        public static string[] outdentWords = new string[]
        {
            "end",
            "when",
            "else",
            "}"
        };
    }
}

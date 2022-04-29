using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Netplait.FileTypes
{
    //p.s. so bad in Regex, wasted a day of work.
    public class FileExtensionHandler
    {
        /// <summary>
        /// Check whether pattern is a valid Python file
        /// </summary>
        public static bool IsPythonFile(string pattern)
        {
            //.py, .pyw, .py3, .pyi, .pyx
            if (pattern.EndsWith(".py", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".pyw", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".py3", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".pyi", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".pyx", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".py*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".pyw*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".py3*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".pyi*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".pyx*", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check whether pattern is a valid HTML file
        /// </summary>
        public static bool IsHTMLFile(string pattern)
        {           
            //.html, .htm, .xhtml
            if (pattern.EndsWith(".html", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".htm", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".shtml", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".shtm", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".xhtml", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".html*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".htm*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".shtml*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".shtm*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".xhtml*", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check whether pattern is a valid CSS file
        /// </summary>
        public static bool IsCSSFile(string pattern)
        {
            //.css
            if (pattern.EndsWith(".css", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".css*", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check whether pattern is a valid Javascript file
        /// </summary>
        public static bool IsJSFile(string pattern)
        {
            //.js
            if (pattern.EndsWith(".js", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".js*", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// TODO - SQL
        /// </summary>
        public static bool IsSQLFile(string pattern)
        {
            //.sq3, .sql, .ddl, .dml
            if (pattern.EndsWith(".sq3", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".sql", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".ddl", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".dml", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".sq3*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".sql*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".ddl*", StringComparison.OrdinalIgnoreCase) ||
                pattern.EndsWith(".dml*", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}

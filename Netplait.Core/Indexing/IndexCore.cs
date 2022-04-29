using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netplait.Core.Indexing
{
    public class IndexCore
    {
        /// <summary>
        /// This method will build the Auto-Complete menu to be assigned to the CSS editor
        /// </summary>
        public static List<string> IndexAutocomplete(string path)
        {
            int counter = 0;
            string line;
            List<string> list = new List<string>();
            System.IO.StreamReader autocomp = new System.IO.StreamReader(path);
            while ((line = autocomp.ReadLine()) != null)
            {
                list.Add(line);
                counter++;
            }
            autocomp.Close();
            list.Sort();
            return list.OrderBy(m => m).ToList();
        }

        /// <summary>
        /// This method is responsible for reading a file containing a list of words seperated by spaces
        /// to be added to a keyword list for autocomplete
        /// </summary>
        public static string IndexKeywords(string path)
        {
            string contents = System.IO.File.ReadAllText(path);
            return contents.ToString();
        }
    }
}

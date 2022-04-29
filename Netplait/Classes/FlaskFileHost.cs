using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Classes
{
    public class FlaskFileHost
    {        
        static string FlaskProj = Application.StartupPath + "\\Templates\\Flask Project\\Flask Template\\";
        static string TempDir = Application.StartupPath + "\\Templates\\Temp\\";

        /// <summary>
        /// This method will create the pre-configured Flask project files for use.
        /// </summary>
        /// <param name="path">The path to the project</param>
        /// <param name="projectName">The project name</param>
        /// <param name="author">The author string value</param>
        /// <param name="desc">The file description string value</param>
        public static void CreateFlaskProjectFiles(string path, string projectName, string author, string desc)
        {
            //Copy temporary project, otherwise you loose the template
            CopyFiles.DirectoryCopy(Application.StartupPath + "\\Templates\\Flask Project\\", TempDir, true);

            //Modify the temp files based on the given data
            Directory.Move(TempDir + "Flask Template", TempDir + projectName);
            File.Move(TempDir + projectName + "\\$(PROJNAME).networkspace", TempDir + projectName + "\\" + projectName + ".networkspace");
            Directory.Move(TempDir + projectName + "\\$(NAME)", TempDir + projectName + "\\" + projectName);

            string ProjFile = File.ReadAllText(TempDir + projectName + "\\" + projectName + ".networkspace");
            ProjFile = ProjFile.Replace("Project Name = ###", "Project Name = " + projectName);
            ProjFile = ProjFile.Replace("Date Created = ###", "Date Created = " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ProjFile = ProjFile.Replace("Project Path = ###", path + "\\" + projectName + "\\" + projectName);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + ".networkspace", ProjFile);

            string AppPy = File.ReadAllText(TempDir + projectName + "\\" + projectName + "\\app.py");
            AppPy = AppPy.Replace("Project maintained by: $Author", "Project maintained by: " + author);
            AppPy = AppPy.Replace("$Desc", desc);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + "\\app.py", AppPy);

            CopyFiles.DirectoryCopy(TempDir, path, true);
            Directory.Delete(TempDir + projectName, true);
        }        
    }
}

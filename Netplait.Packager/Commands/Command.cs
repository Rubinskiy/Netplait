using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Netplait.Packager.Commands;
using System.Windows.Forms;
using System.IO;

namespace Netplait.Packager.Commands
{
    public class Command
    {
        static string TempDir = Application.StartupPath + "\\Templates\\Temp\\";
        public static void SelectFolderPath(TextBox tb)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;

            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                tb.Text = fbd.SelectedPath;
                Environment.SpecialFolder root = fbd.RootFolder;
            }
        }

        public static void GetPythonPath(DataReceivedEventHandler console_output, DataReceivedEventHandler console_error)
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";

            CmdProcess.StartInfo.CreateNoWindow = true;
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;
            CmdProcess.StartInfo.RedirectStandardOutput = true;
            CmdProcess.StartInfo.RedirectStandardError = true;

            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(console_output);
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(console_error);

            CmdProcess.Start();
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            CmdProcess.StandardInput.WriteLine("where python&exit");
        }

        public static void GetPythonVersion(DataReceivedEventHandler console_error, DataReceivedEventHandler version_output, EventHandler version_exit)
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";
            CmdProcess.StartInfo.CreateNoWindow = true;
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;
            CmdProcess.StartInfo.RedirectStandardOutput = true;
            CmdProcess.StartInfo.RedirectStandardError = true;
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(console_error);
            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(version_output);
            CmdProcess.EnableRaisingEvents = true;
            CmdProcess.Exited += new EventHandler(version_exit);

            CmdProcess.Start();
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            CmdProcess.StandardInput.WriteLine("python -V&exit");
        }

        public static void StartProcess(DataReceivedEventHandler output, DataReceivedEventHandler error, EventHandler exit, string arguments)
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";
            CmdProcess.StartInfo.CreateNoWindow = true;
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;
            CmdProcess.StartInfo.RedirectStandardOutput = true;
            CmdProcess.StartInfo.RedirectStandardError = true;
            CmdProcess.EnableRaisingEvents = true;
            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(output);
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(error);
            CmdProcess.Exited += new EventHandler(exit);

            CmdProcess.Start();
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            CmdProcess.StandardInput.WriteLine(arguments);
        }

        /// <summary>
        /// This method will create the pre-configured Python project files for use.
        /// </summary>
        /// <param name="mainpy">Decides whether welcome script will be included</param>
        /// <param name="path">The path to the project</param>
        /// <param name="projectName">The project name</param>
        /// <param name="author">The author string value</param>
        /// <param name="desc">The file description string value</param>
        public static void CreatePythonProjectFiles(bool mainpy, string path, string projectName, string author, string desc)
        {
            //Copy temporary project, otherwise you loose the template
            Copy.Dir(Application.StartupPath + "\\Templates\\Python Project\\", TempDir, true);

            //Modify the temp files based on the given data
            Directory.Move(TempDir + "Python Template", TempDir + projectName);
            File.Move(TempDir + projectName + "\\$(PROJNAME).networkspace", TempDir + projectName + "\\" + projectName + ".networkspace");
            Directory.Move(TempDir + projectName + "\\$(NAME)", TempDir + projectName + "\\" + projectName);

            string ProjFile = File.ReadAllText(TempDir + projectName + "\\" + projectName + ".networkspace");
            ProjFile = ProjFile.Replace("Project Name = ###", "Project Name = " + projectName);
            ProjFile = ProjFile.Replace("Date Created = ###", "Date Created = " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ProjFile = ProjFile.Replace("Project Path = ###", path + "\\" + projectName + "\\" + projectName);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + ".networkspace", ProjFile);

            if (mainpy)
            {
                string MainPy = File.ReadAllText(TempDir + projectName + "\\" + projectName + "\\main.py");
                MainPy = MainPy.Replace("Project maintained by: $Author", "Project maintained by: " + author);
                MainPy = MainPy.Replace("$Desc", desc);
                MainPy = MainPy.Replace("($USER)", Environment.UserName);
                File.WriteAllText(TempDir + projectName + "\\" + projectName + "\\main.py", MainPy);
            }
            else
            {
                File.Delete(TempDir + projectName + "\\" + projectName + "\\main.py");
            }

            Copy.Dir(TempDir, path, true);
            Directory.Delete(TempDir + projectName, true);
        }

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
            Copy.Dir(Application.StartupPath + "\\Templates\\Flask Project\\", TempDir, true);

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

            Copy.Dir(TempDir, path, true);
            Directory.Delete(TempDir + projectName, true);
        }

        /// <summary>
        /// This method will create the pre-configured Django project files for use.
        /// </summary>
        /// <param name="path">The path to the project</param>
        /// <param name="projectName">The project name</param>
        /// <param name="author">The author string value</param>
        /// <param name="desc">The file description string value</param>
        public static void CreateDjangoProjectFiles(string path, string projectName)
        {
            //Copy temporary project, otherwise you loose the template
            Copy.Dir(Application.StartupPath + "\\Templates\\Django Project\\", TempDir, true);

            //Modify the temp files based on the given data
            Directory.Move(TempDir + "Django Template", TempDir + projectName);
            File.Move(TempDir + projectName + "\\$(PROJNAME).networkspace", TempDir + projectName + "\\" + projectName + ".networkspace");

            string ProjFile = File.ReadAllText(TempDir + projectName + "\\" + projectName + ".networkspace");
            ProjFile = ProjFile.Replace("Project Name = ###", "Project Name = " + projectName);
            ProjFile = ProjFile.Replace("Date Created = ###", "Date Created = " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ProjFile = ProjFile.Replace("Project Path = ###", path + "\\" + projectName + "\\" + projectName);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + ".networkspace", ProjFile);

            Copy.Dir(TempDir, path, true);
            Directory.Delete(TempDir + projectName, true);
        }

        /// <summary>
        /// This method will create the pre-configured Pyramid project files for use.
        /// </summary>
        /// <param name="path">The path to the project</param>
        /// <param name="projectName">The project name</param>
        /// <param name="author">The author string value</param>
        /// <param name="desc">The file description string value</param>
        public static void CreatePyramidProjectFiles(string path, string projectName, string author, string desc)
        {
            //Copy temporary project, otherwise you loose the template
            Copy.Dir(Application.StartupPath + "\\Templates\\Pyramid Project\\", TempDir, true);

            //Modify the temp files based on the given data
            Directory.Move(TempDir + "Pyramid Template", TempDir + projectName);
            File.Move(TempDir + projectName + "\\$(PROJNAME).networkspace", TempDir + projectName + "\\" + projectName + ".networkspace");
            Directory.Move(TempDir + projectName + "\\$(NAME)", TempDir + projectName + "\\" + projectName);

            string ProjFile = File.ReadAllText(TempDir + projectName + "\\" + projectName + ".networkspace");
            ProjFile = ProjFile.Replace("Project Name = ###", "Project Name = " + projectName);
            ProjFile = ProjFile.Replace("Date Created = ###", "Date Created = " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ProjFile = ProjFile.Replace("Project Path = ###", path + "\\" + projectName + "\\" + projectName);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + ".networkspace", ProjFile);

            string MainPy = File.ReadAllText(TempDir + projectName + "\\" + projectName + "\\main.py");
            MainPy = MainPy.Replace("Project maintained by: $Author", "Project maintained by: " + author);
            MainPy = MainPy.Replace("$Desc", desc);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + "\\main.py", MainPy);

            Copy.Dir(TempDir, path, true);
            Directory.Delete(TempDir + projectName, true);
        }

        /// <summary>
        /// This method will create the pre-configured Bottle project files for use.
        /// </summary>
        /// <param name="path">The path to the project</param>
        /// <param name="projectName">The project name</param>
        /// <param name="author">The author string value</param>
        /// <param name="desc">The file description string value</param>
        public static void CreateBottleProjectFiles(string path, string projectName, string author, string desc)
        {
            //Copy temporary project, otherwise you loose the template
            Copy.Dir(Application.StartupPath + "\\Templates\\Bottle Project\\", TempDir, true);

            //Modify the temp files based on the given data
            Directory.Move(TempDir + "Bottle Template", TempDir + projectName);
            File.Move(TempDir + projectName + "\\$(PROJNAME).networkspace", TempDir + projectName + "\\" + projectName + ".networkspace");
            Directory.Move(TempDir + projectName + "\\$(NAME)", TempDir + projectName + "\\" + projectName);

            string ProjFile = File.ReadAllText(TempDir + projectName + "\\" + projectName + ".networkspace");
            ProjFile = ProjFile.Replace("Project Name = ###", "Project Name = " + projectName);
            ProjFile = ProjFile.Replace("Date Created = ###", "Date Created = " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ProjFile = ProjFile.Replace("Project Path = ###", path + "\\" + projectName + "\\" + projectName);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + ".networkspace", ProjFile);

            string MainPy = File.ReadAllText(TempDir + projectName + "\\" + projectName + "\\main.py");
            MainPy = MainPy.Replace("Project maintained by: $Author", "Project maintained by: " + author);
            MainPy = MainPy.Replace("$Desc", desc);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + "\\main.py", MainPy);

            Copy.Dir(TempDir, path, true);
            Directory.Delete(TempDir + projectName, true);
        }

        /// <summary>
        /// This method will create the pre-configured Web project files for use.
        /// </summary>
        /// <param name="path">The path to the project</param>
        /// <param name="projectName">The project name</param>
        /// <param name="author">The author string value</param>
        /// <param name="desc">The file description string value</param>
        public static void CreateWebProjectFiles(string path, string projectName, string author, string desc)
        {
            //Copy temporary project, otherwise you loose the template
            Copy.Dir(Application.StartupPath + "\\Templates\\Web Project\\", TempDir, true);

            //Modify the temp files based on the given data
            Directory.Move(TempDir + "Web Template", TempDir + projectName);
            File.Move(TempDir + projectName + "\\$(PROJNAME).networkspace", TempDir + projectName + "\\" + projectName + ".networkspace");
            Directory.Move(TempDir + projectName + "\\$(NAME)", TempDir + projectName + "\\" + projectName);

            string ProjFile = File.ReadAllText(TempDir + projectName + "\\" + projectName + ".networkspace");
            ProjFile = ProjFile.Replace("Project Name = ###", "Project Name = " + projectName);
            ProjFile = ProjFile.Replace("Date Created = ###", "Date Created = " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ProjFile = ProjFile.Replace("Project Path = ###", path + "\\" + projectName + "\\" + projectName);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + ".networkspace", ProjFile);

            string Index = File.ReadAllText(TempDir + projectName + "\\" + projectName + "\\index.html");
            Index = Index.Replace("Project maintained by: $Author", "Project maintained by: " + author);
            Index = Index.Replace("$Desc", desc);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + "\\index.html", Index);

            Copy.Dir(TempDir, path, true);
            Directory.Delete(TempDir + projectName, true);
        }

        /// <summary>
        /// This method will create an empty project.
        /// </summary>
        /// <param name="path">The path to the project</param>
        /// <param name="projectName">The project name</param>
        /// <param name="author">The author string value</param>
        /// <param name="desc">The file description string value</param>
        public static void CreateBlankProject(string path, string projectName)
        {
            //Copy temporary project, otherwise you loose the template
            Copy.Dir(Application.StartupPath + "\\Templates\\Empty Project\\", TempDir, true);

            //Modify the temp files based on the given data
            Directory.Move(TempDir + "Empty Template", TempDir + projectName);
            File.Move(TempDir + projectName + "\\$(PROJNAME).networkspace", TempDir + projectName + "\\" + projectName + ".networkspace");
            Directory.Move(TempDir + projectName + "\\$(NAME)", TempDir + projectName + "\\" + projectName);

            string ProjFile = File.ReadAllText(TempDir + projectName + "\\" + projectName + ".networkspace");
            ProjFile = ProjFile.Replace("Project Name = ###", "Project Name = " + projectName);
            ProjFile = ProjFile.Replace("Date Created = ###", "Date Created = " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            ProjFile = ProjFile.Replace("Project Path = ###", path + "\\" + projectName + "\\" + projectName);
            File.WriteAllText(TempDir + projectName + "\\" + projectName + ".networkspace", ProjFile);

            Copy.Dir(TempDir, path, true);
            Directory.Delete(TempDir + projectName, true);
        }
    }
}

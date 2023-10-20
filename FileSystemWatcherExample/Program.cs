using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileSystemWatcherExample
{
    class Program
    {
        //
        private static string appDir = AppDomain.CurrentDomain.BaseDirectory; //full path into the debug/release folder 
        private static string[] arrFolder = Regex.Split(appDir, "bin"); //Split the path at the bin folder
        private static string dataFolder = arrFolder[0] + @"App_Data"; //Data folder
        private static string LogFile = dataFolder + @"\Log.txt";
        private static readonly string WatchThisDir = @"C:\Temp\WatchThisFolder";

        static void Main(string[] args)
        {
            MakeDirectory(dataFolder);
            RunFolderWatcher(WatchThisDir);
        }

        private static void MakeDirectory(string folder_path)
        {
            if (!Directory.Exists(folder_path))
            {
                Directory.CreateDirectory(folder_path);
            }
        }

        public static void WriteToTextFile(string file_path, string[] arrLines)
        {
            File.AppendAllLines(file_path, arrLines);
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void RunFolderWatcher(string directoryPath)
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                // Directory to watch
                watcher.Path = directoryPath; 

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the application");
                while (Console.Read() != 'q') ;
            }

        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            string msg = String.Format("{0} | Machine:{1} | File: {2} - {3}", DateTime.Now, Environment.MachineName, e.Name, e.ChangeType);
            string[] arrLines = new string[] { msg };
            WriteToTextFile(LogFile, arrLines);
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            string msg = String.Format("{0} | Machine:{1} | File: {2} - {3}", DateTime.Now, Environment.MachineName, e.Name, e.ChangeType);
            string[] arrLines = new string[] { msg };
            WriteToTextFile(LogFile, arrLines);
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }
    }
}

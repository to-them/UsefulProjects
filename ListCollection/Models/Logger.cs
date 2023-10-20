using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListCollection.Models
{
    public class Logger
    {
        private string _logFolder { get; set; }
        public Logger(string folder_path)
        {
            _logFolder = folder_path;
        }

        public void WriteToTextFile(string text_message)
        {
            string _newentry = "********** " + DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " **********";
            string _textfile = GetToDayFilePath(_logFolder);
            StreamWriter sw = new StreamWriter(_textfile, true);
            //sw.WriteLine(_newentry);
            sw.WriteLine(DateTime.Now + " | " + text_message);
            //sw.WriteLine("");
            sw.Flush();
            sw.Close();
        }

        private string GetToDayFilePath(string folder_path)
        {
            //Create file directory if it does not exist
            DirectoryInfo dir = new DirectoryInfo(folder_path);
            if (!dir.Exists)
            {
                dir.Create();
            }

            string _year = DateTime.Now.Year.ToString();
            string _month = DateTime.Now.Month.ToString();
            string _day = DateTime.Now.Day.ToString();
            string _textfile = "Today_" + _month + "-" + _day + "-" + _year + ".txt";

            return folder_path + _textfile;
        }
    }
}

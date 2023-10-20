using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONCRUD
{
    public class bll_ErrorHandling
    {
        public static void WriteError(string exception_caught)
        {
            //string folderName = "errorFolder";
            string fileDir = bll_Utilities.ErrorFolder; // bll_Utilities.AppKeyLookup(folderName);
            //Create file directory if it does not exist
            DirectoryInfo dir = new DirectoryInfo(fileDir);
            if (!dir.Exists)
            {
                dir.Create();
            }

            string sPathName = bll_Utilities.ErrorFolder; //bll_Utilities.AppKeyLookup(folderName);
            string _sLogFormat = "********** " + DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " **********";
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            //sErrorTime = "_" + sMonth + "-" + sDay + "-" + sYear;
            string _sErrorTime = "_" + sMonth + "-" + sDay + "-" + sYear + ".txt";

            StreamWriter sw = new StreamWriter(sPathName + _sErrorTime, true);
            sw.WriteLine(_sLogFormat);
            sw.WriteLine(exception_caught);
            sw.WriteLine("");
            sw.Flush();
            sw.Close();
        }
    }
}

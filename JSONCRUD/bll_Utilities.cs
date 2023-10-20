using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace JSONCRUD
{
    public class bll_Utilities
    {
        private static string appDir = AppDomain.CurrentDomain.BaseDirectory; //full path into the debug/release folder 
        private static string[] lines = Regex.Split(appDir, "bin"); //Split the path at the bin folder
        public static string app_data = lines[0] + @"App_Data";

        private const string connName = "DBconn";
        public static string connStr = AppKeyLookup(connName);

        public static string AppDataFolder = app_data; //HostingEnvironment.MapPath(AppKeyLookup("fileFolder"));
        public static string ErrorFolder = app_data + "\\Exceptions\\Error"; //HostingEnvironment.MapPath(AppKeyLookup("errorFolder"));

        //public const string PersonJsonFile = "Persons.json";
        public static string PersonJsonFile = AppKeyLookup("PersonJsonFile");
        public static string PersonJsonFilePath = app_data + "\\" + PersonJsonFile;

        //sql connection string        
        /// <summary>
        /// Get the connection string specified in the config file
        /// </summary>
        /// <param name="name">if other than Default = DBconn</param>
        /// <returns>Returns connection string</returns>
        private static string ConnString(string name = connName)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        //App_Settings lookup
        /// <summary>
        /// Get App_Settings value specified in the config file
        /// </summary>
        /// <param name="key">App_Settings key</param>
        /// <returns>Returns the value</returns>
        public static string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListCollection.Models
{
    public class General
    {
        public static string DataFolder = GetAppSettingsValue("AppDataFolder");
        public static string LogFolder = GetAppSettingsValue("LogFolder");
        public static string TemplateFileName = GetAppSettingsValue("TemplateFileName");
        public static string TemplatejFileName = GetAppSettingsValue("TemplatejFileName");

        public static string GenerateTempName(int len_char)
        {
            // add any more characters that you wish! 
            string strChars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";
            Random r = new Random();
            string strNewPassword = string.Empty;
            for (int i = 0; i < len_char; i++)
            {
                int intRandom = r.Next(0, strChars.Length);
                strNewPassword += strChars[intRandom];
            }
            return strNewPassword;

        }

        public static string GetAppSettingsValue(string app_setting_key)
        {
            //throw new NotImplementedException();
            string val = "";
            try
            {
                var settings = ConfigurationManager.AppSettings;
                //string result = settings[app_setting_key] ?? "Not Found";
                //Console.WriteLine(result);
                if (settings[app_setting_key] == null)
                {
                    val = "Not Found!";
                }
                else
                {
                    val = settings[app_setting_key];
                }

            }
            catch (ConfigurationErrorsException)
            {
                val = "Error reading app settings";
            }

            return val;
        }

        public static string GetConnectionString(string connection_name)
        {
            //throw new NotImplementedException();
            // Assume failure.
            string connStr = null;
            try
            {
                // Look for the name in the connectionStrings section.
                ConnectionStringSettings connSettings =
                    ConfigurationManager.ConnectionStrings[connection_name];

                // If found, return the connection string.
                if (connSettings != null)
                    connStr = connSettings.ConnectionString;
                else
                    connStr = "Not Found!";

                //Console.WriteLine(connStr);
            }
            catch (ConfigurationErrorsException)
            {
                connStr = "Error reading connection strings";
            }

            return connStr;
        }
    }
}

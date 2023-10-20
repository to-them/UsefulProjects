using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONCRUD.PersonLogic
{
    public static class bll_person_json_processor
    {
        //private static string fileFolder = "filePath";
        public static string FullFilePath(this string fileName)
        {
            string fileDir = bll_Utilities.app_data;
            //Create file directory if it does not exist
            DirectoryInfo dir = new DirectoryInfo(fileDir);
            if (!dir.Exists)
            {
                dir.Create();
            }

            string file_path = fileDir + "\\" + fileName;
            if (!File.Exists(file_path))
            {
                using (var jsonFile = File.Create(file_path))
                {
                    // interact with myFile here, it will be disposed automatically                   
                }

                //Create Initial/Seed JSON file
                WriteJsonData(bll_SeedTable.SeedPerson, file_path);

            }

            return file_path;
        }

        #region :Create
        //Write from object
        /// <summary>
        /// Save List objects to file
        /// </summary>
        /// <param name="ls">List objects</param>
        /// <param name="file_path">Full path to the file location</param>
        public static void WriteJsonData(List<bel_Person> ls, string file_path)
        {
            try
            {
                // Pass the "person list" object for conversion object to JSON string  
                //string jsondata = new JavaScriptSerializer().Serialize(ls); //System.Web.Script.Serialization
                string jsondata = JsonConvert.SerializeObject(ls, Formatting.Indented); //Ref: Extension - Json.NET
                File.WriteAllText(file_path, jsondata);
            }
            catch (System.Exception ex)
            {
                string error = String.Format("WriteJsonData(ls, file_path) {0}", ex.Message);
                bll_ErrorHandling.WriteError(error);
            }

        }

        //Re-write the file after manipulation
        /// <summary>
        /// Save string format of JSON data
        /// </summary>
        /// <param name="file_path">Full path to the file location</param>
        /// <param name="json_data">JSON data in string format</param>
        public static void WriteJsonData(string file_path, string json_data)
        {
            try
            {
                File.WriteAllText(file_path, json_data);
            }
            catch (System.Exception ex)
            {
                string error = String.Format("WriteJsonData(file_path, json_data) exception: {0}", ex.Message);
                bll_ErrorHandling.WriteError(error);
            }

        }

        //Append person new person object and return updated data
        /// <summary>
        /// Add new person object to the list 
        /// </summary>
        /// <param name="json_basedata">JSON data in string format</param>
        /// <param name="obj">New person object</param>
        /// <param name="file_path">Full path to the file location</param>
        /// <returns>Returns string formatted JSON data</returns>
        public static string AddObjectsToJson(string json_basedata, bel_Person obj, string file_path)
        {
            //Add to the list
            List<bel_Person> ls = JsonConvert.DeserializeObject<List<bel_Person>>(json_basedata);
            ls.Add(obj);

            //Write to file
            WriteJsonData(ls, file_path);

            //return JsonConvert.SerializeObject(ls);
            return JsonConvert.SerializeObject(ls, Formatting.Indented);
        }
        #endregion

        #region :Read
        //Get json data in string format
        /// <summary>
        /// Read all rows in the the file
        /// </summary>
        /// <param name="file_path">Full path of the file location</param>
        /// <returns>Returns data in string format</returns>
        public static string getJsonDataString(string file_path)
        {
            string jData = "";
            using (StreamReader r = new StreamReader(file_path))
            {
                jData = r.ReadToEnd();
            }

            return jData;
        }

        //Get json data in person list object
        /// <summary>
        /// Read all rows in JSON file
        /// </summary>
        /// <param name="file_path">Full path to the file</param>
        /// <returns>Returns List object of the file</returns>
        public static List<bel_Person> getJsonDataObject(string file_path)
        {
            List<bel_Person> persons = new List<bel_Person>();
            using (StreamReader r = new StreamReader(file_path))
            {
                string jData = r.ReadToEnd();
                persons = JsonConvert.DeserializeObject<List<bel_Person>>(jData);
            }

            return persons;
        }

        //FIND ITEM
        /// <summary>
        /// Find a record in the JSON file
        /// </summary>
        /// <param name="json_data">JSON data in string format</param>
        /// <param name="id">Record id</param>
        /// <returns>Returns string data</returns>
        public static string FindItem(string json_data, int id)
        {
            JArray json_data_arr = JArray.Parse(json_data);

            //This will get the row data of the id
            string id_coln = PersonTableColn.Id.ToString();
            var companyToDeleted = json_data_arr.FirstOrDefault(obj => obj[id_coln].Value<int>() == id);
            if (companyToDeleted != null)
            {
                string row_data = "Row: \n" + companyToDeleted;
                string row_id = "Row ID: " + companyToDeleted[id_coln].ToString();
                return "\nFOUND \n" + row_id + "\n\n" + row_data;
            }
            else
            {
                return "\nNOT FOUND";
            }

        }
        #endregion

        #region :Update
        //Update Method 2 (Prefer)
        public static bool UpdateRow(string file_path, int id, bel_Person p)
        {
            List<bel_Person> ls = getJsonDataObject(file_path);
            var itemindex = ls.FindIndex(y => y.Id == id);
            var item = ls.ElementAt(itemindex);
            if (item != null)
            {
                ls.RemoveAt(itemindex); //Delete then insert again after update

                item.FirstName = p.FirstName;
                item.LastName = p.LastName;
                item.EmailAddress = p.EmailAddress;
                item.CellphoneNumber = p.CellphoneNumber;
                //item.FullName = p.FullName;

                ls.Insert(itemindex, item); //Insert updated row

                string update_json_data = JsonConvert.SerializeObject(ls, Formatting.Indented);
                WriteJsonData(file_path, update_json_data);

                return true;
            }
            else
            {
                return false;
            }

        }

        //Update Row
        public static bool UpdateRow(string json_data, int id, string json_file_path)
        {
            List<bel_Person> ls = JsonConvert.DeserializeObject<List<bel_Person>>(json_data);

            var itemindex = ls.FindIndex(y => y.Id == id);
            var item = ls.ElementAt(itemindex);
            if (item != null)
            {
                ls.RemoveAt(itemindex);

                item.LastName = "Test Last Name";
                item.FirstName = "Test First Name";

                string fn = item.FirstName;
                string ln = item.LastName;

                ls.Insert(itemindex, item);

                string update_json_data = JsonConvert.SerializeObject(ls, Formatting.Indented);
                WriteJsonData(json_file_path, update_json_data);

                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region :Delete
        //Delete Row
        /// <summary>
        /// Delete record by id
        /// </summary>
        /// <param name="file_path">Full path to file location</param>
        /// <param name="id">Record id</param>
        /// <returns>Returns true if deleted</returns>
        public static bool DeleteRow(string file_path, int id)
        {
            List<bel_Person> ls = getJsonDataObject(file_path);
            var getRow = ls.FirstOrDefault(x => x.Id == id);
            if (getRow != null)
            {
                ls.Remove(getRow);
                string update_json_data = JsonConvert.SerializeObject(ls, Formatting.Indented);
                WriteJsonData(file_path, update_json_data);
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONCRUD.PersonLogic
{
    /// <summary>
    /// Perform JSON File Operations
    /// </summary>
    public class bll_Person_json : ICRUD<bel_Person>
    {
        string file_path = bll_Utilities.PersonJsonFile.FullFilePath(); //Use "PersonJsonFile" to include file name to "FullFilePath"
        //string file_path = bll_Utilities.PersonJsonFilePath;
        public bool Create(bel_Person obj)
        {
            try
            {
                List<bel_Person> people = bll_person_json_processor.getJsonDataObject(file_path);

                int currentId = 1;

                if (people != null)
                {
                    currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
                }

                //if (people.Count > 0)
                //{
                //    currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
                //}

                obj.Id = currentId;

                string json_basedata = bll_person_json_processor.getJsonDataString(file_path);
                string s = bll_person_json_processor.AddObjectsToJson(json_basedata, obj, file_path);

                return true;
            }
            catch (System.Exception ex)
            {
                string error = "Create person json file exceptio: " + ex.Message;
                bll_ErrorHandling.WriteError(error);
                return false;
            }
        }

        public bel_Person Retrieve(string key)
        {
            var person = (from s in RetrieveAll() where key == s.Id.ToString() select s).FirstOrDefault();
            return person;
        }

        public List<bel_Person> RetrieveAll()
        {
            return bll_person_json_processor.getJsonDataObject(file_path);
        }

        public bool Update(bel_Person obj, string key)
        {
            int id = Int32.Parse(key);
            if (bll_person_json_processor.UpdateRow(file_path, id, obj))
                return true;
            else
                return false;
        }

        public bool Delete(string key)
        {
            int id = Int32.Parse(key);
            if (bll_person_json_processor.DeleteRow(file_path, id))
                return true;
            else
                return false;
        }
    }
}

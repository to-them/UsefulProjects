using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ListCollection.Models
{
    [Serializable]
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class TemplateUI
    {
        TemplateService logic = new TemplateService(General.DataFolder, General.TemplateFileName, General.LogFolder);

        #region :CREATE
        public void CreateRecord()
        {
            string name = General.GenerateTempName(8);
            Template t = new Template()
            {
                Name = name,
                Notes = "Notes...",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            if (logic.Create(t))
                Console.WriteLine("\n {0} added", name);
            else
                Console.WriteLine("\n Unable to add {0}!", name);

            ShowRecords();
        }
        #endregion

        #region :READ
        public void ShowRecords()
        {
            List<Template> ls = logic.RetrieveAll();
            foreach (Template t in ls)
            {
                Console.WriteLine("ID:{0} Name:{1}, Notes:{2}, Created:{3}, Upadeted:{4}",
                    t.Id, t.Name, t.Notes, t.CreatedOn, t.UpdatedOn);
            }
        }

        public void ShowRecord(int id)
        {
            Template t = logic.Retrieve(id.ToString());
            if (t != null)
            {
                Console.WriteLine("ID:{0} Name:{1}, Notes:{2}, Created:{3}, Upadeted:{4}",
                    t.Id, t.Name, t.Notes, t.CreatedOn, t.UpdatedOn);
            }

        }
        #endregion

        #region :UPDATE
        public void UpdateRecord(int id)
        {
            Template rec = logic.Retrieve(id.ToString());

            string name = "John Bastis"; //General.GenerateTempName(8);
            if (rec != null)
            {
                Template t = new Template()
                {
                    Id = id,
                    Name = name,
                    Notes = General.GenerateTempName(12),
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                if (logic.Update(t, id.ToString()))
                    Console.WriteLine("\n Id:{0} updated", id);
                else
                    Console.WriteLine("\n Id:{0} not found!", id);
            }

        }
        #endregion

        #region :DELETE
        public void DeleteRecord(int id)
        {
            if (logic.Delete(id.ToString()))
                Console.WriteLine("\n Id:{0} deleted", id);
            else
                Console.WriteLine("\n Id:{0} not found!", id);
        }
        #endregion

    }

    public class TemplateService : ICRUD<Template>
    {
        #region :Init
        private string _file_path { get; set; }
        private string _logFolder { get; set; }

        /// <summary>
        /// Set json file path, and create one if not exist
        /// </summary>
        /// <param name="data_folder_path">Path to data folder</param>
        /// <param name="file_name">serialized file name ex: tools.ayi</param>
        public TemplateService(string data_folder_path, string file_name, string log_folder_path)
        {
            //Set data folder
            DirectoryInfo dir = new DirectoryInfo(data_folder_path);
            if (!dir.Exists)
            {
                dir.Create();
            }

            //Set json file path
            _file_path = data_folder_path + file_name;
            if (!File.Exists(_file_path))
            {
                //Create initial/seed json file
                bool v = CreateByList(InitSeed);
            }

            _logFolder = log_folder_path;

        }

        /// <summary>
        /// Initialize the JSON file
        /// </summary>
        private List<Template> InitSeed
        {
            get
            {
                List<Template> ls = new List<Template>();
                ls.Add(new Template()
                {
                    Id = 1,
                    Name = General.GenerateTempName(8),
                    Notes = "Notes " + General.GenerateTempName(12),
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                });
                ls.Add(new Template()
                {
                    Id = 2,
                    Name = General.GenerateTempName(8),
                    Notes = "Notes " + General.GenerateTempName(12),
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                });
                ls.Add(new Template()
                {
                    Id = 3,
                    Name = General.GenerateTempName(8),
                    Notes = "Notes " + General.GenerateTempName(12),
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                });
                return ls;
            }
        }

        /// <summary>
        /// This will serialized the list to file
        /// </summary>
        /// <param name="ls">Data List</param>
        /// <returns>True if good and false if bad</returns>
        private bool CreateByList(List<Template> ls)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(_file_path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            Logger log = new Logger(_logFolder);

            try
            {
                using (fs)
                {
                    bf.Serialize(fs, ls);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                log.WriteToTextFile(ex.InnerException.Message);
                return false;
            }
        }

        #endregion

        #region :CREATE
        public bool Create(Template obj)
        {
            Logger log = new Logger(_logFolder);
            List<Template> ls = RetrieveAll();

            //Generate new id number
            int currentId = 1;
            if (ls != null)
            {
                currentId = ls.OrderByDescending(x => x.Id).First().Id + 1;
            }
            obj.Id = currentId;
            //=====

            ls.Add(obj); //Add new to the list

            if (CreateByList(ls)) //Write to file
            {
                log.WriteToTextFile("Id:" + currentId + " ADDED!");
                return true;
            }
            else
                return false;
        }
        #endregion

        #region :READ
        /// <summary>
        /// This will deserialized the file to list
        /// </summary>
        /// <returns>List if count > 0 else null</returns>
        public List<Template> RetrieveAll()
        {
            var ls = new List<Template>();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(_file_path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Logger log = new Logger(_logFolder);
            try
            {
                using (fs)
                {
                    ls = (List<Template>)bf.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                log.WriteToTextFile(ex.InnerException.Message);
            }

            return ls;
        }
        public Template Retrieve(string key)
        {
            int id = Int32.Parse(key);
            var item = RetrieveAll().FirstOrDefault(x => x.Id == id);

            return item;
        }
        #endregion

        #region :UPDATE
        public bool Update(Template obj, string key)
        {
            Logger log = new Logger(_logFolder);
            int id = Int32.Parse(key);
            List<Template> ls = RetrieveAll();
            var itemindex = ls.FindIndex(y => y.Id == id);
            var item = ls.ElementAt(itemindex);
            if (item != null)
            {
                obj.CreatedOn = item.CreatedOn;

                ls.RemoveAt(itemindex); //Delete then insert again after update

                item.Name = obj.Name;
                item.Notes = obj.Notes;
                item.CreatedOn = obj.CreatedOn;
                item.UpdatedOn = obj.UpdatedOn;

                ls.Insert(itemindex, item); //Insert updated row
                log.WriteToTextFile("Id:" + key + " UPDATED!");
                CreateByList(ls);   //Save

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region :DELETE
        public bool Delete(string key)
        {
            Logger log = new Logger(_logFolder);
            int id = Int32.Parse(key);
            List<Template> ls = RetrieveAll();
            var getRow = ls.FirstOrDefault(x => x.Id == id);
            if (getRow != null)
            {
                ls.Remove(getRow);  //Delete the record
                log.WriteToTextFile("Id:" + key + " DELETED!");
                CreateByList(ls);   //Save

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

using JSONCRUD.PersonLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONCRUD
{
    public class bll_SeedTable
    {
        #region :Seed Person Data
        /// <summary>
        /// Initialize the JSON file
        /// </summary>
        public static List<bel_Person> SeedPerson
        {
            get
            {
                List<bel_Person> ls = new List<bel_Person>();
                ls.Add(new bel_Person()
                {
                    Id = 0,
                    FirstName = "Seed First",
                    LastName = "Seed Last",
                    EmailAddress = "seed@email.com",
                    CellphoneNumber = "132-000-9800"
                });
                return ls;
            }
        }
        #endregion
    }
}

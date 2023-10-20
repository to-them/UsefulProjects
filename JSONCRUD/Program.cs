using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSONCRUD.PersonLogic;

namespace JSONCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Person");
            bll_Person_json p = new bll_Person_json();
            string id = "6";

            //Test Create - Passed
            //p.Create(CreateObject);

            //Test Update - Passed
            //p.Update(EditObject, id);

            //Test Delete - Passed
            //p.Delete("7");

            //Test Read One - Passed
            var person = p.Retrieve(id);
            if(person != null)
            {
                Console.WriteLine($"id:{person.Id} First: {person.FirstName} Last:{person.LastName}");
            }
            else
            {
                Console.WriteLine($"No data was returned for id:{id}!");
            }

            Console.WriteLine("-----------------\n\nList");

            //Test Read All - Passed
            //Read All
            var persons = p.RetrieveAll();
            if (persons != null)
            {
                int i = 0;
                foreach (var s in persons)
                {
                    i++;
                    Console.WriteLine($"{i} id:{s.Id} Last:{s.LastName} First:{s.FirstName}");
                }
            }
            else
            {
                Console.WriteLine("No data was returned!");
            }

            Console.Write("\nPress any key to exit: ");
            Console.ReadKey();
        }

        //Create Person Object
        static bel_Person CreateObject
        {
            get
            {
                bel_Person per = new bel_Person()
                {
                    Id = 0,
                    FirstName = "fSeed-44",
                    LastName = "lSeed-44",
                    EmailAddress = "seed44@email.com",
                    CellphoneNumber = "713-000-9800"
                };

                return per;
            }
        }

        //Edit Person Object
        static bel_Person EditObject
        {
            get
            {
                bel_Person per = new bel_Person()
                {
                    //Id = 0,
                    FirstName = "fSeed-45",
                    LastName = "lSeed-45",
                    EmailAddress = "seed45@email.com",
                    CellphoneNumber = "713-000-9800"
                };

                return per;
            }
        }

    }
}

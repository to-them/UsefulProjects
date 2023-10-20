using ListCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=-=-=-= Using List Collection =-=-=-=\n");
            //This uses json file
            TemplatejUI temp = new TemplatejUI();
            temp.ShowRecords();
            temp.CreateRecord();
            Console.WriteLine("\n -------- \n");
            temp.ShowRecord(3);
            temp.UpdateRecord(1);
            temp.DeleteRecord(2);
            Console.WriteLine("\n -------- \n");
            temp.ShowRecords();
            Console.WriteLine("===========================\n");

            //This uses serialized file
            /*
            TemplateUI temp = new TemplateUI();
            temp.ShowRecords();
            temp.CreateRecord();
            Console.WriteLine("\n -------- \n");
            temp.ShowRecord(3);
            temp.UpdateRecord(3);
            temp.DeleteRecord(2);
            Console.WriteLine("\n -------- \n");
            temp.ShowRecords();
            Console.WriteLine("===========================\n");
            */

            Console.Write("\n Press any key to exit:");
            Console.ReadKey();
        }
    }
}

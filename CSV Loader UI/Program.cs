using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Loader_UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var errorMessage = "";
            var isSuccess = SqlBuilder.SetUp(ref errorMessage);

            if (isSuccess)
            {
                ReadFile readFile = new ReadFile();
                FindCustomer findCustomer = new FindCustomer();

                MainMenu mainMenu = new MainMenu(readFile, findCustomer);
                mainMenu.Run();
            }
            else
            {
                Console.WriteLine($"Something went wrong on first time Initialization of database: {errorMessage}");
                Console.ReadKey();
            }
            
        }

    }
}

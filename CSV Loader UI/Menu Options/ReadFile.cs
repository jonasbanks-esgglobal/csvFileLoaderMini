
using BusinessLayer.ModelMaps;
using BusinessLayer.Models;
using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Loader_UI
{
    public class ReadFile : IReadFile
    {
        private bool _run;
        public void Run()
        {
            _run = true;

            while (_run)
            {
                Console.Clear();
                Console.WriteLine(MenuConstants.ReadFileHeaderText);
                Console.WriteLine(MenuConstants.ReadFileExampleText);
                Console.WriteLine(MenuConstants.ReturnText);

                var filePath = Console.ReadLine().ToString();

                if (filePath.Length == 1 && filePath.ToUpper().Equals("C"))
                {
                    _run = false;
                    break;
                }

                try
                {
                    List<CustomerModel> customerModels = new List<CustomerModel>();
                    List<Task> customerInformationTasks = new List<Task>();
                    using (var reader = new StreamReader(filePath))
                    {
                        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            csvReader.Configuration.RegisterClassMap<CustomerModelMap>();
                            customerModels.AddRange(csvReader.GetRecords<CustomerModel>());

                            foreach (var customerModel in customerModels)
                            {
                                customerInformationTasks.Add(SendCustomerInformationAsync(customerModel));
                            }

                            var res = Task.WhenAll(customerInformationTasks);
                            var counter = 0;
                            Console.Clear();
                            Console.Write("Saving");
                            while (!res.IsCompleted)
                            {
                                if(counter % 40 == 0)
                                {
                                    Console.Write(".");
                                    counter++;
                                }
                                else if (counter > 1000)
                                {
                                    Console.WriteLine();
                                    counter = 0;
                                }
                            }
                            Console.Clear();
                            Console.WriteLine(MenuConstants.FinishedReadingFileText);

                            var input = Console.ReadLine();
                            if (string.IsNullOrEmpty(input) || !input.ToUpper().Equals("R"))
                            {
                                _run = false;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"There was an issue when trying to read the file:\n {e.Message} \n Press Enter to continue");
                    Console.ReadKey();
                }
            }
        }

        public async Task<bool> SendCustomerInformationAsync(CustomerModel customerModel)
        {
            var wasSuccess = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52252/");
                var customerJson = JsonConvert.SerializeObject(customerModel);
                HttpResponseMessage response = await client.PostAsync("API/Customer", new StringContent(customerJson, Encoding.UTF8, "application/json"));
                if (response != null && response.IsSuccessStatusCode)
                {
                    wasSuccess = true;
                }
            }
            return wasSuccess;
        }
    }
}

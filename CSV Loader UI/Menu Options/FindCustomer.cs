using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Loader_UI
{
    public class FindCustomer : IFindCustomer
    {
        private bool _run;
        public void Run()
        {
            _run = true;
            while (_run)
            {
                Console.Clear();
                Console.WriteLine(MenuConstants.FindCustomerHeaderText);
                Console.WriteLine(MenuConstants.ReturnText);
                Console.Write(MenuConstants.CustomerSearchFieldText);

                var refInput = Console.ReadLine();
                if (refInput.Length == 1 && refInput.ToUpper().Equals("C"))
                {
                    _run = false;
                }
                else
                {
                    Console.Clear();
                    var res = GetCustomerInformationByCustomerReferenceAsync(refInput);
                    Console.WriteLine("Searching");
                    var counter = 0;
                    while (!res.IsCompleted)
                    {
                        if (counter % 1000 == 0)
                        {
                            Console.Write(".");
                        }
                        else if (counter > 40000)
                        {
                            Console.WriteLine();
                            counter = 0;
                        }
                        counter++;
                    }
                    Console.WriteLine();
                    Console.WriteLine(MenuConstants.SearchResultText);
                    Console.WriteLine(res.Result);
                    Console.WriteLine(MenuConstants.ResetText);
                    Console.ReadKey();
                }
            }
        }

        public async Task<string> GetCustomerInformationByCustomerReferenceAsync(string customerReference)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52252/");
                HttpResponseMessage response = await client.GetAsync($"API/Customer/GetCustomerByRef?customerReference={customerReference}");
                if (response != null && response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return $"Something went wrong in the API when searching for the specified customer: {response.Content.ReadAsStringAsync().Result}";
                }
            }
        }
    }
}

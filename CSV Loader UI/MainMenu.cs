using System;

namespace CSV_Loader_UI
{
    public class MainMenu
    {
        private bool runProgram;
        private IReadFile _readFile;
        private IFindCustomer _findCustomer;
        public MainMenu(IReadFile readFileInterface,IFindCustomer findCustomerInterface)
        {
            _readFile = readFileInterface;
            _findCustomer = findCustomerInterface;
        }
        public void Run()
        {
            runProgram = true;
            while (runProgram)
            {
                Console.Clear();
                Console.WriteLine("CSV Loader: To read in a file, type R + Enter to start, to find an existing customer press F + Enter:");
                Console.WriteLine("To exit press X");
                Console.WriteLine("Press E for a special display!");
                var input = Console.ReadLine().ToUpper();

                switch (input)
                {
                    case "R":
                        _readFile.Run();
                        break;
                    case "F":
                        _findCustomer.Run();
                        break;
                    case "X":
                        //Exit program
                        runProgram = false;
                        break;
                    case "E":
                        //Run console EsG flag display? - if time.
                        break;
                    default:
                        Console.WriteLine("Your command was not recognized, press Enter to try again");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}

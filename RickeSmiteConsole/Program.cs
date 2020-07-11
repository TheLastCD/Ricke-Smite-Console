using System;

namespace RickeSmiteConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Manager runtime = new Manager();
            runtime.FillBase();
            while (true)
            {
                Console.WriteLine("Hello What would you like to use:");
                Console.WriteLine("1) Search for Item");
                Console.WriteLine("2) Add a new Item");
                Console.WriteLine("3) View Full List");
                switch (runtime.Int_Verify(3))
                {
                    case 1:
                        runtime.Search();
                        break;
                    case 2:
                        runtime.AddItem();
                        break;
                    case 3:
                        runtime.PrintAll();
                        break;
                    default:
                        runtime.PrintAll();
                        break;

                }
                
            }
            
        }
        
       
    }
}

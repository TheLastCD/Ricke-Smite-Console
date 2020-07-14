using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace RickeSmiteConsole
{
    public class Manager
    {
        public List<StockItem> StockList = new List<StockItem>();
        string Path = @"Collection.txt";
        public void FillBase()
        {
            foreach(string item in File.ReadLines(Path))
            {
                string[] itemAttributes = item.Split(',');

                StockItem.CheckMiss holder = StockItem.CheckMiss.In_Place;
                if (itemAttributes[6] == "Checked_out")
                    holder = StockItem.CheckMiss.Checked_Out;
                if (itemAttributes[6] == "Missing")
                    holder = StockItem.CheckMiss.Missing;

                StockItem addNew = new StockItem(Int32.Parse(itemAttributes[0]),itemAttributes[1],itemAttributes[2],Int32.Parse(itemAttributes[3]),itemAttributes[4],itemAttributes[5],holder);
            }
        }

        public void AddItem()
        {
            int nextROS = 0;
            try
            {
                nextROS = StockList.Last().RosID + 1;
            }
            catch
            {
            }
            
            Console.Write("Please enter an manufacturer:");
            string manu = Verifier(false).ToLower();
            Console.Write("Please enter an manufacturer ID/ PartNumber:");
            string MID = Verifier(false).ToLower();
            Console.Write("Please enter stock amount:");
            int stock = Int32.Parse(Verifier(true));
            Console.Write("Please enter the location:");
            string LOC = Verifier(false).ToLower();
            Console.Write("Please enter the box name:");
            string BN = Verifier(false).ToLower();
            Console.Write("Please enter the State of this item (1) Checked Out 2)In_Place 3) Missing)");
            StockItem.CheckMiss State= StockItem.CheckMiss.Checked_Out;
            switch (Int32.Parse(Verifier(true)))
            {
                case 1:
                    break;
                case 2:
                    State = StockItem.CheckMiss.In_Place;
                    break;
                case 3:
                    State = StockItem.CheckMiss.Missing;
                    break;
                default:
                    Console.WriteLine("Chose an incorrect number its defaulted to Checked Out");
                    break;
            }
            NewEntity(nextROS, manu, MID, stock, LOC, BN, State);
            while (true)
            {
                Console.WriteLine("Would you like to enter another item: Y/N");
                string redo = Verifier(false);
                if (redo.ToUpper() == "Y")
                {
                    AddItem();
                }
                if(redo.ToUpper() == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid response");
                }

            }
            


        }
        private void NewEntity(int R, string M, string MID, int S, string LOC, string BN, StockItem.CheckMiss C)
        {
            StockItem item = new StockItem(R,M,MID,S,LOC,BN,C);
            StockList.Add(item);
            if (!File.Exists(Path))
            {
                Console.WriteLine("The file cannot be found");
            }
            else
            {
                using (StreamWriter sw = File.AppendText(Path))
                {
                    sw.WriteLine($"{R},{M},{MID},{S},{LOC},{BN},{C}");
                }
            }

        }

        public void Search()
        {
            System.Collections.Generic.IEnumerable<RickeSmiteConsole.StockItem> queryresult;
            Console.WriteLine("what category would you like to search?");
            Console.WriteLine("1) By Manufacturer");
            Console.WriteLine("2) By Manufacturer/Part Number");
            Console.WriteLine("3) By Stock Amount");
            Console.WriteLine("4) By Location");
            Console.WriteLine("5) By Box Name");
            Console.WriteLine("6) By State currently doesnt work");
            switch (Int_Verify(6))
            {
                case 1:
                    queryresult = from x in StockList
                                  where x.Manufacturer == Querier("Manufacturer")
                                  select x;
                    break;
                case 2:
                    queryresult = from x in StockList
                                       where x.ManufacturerID == Querier("Part number")
                                       select x;
                    break;
                case 3:
                    queryresult = from x in StockList
                                       where x.Stock == Int32.Parse(Querier("Stock Amount"))
                                       select x;
                    break;
                case 4:
                    queryresult = from x in StockList
                                       where x.Location == Querier("Location")
                                       select x;
                    break;
                case 5:
                    queryresult = from x in StockList
                                       where x.BoxName == Querier("Box Name")
                                       select x;
                    break;
                case 6:
                    queryresult = from x in StockList
                                       where x.Manufacturer == Querier("State")
                                       select x;
                    break;
            }

        }
        public void PrintAll()
        {
            Console.WriteLine("ROSIS  |Manufacturer   |Manufacturer Number   |Stock|Location   |Box Name   |");
            foreach(StockItem item in StockList)
            {
                Console.WriteLine($"{item.RosID}|{item.Manufacturer}|{item.ManufacturerID}|{item.Stock}|{item.Location}|{item.BoxName}|");
            }
        }

        // for filtering text
        public string Verifier(bool isInt)
        {
            string verif = Console.ReadLine();
            if (isInt)
            {
                try
                {
                    Int32.Parse(verif);
                }
                catch
                {
                    Console.WriteLine("Please enter a number");
                    Verifier(isInt);
                }
            }
            else
            {
                while (true)
                {
                    if (verif == "")
                    {
                        Console.WriteLine("please enter a word");
                        verif = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
                

            }
            return verif;
            
        }

        // for ranges of selection
        public int Int_Verify(int max)
        {
            int Chosen = -1;
            while (true)
            {
                try
                {
                    Chosen = Int32.Parse(Console.ReadLine());
                    if (Chosen == -1 || Chosen >= max)
                    {
                        Console.WriteLine($"please enter a value in the range 1 - {max}");
                    }
                    else
                        break;
                }
                catch
                {
                    Console.WriteLine("Please enter a number!!!");
                }
            }
            return Chosen;
        }
        public string Querier(string querytype)
        {
            string query = "";
            while (true)
            {
                Console.WriteLine($"Please enter the {querytype} you are looking for: ");
                query = Console.ReadLine();
                if(query == "")
                {
                    Console.WriteLine("Please enter something");
                }
                else
                {
                    break;
                }
            }
            return query.ToLower();
            
        }
    }
}

using System;
namespace RickeSmiteConsole
{
    public class StockItem
    {
        //defaults
        public enum CheckMiss
        {
            In_Place,
            Checked_Out,
            Missing
        }

        //Storage
        public string Manufacturer, ManufacturerID, Location, BoxName;
        public int RosID, Stock;
        public CheckMiss Whereis;

        //Constructor
        public StockItem(int R, string M, string MID,int S, string LOC,string BN, CheckMiss C)
        {
            RosID = R;
            Manufacturer = M;
            ManufacturerID = MID;
            Stock = S;
            Location = LOC;
            BoxName = BN;
            Whereis = C;
        }

        
    }
}

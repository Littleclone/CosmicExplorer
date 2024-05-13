using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public static class ItemIndex
    {
        private static string? itemName;
        public static bool IsValid(int ID)
        {
            if(ID > 0)
            {
                if(ID == 1)
                {
                    itemName = "petrol";
                    return true;
                }
                if(ID == 2)
                {
                    itemName = "asteroid pieces";
                    return true;
                }
                if(ID == 3)
                {
                    itemName = "iron_ore";
                    return true;
                }
                if(ID == 4)
                {
                    itemName = "copper_ore";
                    return true;
                }
            }
            return false;
        }
        public static string ItemName(int ID)
        {
            if(ID > 0)
            {
                if(ID == 1)
                {
                    return itemName = "Benzin";
                }
                if(ID == 2)
                {
                    return itemName = "Asteroiden stücke";
                }
                if(ID == 3)
                {
                    return itemName = "Eisen Erz";
                }
                if(ID == 4)
                {
                    return itemName = "Kupfer Erz";
                }
                return "ItemID not found";
            }
            return "ItemID > 0";
        }
    }
}

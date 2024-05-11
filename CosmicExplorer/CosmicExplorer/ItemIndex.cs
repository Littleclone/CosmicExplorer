using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public static class ItemIndex
    {
        private static string itemName = "Item";
        public static bool IsValid(int ID)
        {
            if(ID > 0)
            {
                if(ID == 1)
                {
                    itemName = "petrol";
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
                    return itemName = "petrol";
                }
                return "ItemID not found";
            }
            return "ItemID > 0";
        }
    }
}

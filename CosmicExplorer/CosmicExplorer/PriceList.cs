using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public static class PriceList
    {
        //a string so i know what item is this ID
        private static string item;
        public static float BuyPrice(string itemID, float discount)
        {
            if(itemID == "01")
            {
                item = "Petrol";
                if(discount > 0)
                {
                    return 100f * discount;
                }
                return 100f;
            }
            if(itemID == "02")
            {
                item = "asteroid pieces";
                if(discount > 0)
                {
                    return 25f * discount;
                }
                return 25f;
            }
            if(itemID == "03")
            {
                item = "iron_ore";
                if (discount > 0)
                {
                    return 250f * discount;
                }
                return 250f;
            }
            if(itemID == "04")
            {
                item = "copper_ore";
                if (discount > 0)
                {
                    return 150f * discount;
                }
                return 150f;
            }
            return 9999999;
        }
        public static float SellPrice(string itemID, float extraCharge)
        {
            if (itemID == "01")
            {
                item = "Petrol";
                if (extraCharge > 0)
                {
                    return 75f / extraCharge;
                }
                return 75f;
            }
            if (itemID == "02")
            {
                item = "asteroid pieces";
                if (extraCharge > 0)
                {
                    return 10f / extraCharge;
                }
                return 10f;
            }
            if (itemID == "03")
            {
                item = "iron_ore";
                if (extraCharge > 0)
                {
                    return 175f / extraCharge;
                }
                return 175f;
            }
            if (itemID == "04")
            {
                item = "copper_ore";
                if (extraCharge > 0)
                {
                    return 100f / extraCharge;
                }
                return 100f;
            }
            return 1;
        }
    }
}

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
                    itemName = "fuel";
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
                if(ID == 5)
                {
                    itemName = "asteroid dust";
                    return true;
                }
                if(ID == 6)
                {
                    itemName = "iron_ingot";
                    return true;
                }
                if (ID == 7)
                {
                    itemName = "copper_ingot";
                    return true;
                }
                if (ID == 8)
                {
                    itemName = "coal";
                    return true;
                }
                if(ID == 9)
                {
                    itemName = "gold_ore";
                    return true;
                }
                if (ID == 10)
                {
                    itemName = "gold_ingot";
                    return true;
                }
                if (ID == 11)
                {
                    itemName = "a jar with algae";
                    return true;
                }
                if (ID == 12)
                {
                    itemName = "dirt from an alien planet";
                    return true;
                }
                if (ID == 13)
                {
                    itemName = "Experimental alloy";
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
                    return itemName = "Treibstoff";
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
                if(ID == 5)
                {
                    return itemName = "Asteroiden Staub";
                }
                if(ID == 6)
                {
                    return itemName = "Eisenbarrren";
                }
                if(ID == 7)
                {
                    return itemName = "Kupferbarren";
                }
                if(ID == 8)
                {
                    return itemName = "Kohle";
                }
                if (ID == 9)
                {
                    return itemName = "Gold Erz";
                }
                if (ID == 10)
                {
                    return itemName = "Goldbarren";
                }
                if (ID == 11)
                {
                    return itemName = "Ein Glas mit Algen";
                }
                if (ID == 12)
                {
                    return itemName = "Erde von einem Fremden Planeten";
                }
                if (ID == 13)
                {
                    return itemName = "Experimentelle Legierung";
                }
                return "ItemID not found";
            }
            return "ItemID > 0";
        }
    }
}

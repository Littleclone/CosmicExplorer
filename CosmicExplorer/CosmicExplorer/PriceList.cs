using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Copyright 2024 Littleclone

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

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
                item = "fuel";
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
                    return 75f * discount;
                }
                return 75f;
            }
            if(itemID == "04")
            {
                item = "copper_ore";
                if (discount > 0)
                {
                    return 45f * discount;
                }
                return 45f;
            }
            if(itemID == "05")
            {
                item = "asteroid dust";
                if(discount > 0)
                {
                    return 10f * discount;
                }
                return 10f;
            }
            if(itemID == "06")
            {
                item = "iron_ingot";
                if(discount > 0)
                {
                    return 250f * discount;
                }
                return 250f;
            }
            if (itemID == "07")
            {
                item = "copper_ingot";
                if (discount > 0)
                {
                    return 175f * discount;
                }
                return 175f;
            }
            if (itemID == "08")
            {
                item = "coal";
                if (discount > 0)
                {
                    return 55f * discount;
                }
                return 55f;
            }
            if (itemID == "09")
            {
                item = "gold_ore";
                if (discount > 0)
                {
                    return 150f * discount;
                }
                return 150f;
            }
            if (itemID == "10")
            {
                item = "gold_ingot";
                if (discount > 0)
                {
                    return 500f * discount;
                }
                return 500f;
            }
            return 9999999;
        }
        public static float SellPrice(string itemID, float extraCharge)
        {
            if (itemID == "01")
            {
                item = "fuel";
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
                    return 45f / extraCharge;
                }
                return 45f;
            }
            if (itemID == "04")
            {
                item = "copper_ore";
                if (extraCharge > 0)
                {
                    return 25f / extraCharge;
                }
                return 25f;
            }
            if (itemID == "05")
            {
                item = "asteroid dust";
                if (extraCharge > 0)
                {
                    return 5f / extraCharge;
                }
                return 5f;
            }
            if (itemID == "06")
            {
                item = "iron_ingot";
                if (extraCharge > 0)
                {
                    return 175f / extraCharge;
                }
                return 175f;
            }
            if (itemID == "07")
            {
                item = "copper_ingot";
                if (extraCharge > 0)
                {
                    return 105f / extraCharge;
                }
                return 105f;
            }
            if (itemID == "08")
            {
                item = "coal";
                if (extraCharge > 0)
                {
                    return 20f / extraCharge;
                }
                return 20f;
            }
            if (itemID == "09")
            {
                item = "gold_ore";
                if (extraCharge > 0)
                {
                    return 80f / extraCharge;
                }
                return 80f;
            }
            if (itemID == "10")
            {
                item = "gold_ingot";
                if (extraCharge > 0)
                {
                    return 350f / extraCharge;
                }
                return 350f;
            }
            return 1;
        }
    }
}

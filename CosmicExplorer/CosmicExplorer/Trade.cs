using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Copyright 2024 Littleclone

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

namespace Cosmic_Explorer
{
    public class Trade
    {
        //canSell ist welche Items der NPC verkaufen kann und canBuy welche Items der NPC kaufen kann (also welche Items der Spieler an den NPC verkaufen kann)
        public void TradeInterfaceSell(string canSell, float discount)
        {
            // Folgender Abschnitt nimmt sich den canSell und teilt ihn in einzelne 2-stellige Strings auf, die dann in einer Liste gespeichert werden
            // Dieser String gibt man bei der erstellung des NPC's an, sehe Lea in Program.cs als beispiel an
            int j = 1;
            string x = canSell;
            List<string> canSellList = new List<string>();
            for (int i = 0; i < x.Length; i += 2)
            {
                canSellList.Add(x.Substring(i, Math.Min(2, x.Length - i)));
            }
            j = Convert.ToInt32(canSellList[0]); // Soweit ich mich noch daran erinnere dient das dazu um ein Bug zu beheben, der sonst auftreten würde
            foreach (string i in canSellList) // Dies und das folgende zeigen dann in der Console die Items an, die der NPC verkaufen kann
            {
                if (discount > 0)
                {
                    float d = discount;
                    d *= 100;
                    float z = d - 100;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(ItemIndex.ItemName(j) + ": " + Convert.ToInt32(PriceList.BuyPrice(i, discount)) + " Gold," + " ID: " + i + "  ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("  " + Math.Abs(z) + "% Rabat");
                    Console.ResetColor();
                    Console.WriteLine("");
                    j++;
                    continue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(ItemIndex.ItemName(j) + ": " + Convert.ToInt32(PriceList.BuyPrice(i, discount)) + " Gold," + " ID: " + i);
                    Console.ResetColor();
                    Console.WriteLine("");
                    j++;
                    continue;
                }
            }
        }
        public void TradeInterfaceBuy(string canBuy, float extraCharge)
        {
            // Folgender Abschnitt nimmt sich den canBuy und teilt ihn in einzelne 2-stellige Strings auf, die dann in einer Liste gespeichert werden
            // Dieser String gibt man bei der erstellung des NPC's an, sehe Lea in Program.cs als beispiel an
            int j = 1;
            string y = canBuy;
            List<string> canBuyList = new List<string>();
            for (int i = 0; i < y.Length; i += 2)
            {
                canBuyList.Add(y.Substring(i, Math.Min(2, y.Length - i)));
            }
            j = Convert.ToInt32(canBuyList[0]); // Soweit ich mich noch daran erinnere dient das dazu um ein Bug zu beheben, der sonst auftreten würde
            foreach (string i in canBuyList) // Dies und das folgende zeigen dann in der Console die Items an, die der NPC kaufen kann
            {
                if (extraCharge > 0)
                {
                    float d = extraCharge;
                    d *= 100;
                    float z = d - 100;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(ItemIndex.ItemName(j) + ": " + Convert.ToInt32(PriceList.SellPrice(i, extraCharge)) + " Gold, " + " ID: " + i + "  ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("  " + Math.Abs(z) + "% Aufpreis");
                    Console.ResetColor();
                    Console.WriteLine("");
                    j++;
                    continue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(ItemIndex.ItemName(j) + ": " + Convert.ToInt32(PriceList.SellPrice(i, extraCharge)) + " Gold," + " ID: " + i);
                    Console.ResetColor();
                    Console.WriteLine("");
                    j++;
                    continue;
                }
            }
        }
    }
}

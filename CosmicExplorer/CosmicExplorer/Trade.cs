using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class Trade
    {
        //canSell ist welche Items der NPC verkaufen kann und canBuy welche Items der NPC kaufen kann (also welche Items der Spieler an den NPC verkaufen kann)
        public void TradeInterfaceSell(string canSell, float discount)
        {
            int j = 1;
            string x = canSell;
            List<string> canSellList = new List<string>();
            for (int i = 0; i < x.Length; i += 2)
            {
                canSellList.Add(x.Substring(i, Math.Min(2, x.Length - i)));
            }
            foreach (string i in canSellList)
            {
                if (discount > 0)
                {
                    float d = discount;
                    d *= 100;
                    float z = d - 100;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(ItemIndex.ItemName(j) + ": " + Convert.ToInt32(PriceList.BuyPrice(i, discount)) + " Gold," + " ID: " + i);
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
            int j = 1;
            string y = canBuy;
            List<string> canBuyList = new List<string>();
            for (int i = 0; i < y.Length; i += 2)
            {
                canBuyList.Add(y.Substring(i, Math.Min(2, y.Length - i)));
            }
            foreach (string i in canBuyList)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class NPC
    {
        private QuestSystem qSystem;
        private Game game;
        private Trade trade;
        private Science science;
        private Quest quest;
        private Inventory inventory;
        private Player player;
        public void initObj(QuestSystem qSystem, Game game, Trade trade, Science science, Quest quest, Inventory inv, Player player)
        {
            this.qSystem = qSystem;
            this.game = game;
            this.trade = trade;
            this.science = science;
            this.quest = quest;
            this.inventory = inv;
            this.player = player;
            //if (game.dev)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Objekte Initalisiert[NPC's]. [NUR WÄHREND DES DEBUGS VISIBLE]");
            //    Console.ResetColor();
            //}
        }
        public sbyte state;
        string name;
        int npcID;
        int maxMessages;
        int canSell;
        int canBuy;
        string messages;
        public NPC(string npcName, int NPCID, int NPCmaxMessages, int npcCanSell, int npcCanBuy) 
        {
            name = npcName;
            npcID = NPCID;
            maxMessages = NPCmaxMessages;
            canSell = npcCanSell;
            canBuy = npcCanBuy;
            //Console.ForegroundColor = ConsoleColor.DarkCyan;
            //Console.WriteLine(npcName + ", " + npcID + ", " + maxMessages);
            //Console.ResetColor();
        }
        //For the NPC Hanna
        public void NPCStartHanna()
        {
            if(state == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 1; i <= 2; i++)
                {
                    Console.WriteLine(StringHandler.NPCMessages(name, npcID, i));
                    Console.ReadKey();
                    Console.WriteLine("");
                    state = 1;
                }
                Console.ResetColor();
            }
            if(state == 1)
            {
                while (true)
                {
                    Console.WriteLine("Welchen Debug Command willst du nutzen?");
                    Console.Write("Eingabe:");
                    messages = Console.ReadLine();
                    if(game.dev)
                    {
                        switch (messages)
                        {
                            default: 
                                Console.WriteLine("Nutz einer der Validen Debug Commands");
                                continue;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor= ConsoleColor.DarkRed;
                        Console.WriteLine("Dev Flag ist nicht auf True!");
                        Console.ResetColor();
                        break;
                    }
                }
            }
        }
        //For the NPC Lea
        public void NPCStartLea()
        {
            if (state == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 1; i <= 7; i++)
                {
                    Console.WriteLine(StringHandler.NPCMessages(name, npcID, i));
                    Console.ReadKey();
                    Console.WriteLine("");
                    state = 1;
                }
                Console.ResetColor();
                qSystem.QState[2] = 1;
            }
            if (game._save[10] == 1 && qSystem.QState[2] == 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 8; i <= 16; i++)
                {
                    Console.WriteLine(StringHandler.NPCMessages(name, npcID, i));
                    Console.ReadKey();
                    Console.WriteLine("");
                }
                Console.ResetColor();
                qSystem.QState[2] = 15;
            }
            if (state == 1)
            {
                float discount = 0;
                float extra = 0;
                if (qSystem.QState[2] == 15)
                {
                    discount = 0.80f;
                    extra = 0.90f;
                }
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(name +":\nHey Charlotte, willst du was Kaufen[1] oder Verkaufen[2]? Wenn du schon genug hast kannst du auch 'exit' nutzen.");
                    Console.ResetColor();
                    Console.Write("Eingabe:");
                    messages = Console.ReadLine();
                    switch (messages)
                    {
                        case "1":
                            while (true)
                            {
                                Console.WriteLine("Gib die ID an die du kaufen willst (Immer nur eine ID gleichzeitig)" + " Dein gold: " + player.gold +
                                    "\nDu kannst solange kaufen bist du 'exit' eingibtst");
                                trade.TradeInterfaceSell(canSell, discount);
                                Console.Write("Eingabe:");
                                messages = Console.ReadLine();
                                int x = 0;
                                int y = 0;
                                int z = 0;
                                switch (messages)
                                {
                                    case "01":
                                        Console.WriteLine("Wie viel Liter Benzin willst du kaufen?");
                                        Console.Write("Eingabe:");
                                        messages = Console.ReadLine();
                                        try
                                        {
                                            x = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            y = Convert.ToInt32(PriceList.BuyPrice("01", discount));
                                            x *= y;
                                            if(player.gold < x)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Gold!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold -= x;
                                            inventory.AddItem(1, z, false);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            continue;
                                        }
                                        break;
                                    case "02":
                                        Console.WriteLine("Wie viele Asteroiden stücke willst du kaufen?");
                                        Console.Write("Eingabe:");
                                        messages = Console.ReadLine();
                                        try
                                        {
                                            x = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            y = Convert.ToInt32(PriceList.BuyPrice("02", discount));
                                            x *= y;
                                            if (player.gold < x)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Gold!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold -= x;
                                            inventory.AddItem(2, z, false);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            continue;
                                        }
                                        break;
                                    case "03":
                                        Console.WriteLine("Wie viel Eisen Erz willst du kaufen?");
                                        Console.Write("Eingabe:");
                                        messages = Console.ReadLine();
                                        try
                                        {
                                            x = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            y = Convert.ToInt32(PriceList.BuyPrice("03", discount));
                                            x *= y;
                                            if (player.gold < x)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Gold!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold -= x;
                                            inventory.AddItem(3, z, false);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            continue;
                                        }
                                        break;
                                        case "04":
                                        Console.WriteLine("Wie viel Kupfer Erz willst du kaufen?");
                                        Console.Write("Eingabe:");
                                        messages = Console.ReadLine();
                                        try
                                        {
                                            x = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            y = Convert.ToInt32(PriceList.BuyPrice("04", discount));
                                            x *= y;
                                            if (player.gold < x)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Gold!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold -= x;
                                            inventory.AddItem(4, z, false);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            continue;
                                        }
                                        break;
                                        case "exit":
                                        goto Temp;
                                        default:
                                        Console.WriteLine("Gib eine Gültige ID ein!");
                                        continue;
                                }
                                continue;
                            }
                            Temp:
                            continue;
                        case "2":
                            while (true)
                            {
                                Console.WriteLine("Gib die ID an die du verkaufen willst (Immer nur eine ID gleichzeitig)" + " Dein gold: " + player.gold +
                                    "\nDu kannst solange kaufen bist du 'exit' eingibtst");
                                trade.TradeInterfaceBuy(canBuy, extra);
                                Console.Write("Eingabe:");
                                messages = Console.ReadLine();
                                int x = 0;
                                int y = 0;
                                int z = 0;
                                switch (messages)
                                {
                                    case "01":
                                        Console.WriteLine("Wie viel Liter Benzin willst du verkaufen?");
                                        Console.Write("Eingabe:");
                                        messages = Console.ReadLine();
                                        try
                                        {
                                            x = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            y = Convert.ToInt32(PriceList.SellPrice("01", extra));
                                            x *= y;
                                            if (inventory.itemIndex[1] < z)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Benzin!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold += x;
                                            inventory.RemoveItem(1, z, false);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            continue;
                                        }
                                        break;
                                    case "02":
                                        Console.WriteLine("Wie viele Asteroiden stücke willst du verkaufen?");
                                        Console.Write("Eingabe:");
                                        messages = Console.ReadLine();
                                        try
                                        {
                                            x = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            y = Convert.ToInt32(PriceList.SellPrice("02", extra));
                                            x *= y;
                                            if (inventory.itemIndex[2] < z)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Asteroiden stücke!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold += x;
                                            inventory.RemoveItem(2, z, false);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            continue;
                                        }
                                        break;
                                    case "03":
                                        Console.WriteLine("Wie viel Eisen Erz willst du verkaufen?");
                                        Console.Write("Eingabe:");
                                        messages = Console.ReadLine();
                                        try
                                        {
                                            x = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            y = Convert.ToInt32(PriceList.SellPrice("03", extra));
                                            x *= y;
                                            if (inventory.itemIndex[3] < z)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Eisen Erz!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold += x;
                                            inventory.RemoveItem(3, z, false);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            continue;
                                        }
                                        break;
                                    case "04":
                                        Console.WriteLine("Wie viel Kupfer Erz willst du verkaufen?");
                                        Console.Write("Eingabe:");
                                        messages = Console.ReadLine();
                                        try
                                        {
                                            x = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            y = Convert.ToInt32(PriceList.SellPrice("04", extra));
                                            x *= y;
                                            if (inventory.itemIndex[4] < z)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Kuper Erz!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold += x;
                                            inventory.RemoveItem(4, z, false);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            continue;
                                        }
                                        break;
                                    case "exit":
                                        goto Temp1;
                                    default:
                                        Console.WriteLine("Gib eine Gültige ID ein!");
                                        continue;
                                }
                                continue;
                            }
                            Temp1:
                            continue;
                        case "exit":
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(name + ":\nBis dann Charlotte. *Winkt*");
                            Console.ResetColor();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(name + ":\nTut mir leid, ich weiß aber nicht was du meinst.");
                            Console.ResetColor();
                            continue;
                    }
                    break;
                }
            }
        }
        //For the NPC...
    }
}

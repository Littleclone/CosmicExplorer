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
            if(name == "Hanna")
            {
                game.npcHanna = true;
            }
            else if(name == "Lea")
            {
                game.npcLea = true;
            }
        }
        public sbyte state;
        string name;
        int npcID;
        string canSell;
        string canBuy;
        string messages;
        string _x;
        public NPC(string npcName, int NPCID, string npcCanSell, string npcCanBuy) 
        {
            name = npcName;
            npcID = NPCID;
            canSell = npcCanSell;
            canBuy = npcCanBuy;
        }
        public void ResetVar()
        {
            state = 0;
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
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Eingabe:");
                    Console.ResetColor();
                    messages = Console.ReadLine().Trim().ToLower();
                    if(game.dev)
                    {
                        switch (messages)
                        {
                            case "item":
                                while (true)
                                {
                                    Console.WriteLine("Was willst du machen beim Inventar?");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Eingabe:");
                                    Console.ResetColor();
                                    messages = Console.ReadLine().Trim().ToLower();
                                    switch (messages)
                                    {
                                        case "give":
                                            while (true)
                                            {
                                                Console.WriteLine("Gib die ItemID an");
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.Write("Eingabe:");
                                                Console.ResetColor();
                                                messages = Console.ReadLine();
                                                try
                                                {
                                                    int x = Convert.ToInt32(messages);
                                                    Console.WriteLine("Gib die Item Anzahl an");
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    Console.Write("Eingabe:");
                                                    Console.ResetColor();
                                                    messages = Console.ReadLine();
                                                    int y = Convert.ToInt32(messages);
                                                    inventory.AddItem(x, y, false);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                    continue;
                                                }
                                                break;
                                            }
                                            continue;
                                        case "remove":
                                            while (true)
                                            {
                                                Console.WriteLine("Gib die ItemID an");
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.Write("Eingabe:");
                                                Console.ResetColor();
                                                messages = Console.ReadLine();
                                                try
                                                {
                                                    int x = Convert.ToInt32(messages);
                                                    Console.WriteLine("Gib die Item Anzahl an");
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    Console.Write("Eingabe:");
                                                    Console.ResetColor();
                                                    messages = Console.ReadLine();
                                                    int y = Convert.ToInt32(messages);
                                                    inventory.RemoveItem(x, y, false);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                    continue;
                                                }
                                                break;
                                            }
                                            continue;
                                        case "exit":
                                            break;
                                        default:
                                            Console.WriteLine("Nutz einer der Validen Debug Commands");
                                            continue;
                                    }
                                    break;
                                }
                                continue;
                            case "gold":
                                while (true)
                                {
                                    Console.WriteLine("Gib die anzahl an Gold an");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Eingabe:");
                                    Console.ResetColor();
                                    messages = Console.ReadLine();
                                    try
                                    {
                                        int x = Convert.ToInt32(messages);
                                        if(x < 0)
                                        {
                                            player.RemoveGold(x);
                                        }
                                        else
                                        {
                                            player.AddGold(x);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        continue;
                                    }
                                    break;
                                }
                                continue;
                            case "quest":
                                while (true)
                                {
                                    Console.WriteLine("Gib die QuestID an");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Eingabe:");
                                    Console.ResetColor();
                                    messages = Console.ReadLine();
                                    try
                                    {
                                        int x = Convert.ToInt32(messages);
                                        Console.WriteLine("Gib den State der Quest an");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        messages = Console.ReadLine();
                                        SByte y = Convert.ToSByte(messages);
                                        qSystem.QState[x] = y;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        continue;
                                    }
                                    break;
                                }
                                continue;
                            case "science":
                                while (true)
                                {
                                    Console.WriteLine("Gib die ForschungsID an");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Eingabe:");
                                    Console.ResetColor();
                                    messages = Console.ReadLine();
                                    try
                                    {
                                        int x = Convert.ToInt32(messages);
                                        Console.WriteLine("Gib den progress der Forschung an");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        messages = Console.ReadLine();
                                        SByte y = Convert.ToSByte(messages);
                                        science.progress[x] = y;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        continue;
                                    }
                                    break;
                                }
                                continue;
                            case "_save":
                                while (true)
                                {
                                    Console.WriteLine("Gib den _saveIndex an");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Eingabe:");
                                    Console.ResetColor();
                                    messages = Console.ReadLine();
                                    try
                                    {
                                        int x = Convert.ToInt32(messages);
                                        Console.WriteLine("Gib den State des _save's an");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        messages = Console.ReadLine();
                                        SByte y = Convert.ToSByte(messages);
                                        game._save[x] = y;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        continue;
                                    }
                                    break;
                                }
                                continue;
                            case "exit":
                                break;
                            default: 
                                Console.WriteLine("Nutz einer der Validen Debug Commands");
                                continue;
                        }
                        break;
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
        public void NPCStartGFI()
        {
            if (state == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 1; i <= 9; i++)
                {
                    Console.WriteLine(StringHandler.NPCMessages(name, npcID, i));
                    Console.ReadKey();
                    Console.WriteLine("");
                    state = 1;
                }
                Console.ResetColor();
                qSystem.QState[1] = 1;
                return;
            }
            if (inventory.itemIndex[1] == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("GFI : \nWas Gibts Charlotte?");
                Console.WriteLine("");
                Console.ReadKey();
                Console.WriteLine("Charlotte : \nIch habe kein Treibstoff mehr und brauche neues.");
                Console.WriteLine("");
                Console.ReadKey();
                Console.WriteLine("GFI : \n*Seufzt* Okay, wir schicken dir neues.");
                Console.WriteLine("");
                Console.ReadKey();
                inventory.AddItem(1, 1000, false);
                Console.WriteLine("Du hast 1000 Treibstoff erhalten [Achte darauf Händler wie 'Lea' zu finden um Treibstoff zu kaufen]");
                Console.WriteLine("Hinweis: Dies ist nur derzeit um gegen Softlock anzukommen, im Fertigem Spiel wird es dies nicht geben.");
                Console.WriteLine("");
                Console.ReadKey();
                Console.ResetColor();
                return;
            }
            if (state == 1 && qSystem.QState[1] == 4)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 10; i <= 14; i++)
                {
                    Console.WriteLine(StringHandler.NPCMessages(name, npcID, i));
                    Console.ReadKey();
                    Console.WriteLine("");
                }
                Console.ResetColor();
                state = 2;
                qSystem.QState[1] = 15;
                return;
            }
            if (state == 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 15; i <= 18; i++)
                {
                    Console.WriteLine(StringHandler.NPCMessages(name, npcID, i));
                    Console.ReadKey();
                    Console.WriteLine("");
                }
                Console.ResetColor();
                state = 3;
                qSystem.QState[3] = 1;
                return;
            }
            if (state > 0)
            {
                ulong x = 30;
                x -= game.DayCounter;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("GFI ist gerade nicht Erreichbar, versuch es Später nochmal wenn du mit der Quest Weiter bist.");
                if(x > 1)
                {
                    Console.WriteLine($"Es sind noch {x} Tage bis zu deinem Nächsten Gehalt.");
                }
                else if(x == 1)
                {
                    Console.WriteLine("1 Tag noch bis zu deinem Nächstem Gehalt.");
                }
                else
                {
                    Console.WriteLine("Du hast heute dein Gehalt bekommen.");
                }
                Console.ResetColor();
                Console.WriteLine("");
                Console.ReadKey();
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
            if (qSystem.QState[2] == 2)
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
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Eingabe:");
                    Console.ResetColor();
                    messages = Console.ReadLine();
                    switch (messages)
                    {
                        case "1":
                            while (true)
                            {
                                Console.WriteLine("Gib die ID an die du kaufen willst (Immer nur eine ID gleichzeitig)" + " Dein gold: " + player.gold +
                                    "\nDu kannst solange kaufen bist du 'exit' eingibst\n");
                                trade.TradeInterfaceSell(canSell, discount);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Eingabe:");
                                Console.ResetColor();
                                messages = Console.ReadLine();
                                if(messages == "exit")
                                {
                                    break;
                                }
                                int x = 0;
                                int y = 0;
                                int z = 0;
                                int w = 0;
                                try
                                {
                                    x = Convert.ToInt32(messages);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Falsches Format!");
                                    continue;
                                }
                                if (x == 1 || x == 2 || x == 3 || x == 4 || x == 5 || x == 6 || x == 7 || x == 8)
                                {
                                    while (true)
                                    {
                                        TradeString(x, false, false);
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        messages = Console.ReadLine();
                                        if (messages == "exit")
                                        {
                                            break;
                                        }
                                        try
                                        {
                                            y = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            w = Convert.ToInt32(PriceList.BuyPrice(_x, discount));
                                            y *= w;
                                            if (player.gold < y)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Du hast nicht genügend Gold!");
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold -= y;
                                            inventory.AddItem(x, z, false);
                                            break;
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Das wird hier nicht verkauft");
                                    break;
                                }

                                continue;
                            }
                            continue;
                        case "2":
                            while (true)
                            {
                                Console.WriteLine("Gib die ID an die du verkaufen willst (Immer nur eine ID gleichzeitig)" + " Dein gold: " + player.gold +
                                    "\nDu kannst solange kaufen bist du 'exit' eingibst\n");
                                trade.TradeInterfaceBuy(canBuy, extra);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Eingabe:");
                                Console.ResetColor();
                                messages = Console.ReadLine();
                                int x = 0;
                                int y = 0;
                                int z = 0;
                                int w = 0;
                                try
                                {
                                    x = Convert.ToInt32(messages);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Falsches Format!");
                                    break;
                                }
                                if (x == 1 || x == 2 || x == 3 || x == 4 || x == 5 || x == 6 || x == 7 || x == 8)
                                {
                                    while (true)
                                    {
                                        TradeString(x, true, false);
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        messages = Console.ReadLine();
                                        if (messages == "exit")
                                        {
                                            break;
                                        }
                                        try
                                        {
                                            y = Convert.ToInt32(messages);
                                            z = Convert.ToInt32(messages);
                                            w = Convert.ToInt32(PriceList.SellPrice(_x, discount));
                                            y *= w;
                                            if (inventory.itemIndex[x] < z)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                TradeString(x, true, true);
                                                Console.ResetColor();
                                                break;
                                            }
                                            player.gold += y;
                                            inventory.RemoveItem(x, z, false);
                                            break;
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Falsches Format!");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Das wird hier nicht gekauft.");
                                }

                                continue;
                            }
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
        public void NPCStartSupplier()
        {
            if (state == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine(StringHandler.NPCMessages(name, npcID, i));
                    Console.ReadKey();
                    Console.WriteLine("");
                    state = 1;
                }
                Console.ResetColor();
                qSystem.QState[1] = 3;
            }
            if (state == 1 && qSystem.QState[1] == 4)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 11; i <= 20; i++)
                {
                    Console.WriteLine(StringHandler.NPCMessages(name, npcID, i));
                    Console.ReadKey();
                    Console.WriteLine("");
                    state = 2;
                }
                Console.ResetColor();
                qSystem.QState[1] = 15;
            }
        }
        public void TradeString(int x, bool isBuy, bool notEnough)
        {
            if (notEnough)
            {
                Console.WriteLine("Du hast nicht genügend " + ItemIndex.ItemName(x) + "!");
                return;
            }
            //Kaufen
            if (x == 1 && !isBuy)
            {
                Console.WriteLine("Wie viel Liter Treibstoff willst du kaufen?");
                _x = "01";
                return;
            }
            //Asteroiden Stücke, Eisenbarren Kupfer barren
            if(x == 2 || x == 6 || x == 7 && !isBuy)
            {
                Console.WriteLine("Wie viele "+ ItemIndex.ItemName(x) + " stücke willst du kaufen?");
                if (x == 2)
                {
                    _x = "02";
                }
                else if (x == 6)
                {
                    _x = "06";
                }
                else if (x == 7)
                {
                    _x = "07";
                }
                return;
            }
            //Eisen Erz, Kupfer Erz, Asteroiden Staub, Kohle
            if(x == 3 || x == 4 || x == 5 || x == 8 && !isBuy)
            {
                Console.WriteLine("Wie viel "+ ItemIndex.ItemName(x) + " willst du kaufen?");
                if (x == 3)
                {
                    _x = "03";
                }
                else if (x == 4)
                {
                    _x = "04";
                }
                else if (x == 5)
                {
                    _x = "05";
                }
                else if (x == 8)
                {
                    _x = "08";
                }
                return;
            }
            //Verkaufen
            if (x == 1 && isBuy)
            {
                Console.WriteLine("Wie viel Liter Treibstoff willst du verkaufen?");
                _x = "01";
                return;
            }
            //Asteroiden Stücke, Eisenbarren Kupfer barren
            if (x == 2 || x == 6 || x == 7 && isBuy)
            {
                Console.WriteLine("Wie viele " + ItemIndex.ItemName(x) + " stücke willst du verkaufen?");
                if (x == 2)
                {
                    _x = "02";
                }
                else if (x == 6)
                {
                    _x = "06";
                }
                else if (x == 7)
                {
                    _x = "07";
                }
                return;
            }
            //Eisen Erz, Kupfer Erz, Asteroiden Staub, Kohle
            if (x == 3 || x == 4 || x == 5 || x == 8 && isBuy)
            {
                Console.WriteLine("Wie viel " + ItemIndex.ItemName(x) + " willst du verkaufen?");
                if(x == 3)
                {
                    _x = "03";
                }
                else if(x == 4)
                {
                    _x = "04";
                }
                else if (x == 5)
                {
                    _x = "05";
                }
                else if (x == 8)
                {
                    _x = "08";
                }
                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class Inventory
    {
        private World world;
        private Game game;
        private SpaceShuttle shuttle;
        private Space space;
        private Activities actions;
        private PassivSystem passiv;
        private Science science;
        private QuestSystem quest;
        public void InvInit(SpaceShuttle shuttle, Space space, Activities action, Game games, World world, PassivSystem systems, Science science, QuestSystem quest)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.space = space;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            this.science = science;
            this.quest = quest;
            game.inv = true;
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Inventory]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public void StorageRoom()
        {
            while (true)
            {
                string message = "error";
                game.NewDayStart(shuttle.currentTime);
                shuttle.currentRoom = 4;
                Console.WriteLine("Du bist im Lagerraum, was willst du machen? [Uhrzeit: " + shuttle.currentTime + ":00]");
                Console.WriteLine("Schau dir an was du im Lager hast.[1]");
                Console.WriteLine("Ins Schlafzimmer gehen.[2]");
                Console.WriteLine("In die Kommando Zentrale gehen.[3]");
                Console.WriteLine("Zur Luftschleuse gehen.[4]");
                Console.WriteLine("In die Küche gehen.[5] X");
                Console.WriteLine("Zum Generator gehen.[6] X");
                Console.WriteLine("Ins Labor gehen.[7]");
                Console.WriteLine("In die Werkstatt gehen.[8]");
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Eingabe:");
                    Console.ResetColor();
                    message = Console.ReadLine().Trim();
                    switch (message)
                    {
                        case "1":
                            shuttle.currentTime++;
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.White;
                            PlayerInventory();
                            Console.ResetColor();
                            Console.WriteLine("");
                            break;
                        case "2":
                            shuttle.currentTime++;
                            shuttle.Bedroom(shuttle.currentTime);
                            break;
                        case "3":
                            shuttle.currentTime++;
                            shuttle.Commandcenter();
                            break;
                        case "4":
                            shuttle.currentTime++;
                            shuttle.Airlock();
                            break;
                        case "5":
                            Console.WriteLine("Noch nicht Implementiert!");
                            continue;
                        case "6":
                            Console.WriteLine("Noch nicht Implementiert!");
                            continue;
                        case "7":
                            science.Laboratory();
                            break;
                        case "8":
                            shuttle.currentTime++;
                            WorkshopRoom();
                            break;
                        default:
                            Console.WriteLine("Wähle einer der Nummern aus!");
                            continue;
                    }
                    break;
                }
            }
        }
        public void WorkshopRoom()
        {
            while (true)
            {
                string message = "error";
                game.NewDayStart(shuttle.currentTime);
                shuttle.currentRoom = 4;
                Console.WriteLine("Du bist bei der Werkstatt, was willst du machen? [Uhrzeit: " + shuttle.currentTime + ":00]");
                Console.WriteLine("Geh in die Werkstatt.[1]");
                Console.WriteLine("Ins Schlafzimmer gehen.[2]");
                Console.WriteLine("In die Kommando Zentrale gehen.[3]");
                Console.WriteLine("Zum Lagerraum gehen.[4]");
                Console.WriteLine("Zur Luftschleuse gehen.[5]");
                Console.WriteLine("In die Küche gehen.[6] X");
                Console.WriteLine("Zum Generator gehen.[7] X");
                Console.WriteLine("Ins Labor gehen.[8]");
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Eingabe:");
                    Console.ResetColor();
                    message = Console.ReadLine().Trim();
                    switch (message)
                    {
                        case "1":
                            WorkshopSpace();
                            break;
                        case "2":
                            shuttle.currentTime++;
                            shuttle.Bedroom(shuttle.currentTime);
                            break;
                        case "3":
                            shuttle.currentTime++;
                            shuttle.Commandcenter();
                            break;
                        case "4":
                            shuttle.currentTime++;
                            StorageRoom();
                            break;
                        case "5":
                            shuttle.currentTime++;
                            shuttle.Airlock();
                            break;
                        case "6":
                            shuttle.currentTime++;
                            shuttle.Airlock();
                            break;
                        case "7":
                            Console.WriteLine("Noch nicht Implementiert!");
                            continue;
                        case "8":
                            shuttle.currentTime++;
                            science.Laboratory();
                            break;
                        default:
                            Console.WriteLine("Wähle einer der Nummern aus!");
                            continue;
                    }
                    break;
                }
            }
        }
        public void WorkshopSpace()
        {
            while (true)
            {
                string message = "error";
                game.NewDayStart(shuttle.currentTime);
                shuttle.currentRoom = 4;
                Console.WriteLine("Du bist in der Werkstatt, was willst du machen? [Uhrzeit: " + shuttle.currentTime + ":00]");
                Console.WriteLine("Geh an die Schmelze.[1]");
                Console.WriteLine("Geh zurück.[2]");
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Eingabe:");
                    Console.ResetColor();
                    message = Console.ReadLine().Trim();
                    switch (message)
                    {
                        case "1":
                            Console.WriteLine("Welches Item wills du schmelzen? Restliche Kohle: " + itemIndex[8]);
                            if (science.progress[2] == 3)
                            {
                                Console.WriteLine(ItemIndex.ItemName(2) + ": " + itemIndex[2] + ", ID: 02");
                            }
                            Console.WriteLine(ItemIndex.ItemName(3) + ": " + itemIndex[3] + ", ID: 03");
                            Console.WriteLine(ItemIndex.ItemName(4) + ": " + itemIndex[4] + ", ID: 04");
                            Console.WriteLine(ItemIndex.ItemName(9) + ": " + itemIndex[9] + ", ID: 09");
                            if (quest.QState[3] == 3)
                            {
                                Console.WriteLine(ItemIndex.ItemName(13) + ": " + itemIndex[13] + ", ID: 13");
                            }
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Eingabe:");
                                Console.ResetColor();
                                message = Console.ReadLine().Trim();
                                if (message == "02" && game._save[11] == 1)
                                {
                                    bool isFinishedAsteroidPiece = false;
                                    while (!isFinishedAsteroidPiece)
                                    {
                                        Console.WriteLine("Wieviele Erze möchtest du herstellen? (Es werden immer 20 Asteroiden stücke benötigt)");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        try
                                        {
                                            message = Console.ReadLine().Trim().ToLower();
                                            if (message == "exit")
                                            {
                                                break;
                                            }
                                            int x = Convert.ToInt32(message);
                                            int y = x;
                                            x *= 20;
                                            if (itemIndex[2] >= x)
                                            {
                                                if (itemIndex[8] >= y)
                                                {
                                                    Random random = new Random();
                                                    AddItem(3, random.Next(0, 3), true);
                                                    AddItem(4, random.Next(0, 5), true);
                                                    AddItem(9, random.Next(0, 2), true);
                                                    RemoveItem(2, x, false);
                                                    RemoveItem(8, y, false);
                                                    Console.WriteLine("Du hast erhalten:");
                                                    Console.WriteLine("Gold Erz: " + gold_ore);
                                                    Console.WriteLine("Eisen Erz: " + iron_ore);
                                                    Console.WriteLine("Kupfer Erz: " + copper_ore);
                                                    Console.WriteLine("Du hast Verbraucht:\n" + ItemIndex.ItemName(2) + " " + x + "\n" + ItemIndex.ItemName(8) + " " + y);
                                                    isFinishedAsteroidPiece = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nicht genügend Kohle!");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Nicht genügend Asteroiden Stücke!");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Versuch es nochmal!");
                                        }
                                    }
                                    break;
                                }
                                else if (message == "03")
                                {
                                    bool isFinishedIron = false;
                                    while (!isFinishedIron)
                                    {
                                        Console.WriteLine("Wieviele Eisenbarren möchtest du herstellen? (1 Eisenbarren braucht 5 Eisen Erz und 1 Kohle)");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        try
                                        {
                                            message = Console.ReadLine().Trim().ToLower();
                                            if (message == "exit")
                                            {
                                                break;
                                            }
                                            int x = Convert.ToInt32(message);
                                            int y = x; // Die Kohle Menge
                                            x *= 5; // Die Menge an Eisenerz die benötigt wird
                                            if (itemIndex[3] >= x)
                                            {
                                                if (itemIndex[8] >= y)
                                                {
                                                    RemoveItem(3, x, false);
                                                    RemoveItem(8, y, false);
                                                    Console.WriteLine("Du hast " + y + " " + ItemIndex.ItemName(6) + " hergestellt!");
                                                    Console.WriteLine("Du hast Verbraucht:\n" + ItemIndex.ItemName(3) + " " + x + "\n" + ItemIndex.ItemName(8) + " " + y);
                                                    AddItem(6, y, false);
                                                    isFinishedIron = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nicht genügend Kohle!");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Nicht genügend Eisen Erz!");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Versuch es nochmal!");
                                        }
                                    }
                                    break;
                                }
                                else if(message == "04")
                                {
                                    bool isFinishedCopper = false;
                                    while (!isFinishedCopper)
                                    {
                                        Console.WriteLine("Wieviele Kupferbarren möchtest du herstellen? (1 Kupferbarren braucht 5 Kupfer Erz und 1 Kohle)");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        try
                                        {
                                            message = Console.ReadLine().Trim().ToLower();
                                            if (message == "exit")
                                            {
                                                break;
                                            }
                                            int x = Convert.ToInt32(message);
                                            int y = x;
                                            x *= 5;
                                            if (itemIndex[4] >= x)
                                            {
                                                if (itemIndex[8] >= y)
                                                {
                                                    RemoveItem(4, x, false);
                                                    RemoveItem(8, y, false);
                                                    Console.WriteLine("Du hast " + y + " " + ItemIndex.ItemName(7) + " hergestellt!");
                                                    Console.WriteLine("Du hast Verbraucht:\n" + ItemIndex.ItemName(4) + " " + x + "\n" + ItemIndex.ItemName(8) + " " + y);
                                                    AddItem(7, y, false);
                                                    isFinishedCopper = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nicht genügend Kohle!");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Nicht genügend Kupfer Erz!");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Versuch es nochmal!");
                                        }
                                    }
                                    break;

                                }
                                else if (message == "09")
                                {
                                    bool isFinishedGold = false;
                                    while (!isFinishedGold)
                                    {
                                        Console.WriteLine("Wieviele Goldbarren möchtest du herstellen? (1 Goldbarren braucht 5 Gold Erz und 1 Kohle)");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        try
                                        {
                                            message = Console.ReadLine().Trim().ToLower();
                                            if (message == "exit")
                                            {
                                                break;
                                            }
                                            int x = Convert.ToInt32(message);
                                            int y = x;
                                            x *= 5;
                                            if (itemIndex[9] >= x)
                                            {
                                                if (itemIndex[8] >= y)
                                                {
                                                    RemoveItem(9, x, false);
                                                    RemoveItem(8, y, false);
                                                    Console.WriteLine("Du hast " + y + " " + ItemIndex.ItemName(10) + " hergestellt!");
                                                    Console.WriteLine("Du hast Verbraucht:\n" + ItemIndex.ItemName(9) + " " + x + "\n" + ItemIndex.ItemName(8) + " " + y);
                                                    AddItem(10, y, false);
                                                    isFinishedGold = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nicht genügend Kohle!");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Nicht genügend Gold Erz!");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Versuch es nochmal!");
                                        }
                                    }
                                    break;
                                }
                                else if (message == "13" && quest.QState[3] == 3)
                                {
                                    bool isFinishedLegierung = false;
                                    while (!isFinishedLegierung)
                                    {
                                        Console.WriteLine("Wieviele Experiemente Legierungen möchtest du herstellen?\n(Eine Experiementelle Legierung Braucht: 1 Eisenbarren, 3 Kupferbarren und 1 Kohle)");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Eingabe:");
                                        Console.ResetColor();
                                        try
                                        {
                                            message = Console.ReadLine().Trim().ToLower();
                                            if (message == "exit")
                                            {
                                                break;
                                            }
                                            int x = Convert.ToInt32(message);
                                            int y = x; // Die Kohle Menge die benötigt wird
                                            int z = x; // Die Menge an Eisenbarren die benötigt wird
                                            x *= 3; // Die Menge an Kupferbarren die benötigt wird
                                            if (itemIndex[7] >= x)
                                            {
                                                if (itemIndex[6] >= z)
                                                {
                                                    if (itemIndex[8] >= y)
                                                    {
                                                        RemoveItem(7, x, false);
                                                        RemoveItem(6, z, false);
                                                        RemoveItem(8, y, false);
                                                        Console.WriteLine("Du hast " + y + " " + ItemIndex.ItemName(13) + " hergestellt!");
                                                        Console.WriteLine("Du hast Verbraucht:\n" + ItemIndex.ItemName(7) + " " + x + "\n" + ItemIndex.ItemName(6) + " " + z + "\n" + ItemIndex.ItemName(8) + " " + y);
                                                        AddItem(13, y, false);
                                                        isFinishedLegierung = true;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Nicht genügend Kohle!");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nicht genügend Eisenbarren!");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Nicht genügend Kupferbarren!");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Versuch es nochmal!");
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wähle eine Valide ID!");
                                    continue;
                                }
                            }
                            break;
                        case "2":
                            WorkshopRoom();
                            break;
                        default:
                            Console.WriteLine("Wähle einer der Nummern aus!");
                            continue;
                    }
                    break;
                }
            }
        }
        public int[] itemIndex = new int[100]; //array to save the Items
        //Need a rework in the future!!!
        public int petrol = 0;
        public int asteroid_pieces = 0;
        public int asteroid_dust = 0;
        public int iron_ore = 0;
        public int iron_ingot = 0;
        public int copper_ore = 0;
        public int copper_ingot = 0;
        public int coal = 0;
        public int gold_ore = 0;
        public int gold_ingot = 0;
        public void PlayerInventory()
        {
            for (int i = 1; i <= 10; i++)
            {
                if(ItemIndex.IsValid(i))
                {
                    if (itemIndex[i] != 0)
                    {
                        if(i < 10)
                        {
                            Console.WriteLine(ItemIndex.ItemName(i) + ": " + itemIndex[i] + " ID:0" + i);
                            continue;
                        }
                        Console.WriteLine(ItemIndex.ItemName(i) + ": " + itemIndex[i] + " ID:" + i);
                    }
                }
            }
        }
        public void AddItem(int ItemID, int amount, bool count)
        {
            if(ItemIndex.IsValid(ItemID))
            {
                if(!count)
                {
                    petrol = 0;
                    asteroid_pieces = 0;
                    asteroid_dust = 0;
                    iron_ore = 0;
                    iron_ingot = 0;
                    copper_ore = 0;
                    copper_ingot = 0;
                    coal = 0;
                    gold_ore = 0;
                    gold_ingot = 0;
                }
                if (ItemID == 1)
                {
                    itemIndex[1] += amount;
                    petrol += amount;
                }
                if(ItemID == 2)
                {
                    itemIndex[2] += amount;
                    asteroid_pieces += amount;
                }
                if(ItemID == 3)
                {
                    itemIndex[3] += amount;
                    iron_ore += amount;
                }
                if(ItemID == 4)
                {
                    itemIndex[4] += amount;
                    copper_ore += amount;
                }
                if(ItemID == 5)
                {
                    itemIndex[5] += amount;
                    asteroid_dust += amount;
                }
                if(ItemID == 6)
                {
                    itemIndex[6] += amount;
                    iron_ingot += amount;
                }
                if(ItemID == 7)
                {
                    itemIndex[7] += amount;
                    copper_ingot += amount;
                }
                if(ItemID == 8)
                {
                    itemIndex[8] += amount;
                    coal += amount;
                }
                if(ItemID == 9)
                {
                    itemIndex[9] += amount;
                    gold_ore += amount;
                }
                if(ItemID == 10)
                {
                    itemIndex[10] += amount;
                    gold_ingot += amount;
                }
                if(ItemID == 11)
                {
                    itemIndex[11] += amount;
                }
                if (ItemID == 12)
                {
                    itemIndex[12] += amount;
                }
                if (ItemID == 13)
                {
                    itemIndex[13] += amount;
                }
            }
        }
        public void RemoveItem(int ItemID, int amount, bool count)
        {
            if (ItemIndex.IsValid(ItemID))
            {
                if(!count)
                {
                    petrol = 0;
                    asteroid_pieces = 0;
                    asteroid_dust = 0;
                    iron_ore = 0;
                    iron_ingot = 0;
                    copper_ore = 0;
                    copper_ingot = 0;
                    coal = 0;
                    gold_ore = 0;
                    gold_ingot = 0;
                }
                if (ItemID == 1)
                {
                    itemIndex[1] -= amount;
                    petrol -= amount;
                }
                if (ItemID == 2)
                {
                    itemIndex[2] -= amount;
                    asteroid_pieces -= amount;
                }
                if(ItemID == 3)
                {
                    itemIndex[3] -= amount;
                    iron_ore -= amount;
                }
                if(ItemID == 4)
                {
                    itemIndex[4] -= amount;
                    copper_ore -= amount;
                }
                if (ItemID == 5)
                {
                    itemIndex[5] -= amount;
                    asteroid_dust -= amount;
                }
                if (ItemID == 6)
                {
                    itemIndex[6] -= amount;
                    iron_ingot -= amount;
                }
                if (ItemID == 7)
                {
                    itemIndex[7] -= amount;
                    copper_ingot -= amount;
                }
                if (ItemID == 8)
                {
                    itemIndex[8] -= amount;
                    coal -= amount;
                }
                if (ItemID == 9)
                {
                    itemIndex[9] -= amount;
                    gold_ore -= amount;
                }
                if (ItemID == 10)
                {
                    itemIndex[10] -= amount;
                    gold_ingot -= amount;
                }
                if (ItemID == 11)
                {
                    itemIndex[11] -= amount;
                }
                if (ItemID == 12)
                {
                    itemIndex[12] -= amount;
                }
                if (ItemID == 13)
                {
                    itemIndex[13] -= amount;
                }
            }
        }
        public void ResetVar()
        {
            //Setze alle variablen zurück und leere das itemindex array
            petrol = 0;
            asteroid_pieces = 0;
            asteroid_dust = 0;
            iron_ore = 0;
            iron_ingot = 0;
            copper_ore = 0;
            copper_ingot = 0;
            coal = 0;
            gold_ore = 0;
            gold_ingot = 0;
            Array.Fill(itemIndex, 0);

        }
    }
}
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
        public void InvInit(SpaceShuttle shuttle, Space space, Activities action, Game games, World world, PassivSystem systems, Science science)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.space = space;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            this.science = science;
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
                Console.WriteLine("In die Küche gehen.[5]");
                Console.WriteLine("Zum Generator gehen.[6]");
                Console.WriteLine("Ins Labor gehen.[7]");
                while (true)
                {
                    Console.Write("Eingabe:");
                    message = Console.ReadLine();
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
                        default:
                            Console.WriteLine("Wähle einer der Nummern aus!");
                            continue;
                    }
                    break;
                }
            }
        }
        public int[] itemIndex = new int[10]; //array to save the Items
        public int petrol = 0;
        public int asteroid_pieces = 0;
        public int iron_ore = 0;
        public int copper_ore = 0;
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
                    iron_ore = 0;
                    copper_ore = 0;
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
                    iron_ore = 0;
                    copper_ore = 0;
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
            }
        }
    }
}
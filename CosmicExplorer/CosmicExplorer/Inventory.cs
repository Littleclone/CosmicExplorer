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
        public void InvInit(SpaceShuttle shuttle, Space space, Activities action, Game games, World world, PassivSystem systems)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.space = space;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Inventory]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public void StorageRoom()
        {
            int currentTime;
            string message = "error";
            currentTime = shuttle.currentTime;
            game.NewDayStart(currentTime);
            shuttle.currentRoom = 4;
            Console.WriteLine("Du bist im Lagerraum, was willst du machen? [Uhrzeit: " + currentTime + ":00]");
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
                        currentTime++;
                        PlayerInventory();
                        continue;
                    case "2":
                        currentTime++;
                        shuttle.Bedroom(currentTime);
                        break;
                    case "3":
                        currentTime++;
                        shuttle.currentTime = currentTime;
                        shuttle.Commandcenter();
                        break;
                    case "4":
                        currentTime++;
                        shuttle.currentTime = currentTime;
                        shuttle.Airlock();
                        break;
                    case "5":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "6":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "7":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
            }
        }
        public int[] itemIndex = new int[5]; //array to save the Items
        public void PlayerInventory()
        {
            for (int i = 1; i <= 4; i++)
            {
                if(ItemIndex.IsValid(i))
                {
                    if (itemIndex[i] != 0)
                    {
                        Console.WriteLine(ItemIndex.ItemName(i) + ": " + itemIndex[i] + " ID:" + i);
                    }
                }
            }
        }
        public void AddItem(int ItemID, int amount)
        {
            if(ItemIndex.IsValid(ItemID))
            {
                if (ItemID == 1)
                {
                    itemIndex[1] += amount;
                }
            }
        }
        public void RemoveItem(int ItemID, int amount)
        {
            if (ItemIndex.IsValid(ItemID))
            {
                if (ItemID == 1)
                {
                    itemIndex[1] -= amount;
                }
            }
        }
    }
}
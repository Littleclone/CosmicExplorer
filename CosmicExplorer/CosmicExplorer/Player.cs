using CosmicExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class Player
    {
        public int health = 100;
        public int hunger = 100;
        public int thirst = 100;
        public int oxygen = 100;
        public long gold = 1000;
        public bool isDeath = false;
        Random random = new Random();
        private Game game;
        public void ObjInit(Game game)
        {
            this.game = game;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Player]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public void AddGold(long amount)
        {
            gold += amount;
        }
        public void RemoveGold(long amount)
        {
            gold -= amount;
        }
        public void EasyActions()
        {
            hunger -= random.Next(0, 5);
            thirst -= random.Next(1, 8);
            oxygen -= random.Next(3, 5);
            OxygenCalculations();
        }
        public void MediumActions()
        {
            {
                hunger -= random.Next(8, 15);
                thirst -= random.Next(10, 16);
                oxygen -= random.Next(7, 11);
                OxygenCalculations();
            }
        }
        public void HardActions()
        {
            {
                hunger -= random.Next(17, 23);
                thirst -= random.Next(18, 24);
                oxygen -= random.Next(12, 17);
                OxygenCalculations();
            }
        }
        public void OxygenCalculations()
        {
            if(oxygen > 40)
            {
                health -= random.Next(2, 7);
            }
            if(health <= 0)
            {
                string message = "error";
                isDeath = true;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Du bist gestorben!");
                if(!game.hardCore)
                {
                    Console.WriteLine("Was willst du machen? [exit = Geh zum Hauptmenü] [load = Lade dein Letzten Tag]");
                    Console.ResetColor();
                    Console.Write("Eingabe:");
                    message = Console.ReadLine();
                    switch (message)
                    {
                        case "exit":
                            game.Start();
                            break;
                        case "load":
                            game.LoadSaveFile();
                            game.NewDayStart(23);
                            break;
                        default:
                            break;
                    }
                }
                //Passiert wenn du den Hardcore mode aktiviert hast
                else
                {
                    Console.WriteLine("Deine SaveFiles werden gelöscht. Du musst neu anfangen!");
                    Console.ResetColor();
                    DataManager.DeleteSaveFiles();
                    game.Start();
                }
            }
        }
    }
}

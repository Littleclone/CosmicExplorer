using CosmicExplorer;
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
    public class Player
    {
        public int health = 100; // Die Player Leben
        public int hunger = 100; // Der Player Hunger
        public int thirst = 100; // Der Player Durst
        public int oxygen = 100; // Der Player Sauerstoff
        public long gold = 1000; // Das Player Gold
        public bool isDeath = false; // Ist der Player Tod?
        Random random = new Random();
        private Game game;
        public void ObjInit(Game game)
        {
            this.game = game;
            game.mainPlayer = true;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Player]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public void ResetVar()
        {
            health = 100;
            hunger = 100;
            thirst = 100;
            oxygen = 100;
            gold = 1000;
            isDeath = false;
        }
        public void AddGold(long amount)
        {
            gold += amount;
        }
        public void RemoveGold(long amount)
        {
            gold -= amount;
        }
        public void EasyActions() // Für alle Aktionen die nur leichte Körperliche Aktivität haben
        {
            hunger -= random.Next(0, 5);
            thirst -= random.Next(1, 8);
            oxygen -= random.Next(3, 5);
            OxygenCalculations();
        }
        public void MediumActions() // Für alle Aktionen die mittlere Körperliche Aktivität haben
        {
            {
                hunger -= random.Next(8, 15);
                thirst -= random.Next(10, 16);
                oxygen -= random.Next(7, 11);
                OxygenCalculations();
            }
        }
        public void HardActions() // Für alle Aktionen die schwere Körperliche Aktivität haben
        {
            {
                hunger -= random.Next(17, 23);
                thirst -= random.Next(18, 24);
                oxygen -= random.Next(12, 17);
                OxygenCalculations();
            }
        }
        public void OxygenCalculations() // Berechnet anhand des Sauerstoffs den Lebensverlust und den Tod
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
                    while (true)
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
                                continue;
                        }
                        break;
                    }
                }
                //Passiert wenn du den Hardcore mode aktiviert hast
                else
                {
                    Console.WriteLine("Deine SaveFiles werden gelöscht. Du musst neu anfangen! [Hardcore Mode]");
                    Console.ResetColor();
                    DataManager.DeleteSaveFiles();
                    game.Start();
                }
            }
        }
    }
}

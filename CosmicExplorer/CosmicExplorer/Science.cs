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
    public class Science
    {
        private Game game;
        private Inventory inventory;
        private SpaceShuttle shuttle;
        private PassivSystem passiv;
        private QuestSystem qSystem;
        public void Init(Inventory inv, Game game, SpaceShuttle shuttle, PassivSystem passiv, QuestSystem qSystem)
        {
            this.inventory = inv;
            this.game = game;
            this.shuttle = shuttle;
            this.passiv = passiv;
            this.qSystem = qSystem;
            game.sciences = true;
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Science]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        string message = "error";
        public SByte[] progress = new SByte[100];
        public void ResetVar()
        {
            Array.Fill<SByte>(progress, 0);
        }
        public void Laboratory()
        {
            while (true)
            {
                game.NewDayStart(shuttle.currentTime);
                shuttle.currentRoom = 5;
                Console.WriteLine("Du bist im Labor, was willst du machen? [Uhrzeit: " + shuttle.currentTime + ":00]");
                Console.WriteLine("Geh zu dein Equipment.[1]");
                Console.WriteLine("Ins Schlafzimmer gehen.[2]");
                Console.WriteLine("In die Kommando Zentrale gehen.[3]");
                Console.WriteLine("Zur Luftschleuse gehen.[4]");
                Console.WriteLine("In die Küche gehen.[5] X");
                Console.WriteLine("Zum Lagerraum gehen.[6]");
                Console.WriteLine("Zum Generator gehen.[7]");
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
                            LaborEquipment();
                            break;
                        case "2":
                            shuttle.currentTime++;
                            shuttle.Bedroom();
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
                            shuttle.currentTime++;
                            inventory.StorageRoom();
                            break;
                        case "7":
                            shuttle.currentTime++;
                            shuttle.Generator();
                            break;
                        case "8":
                            shuttle.currentTime++;
                            inventory.WorkshopRoom();
                            break;
                        default:
                            Console.WriteLine("Wähle einer der Nummern aus!");
                            continue;
                    }
                    break;
                }
            }
        }
        public void LaborEquipment()
        {
            while (true)
            {
                game.NewDayStart(shuttle.currentTime);
                Console.WriteLine("Du bist bei deinem Equipment, was willst du nutzen? [Uhrzeit: " + shuttle.currentTime + ":00]");
                Console.WriteLine("Nutz dein Mikroskop.[1]");
                Console.WriteLine("Geh zurück[2]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine().Trim();
                switch (message)
                {
                    case "1":
                        while (true)
                        {
                            Console.WriteLine("Welches Item willst du erforschen?");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Hinweis, das Item wird verbraucht wenn du es Forschen willst!");
                            Console.ResetColor();
                            Console.WriteLine(ItemIndex.ItemName(1) + ", ID: 01");
                            Console.WriteLine(ItemIndex.ItemName(5) + ", ID: 05");
                            if (qSystem.QState[1] == 3)
                            {
                                Console.WriteLine(ItemIndex.ItemName(11) + ", ID: 11");
                                Console.WriteLine(ItemIndex.ItemName(12) + ", ID: 12");
                            }
                            if(qSystem.QState[3] == 5)
                            {
                                Console.WriteLine(ItemIndex.ItemName(13) + ", ID: 13");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Eingabe:");
                            Console.ResetColor();
                            message = Console.ReadLine().Trim().ToLower();
                            if(message == "01")
                            {
                                if (progress[1] != 4)
                                {
                                    if (inventory.itemIndex[1] >= 1)
                                    {
                                        Console.WriteLine(ScienceProgress.ScienceProg(1, progress[1]));
                                        inventory.RemoveItem(1, 1, false);
                                        progress[1]++;
                                        shuttle.currentTime += 2;
                                        Console.ResetColor();
                                        passiv.ActionMaked();
                                        continue;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Du hast nicht genügend Treibstoff!");
                                        Console.ResetColor();
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Du hast es bereits fertig erforscht. Wähle eine andere ID (oder 'exit')");
                                    continue;
                                }
                            }
                            else if (message == "02")
                            {
                                if (progress[2] != 4)
                                {
                                    if (inventory.itemIndex[2] >= 1)
                                    {
                                        Console.WriteLine(ScienceProgress.ScienceProg(2, progress[2]));
                                        inventory.RemoveItem(2, 1, false);
                                        progress[2]++;
                                        shuttle.currentTime += 2;
                                        Console.ResetColor();
                                        passiv.ActionMaked();
                                        break;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Du hast kein Asteroiden Stück!");
                                        Console.ResetColor();
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Du hast es bereits fertig erforscht. Wähle eine andere ID (oder 'exit')");
                                    continue;
                                }
                            }
                            else if (message == "11" && qSystem.QState[1] == 3)
                            {
                                if (progress[3] != 2)
                                {
                                    if (inventory.itemIndex[11] >= 1)
                                    {
                                        Console.WriteLine(ScienceProgress.ScienceProg(3, progress[3]));
                                        inventory.RemoveItem(11, 1, false);
                                        progress[3]++;
                                        shuttle.currentTime += 2;
                                        Console.ResetColor();
                                        passiv.ActionMaked();
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Du hast es bereits fertig erforscht. Wähle eine andere ID (oder 'exit')");
                                    continue;
                                }
                            }
                            else if (message == "12" && qSystem.QState[1] == 3)
                            {
                                if (progress[4] != 2)
                                {
                                    if (inventory.itemIndex[12] >= 1)
                                    {
                                        Console.WriteLine(ScienceProgress.ScienceProg(4, progress[4]));
                                        inventory.RemoveItem(12, 1, false);
                                        progress[4]++;
                                        shuttle.currentTime += 2;
                                        Console.ResetColor();
                                        passiv.ActionMaked();
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Du hast es bereits fertig erforscht. Wähle eine andere ID (oder 'exit')");
                                    continue;
                                }
                            }
                            else if (message == "13" && qSystem.QState[3] == 5)
                            {
                                if (progress[5] != 4)
                                {
                                    if (inventory.itemIndex[13] >= 1)
                                    {
                                        Console.WriteLine(ScienceProgress.ScienceProg(5, progress[5]));
                                        inventory.RemoveItem(13, 1, false);
                                        progress[5]++;
                                        shuttle.currentTime += 2;
                                        Console.ResetColor();
                                        passiv.ActionMaked();
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Du hast es bereits fertig erforscht. Wähle eine andere ID (oder 'exit')");
                                    continue;
                                }
                            }
                            else if(message == "exit")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wähle eine gültige ID oder 'exit'");
                                continue;
                            }
                        }
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
            }
        }
    }
}

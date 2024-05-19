using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("Zum Generator gehen.[7] X");
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
                            LaborEquipment();
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
                            shuttle.currentTime++;
                            inventory.StorageRoom();
                            break;
                        case "7":
                            Console.WriteLine("Noch nicht Implementiert!");
                            continue;
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
                Console.WriteLine("Geh zurück[4]");
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
                                        shuttle.currentTime += 3;
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
                                        shuttle.currentTime += 3;
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
                                        shuttle.currentTime += 3;
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
                                        shuttle.currentTime += 3;
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
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
            }
        }
    }
}

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
        public void Init(Inventory inv, Game game, SpaceShuttle shuttle, PassivSystem passiv)
        {
            this.inventory = inv;
            this.game = game;
            this.shuttle = shuttle;
            this.passiv = passiv;
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[SpaceShuttle]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        string message = "error";
        public SByte[] progress = new SByte[10];
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
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Eingabe:");
                    Console.ResetColor();
                    message = Console.ReadLine();
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
                Console.Write("Eingabe:");
                message = Console.ReadLine();
                switch (message)
                {
                    case "1":
                        Console.WriteLine("Welches Item willst du erforschen? (Du bruachst die ID die im Storage angezeigt wird)");
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Eingabe:");
                            Console.ResetColor();
                            message = Console.ReadLine();
                            switch(message)
                            {
                                case "01":
                                    if (progress[1] != 4)
                                    {
                                        if (inventory.itemIndex[1] >= 1)
                                        {
                                            Console.WriteLine(ScienceProgress.ScienceProg(1, progress[1]));
                                            inventory.RemoveItem(1, 1, false);
                                            progress[1]++;
                                            shuttle.currentTime += 3;
                                            Console.ResetColor();
                                            if (progress[1] == 3)
                                            {
                                                game._save[10] = 1;
                                            }
                                            passiv.ActionMaked();
                                            break;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Du hast nicht genügend Benzin!");
                                            Console.ResetColor();
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Du hast es bereits fertig erforscht. Wähle eine andere ID (oder 'exit')");
                                        continue;
                                    }
                                case "exit":
                                    break;
                                default:
                                    Console.WriteLine("Wähle eine gültige ID oder 'exit'");
                                    continue;
                            }
                            break;
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

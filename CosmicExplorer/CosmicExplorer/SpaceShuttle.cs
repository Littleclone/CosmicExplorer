using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cosmic_Explorer.World;

namespace Cosmic_Explorer
{
    public class SpaceShuttle
    {
        private World world;
        private Game game;
        private SpaceShuttle shuttle;
        private Space Space;
        private Activities actions;
        private PassivSystem passiv;
        private Inventory inventory;
        private OwnMath math;
        private Quest quest;
        private QuestSystem questSystem;
        private Player player;
        private Science science;
        private NPC npc;
        string message;
        public int currentRoom = 0;
        public int currentTime;
        //shuttle
        public int Health = 100;
        public int Energy = 100;
        //Passiv System bool flags
        bool antiCrashSystem = true;
        public bool sonarActive = false;
        public void SpaceShip(Space space, Activities action, Game games, World world, PassivSystem systems, Inventory inv, OwnMath math, Quest quest, QuestSystem questS, Player player, Science scient, NPC gfi)
        {
            this.game = games;
            this.Space = space;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            this.inventory = inv;
            this.math = math;
            this.quest = quest;
            this.questSystem = questS;
            this.player = player;
            this.science = scient;
            this.npc = gfi;
            game.spaceShip = true;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[SpaceShuttle]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public void ResetVar()
        {
            currentRoom = 0;
            currentTime = 0;
            Health = 100;
            Energy = 100;
            antiCrashSystem = true;
            sonarActive = false;
        }
        public void Bedroom(int currentTime1)
        {
            game.newGame = false;
            //Setzt die Zeit und tag auf den aktuellen wert. (Und weil es im ersten Raum startet wird current Room auf 1 gesetzt)
            currentTime = currentTime1;
            currentRoom = 1;
            //Überprüft ob es bereits 23 Uhr ist
            game.NewDayStart(currentTime);
            //Zeigt das "UI" an
            Console.WriteLine("Du bist im Schlafzimmer, was willst du machen? [Uhrzeit: "+ currentTime + ":00]");
            Console.WriteLine("In die Kommando Zentrale gehen.[1]");
            Console.WriteLine("Zur Luftschleuse gehen.[2]");
            Console.WriteLine("In die Küche gehen.[3] X");
            Console.WriteLine("Zum Lagerraum gehen.[4]");
            Console.WriteLine("Zum Generator gehen.[5] X");
            Console.WriteLine("Ins Labor gehen.[6]");
            Console.WriteLine("In die Werkstatt gehen.[7]");
            Console.WriteLine("Dich ausruhen (Regeniert bissen leben).[8] X");
            Console.WriteLine("Schlafen (Tag beenden).[9]");
            Console.WriteLine("Hauptmenü öffnen (ES SPEICHERT NUR WENN EIN NEUER TAG STARTET) [10]");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine().Trim();
                //Auswahlverfahren von was der User ausgewählt hat
                switch (message)
                {
                    case "1":
                        currentTime++;
                        Commandcenter();
                        break;
                    case "2":
                        currentTime++;
                        Airlock();
                        break;
                    case "3":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "4":
                        currentTime++;
                        inventory.StorageRoom();
                        continue;
                    case "5":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "6":
                        science.Laboratory();
                        break;
                    case "7":
                        currentTime++;
                        inventory.WorkshopRoom();
                        break;
                    case "8":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "9":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Es verging viel Zeit.");
                        Console.ResetColor();
                        game.NewDayStart(23);
                        break;
                    case "10":
                        game.Start();
                        break;
                    //Debug Action
                    case "99":
                        continue;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
                return;
            }
        }
        public void Commandcenter()
        {
            currentRoom = 2;
            //Überprüft ob es bereits 23 Uhr ist
            game.NewDayStart(currentTime);
            Console.WriteLine("Du bist in der Kommando Zentrale, was willst du machen? [Uhrzeit: " + currentTime + ":00]");
            Console.WriteLine("Die Konsole öffnen.[1]");
            Console.WriteLine("Ins Schlafzimmer gehen.[2]");
            Console.WriteLine("Zur Luftschleuse gehen.[3]");
            Console.WriteLine("In die Küche gehen.[4] X");
            Console.WriteLine("Zum Lagerraum gehen.[5]");
            Console.WriteLine("Zum Generator gehen.[6] X");
            Console.WriteLine("Ins Labor gehen.[7]");
            Console.WriteLine("In die Werkstatt gehen.[8]");
            Console.WriteLine("Nehme Kontakt zu GFI auf.[9]");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine().Trim();
                switch (message)
                {
                    case "1":
                        ;
                        currentTime++;
                        if (Energy < 10)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("Nicht genügend Energie!");
                            Console.ResetColor();
                            continue;
                        }
                        Energy -= 10;
                        Konsole();
                        break;
                    case "2":
                        currentTime++;
                        Bedroom(currentTime);
                        break;
                    case "3":
                        currentTime++;
                        Airlock();
                        break;
                    case "4":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "5":
                        currentTime++;
                        inventory.StorageRoom();
                        break;
                    case "6":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "7":
                        science.Laboratory();
                        break;
                    case "8":
                        currentTime++;
                        inventory.WorkshopRoom();
                        break;
                    case "9":
                        Console.WriteLine(" ");
                        npc.NPCStartGFI();
                        Commandcenter();
                        break;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
            }
        }
        public void Airlock()
        {
            currentRoom = 3;
            //Überprüft ob es bereits 23 Uhr ist
            game.NewDayStart(currentTime);
            Console.WriteLine("Du bist in der Luftschleuse, was willst du machen? [Uhrzeit: " + currentTime + ":00] | [Restliche Energie: " + Energy +"]");
            Console.WriteLine("In den Weltraum gehen[1]");
            Console.WriteLine("Ins Schlafzimmer gehen[2]");
            Console.WriteLine("In die Kommando Zentrale gehen.[3]");
            Console.WriteLine("In die Küche gehen.[4] X");
            Console.WriteLine("Zum Lagerraum gehen.[5]");
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
                        if (Energy < 20)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                            Console.ResetColor();
                            continue;
                        }
                        Energy -= 20;
                        currentRoom = 0;
                        currentTime++;
                        Space.currentTime = currentTime;
                        Space.InSpace();
                        continue;
                    case "2":
                        currentTime++;
                        Bedroom(currentTime);
                        break;
                    case "3":
                        currentTime++;
                        Commandcenter();
                        break;
                    case "4":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "5":
                        currentTime++;
                        inventory.StorageRoom();
                        break;
                    case "6":
                        Console.WriteLine("Noch nicht Implementiert!");
                        continue;
                    case "7":
                        science.Laboratory();
                        break;
                    case "8":
                        currentTime++;
                        inventory.WorkshopRoom();
                        break;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
            }
        }
        public void Konsole()
        {
            //Überprüft ob es bereits 23 Uhr ist
            game.NewDayStart(currentTime);
            Console.WriteLine("Du hast die Konsole geöffnet. [Uhrzeit: " + currentTime + ":00] | [Restliche Energie: " + Energy +"]");
            Console.WriteLine("Welchen Befehl willst du machen? [Nutze help wenn du dich nicht mehr erinnerst]");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine().Trim().ToLower(); ;
                //überprüft ob Shuttle Energie Größer als 5 ist (5 Ist die Kleinst Mögliche Energy benötigung für eine Aktion)
                if (Energy < 5 && message != "exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Du hast zu Wenig Energie um eine Aktion durchzuführen!");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    switch (message)
                    {
                        case "exit":
                            Console.WriteLine("Konsole wird geschlossen.");
                            Commandcenter();
                            break;
                        case "help":
                            Console.WriteLine("help = Zeigt diese liste an [Kostet keine Energie]");
                            Console.WriteLine("info = gib den namen des Passiven Systems an um mehr darüber zu erfahren. [Kostet keine Energie]");
                            Console.WriteLine("kurs X Y = Steuert dein Schiff zu diesen Koordinaten. [Nutzt X Liter Benzin proportional zu der entfernung und 10 Energie]");
                            Console.WriteLine("standort = Zeigt deine aktuelle X und Y Koordinate an. [Nutzt 5 Energie aktiv]");
                            Console.WriteLine("sonar = Lässt dich das Sonar aktivieren / Deaktivieren [Nutzt 5 Energie Passiv]");
                            Console.WriteLine("scanner = Lässt dich entweder Objekte in deiner Nähe Scannen oder ein Objekt in der. [10 Energie Aktiv]");
                            Console.WriteLine("pSystems = Zeigt dir die Passiven systeme an und ob sie aktiv sind und wie viel energie sie brauchen. [Kostet keine Energie]");
                            Console.WriteLine("quests = Zeigt dir deine Aktuellen Quests an. [Kostet keine Energie]");
                            Console.WriteLine("mine = Baut die 3 Asterioden vor dem Player ab. [Kostet 20 Energie Pro Asteroid Aktiv]");
                            Console.WriteLine("call = Stelle eine Verbindung zu einem NPC um dich erhum her. [Kostet 5 Energie]");
                            Console.WriteLine("logbuch = Zeigt dir die Standorte von NPC die du bereits gesehen hast [Kostet keine Energie]");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Mehr kommt in den Nächsten Updates!");
                            Console.ResetColor();
                            goto Start;
                        case "info":
                            Console.WriteLine("Welches System willst du dir ansehen?");
                            Console.Write("Eingabe:");
                            message = Console.ReadLine();
                            switch (message)
                            {
                                case "sonar":
                                    Console.WriteLine("Zeigt bei jeder Aktiven Aktion die Koordinaten umgebener Objekte an.\n" +
                                        "NPC's und du werden Farbig markiert.\n");
                                    goto Start;
                                case "anticrash":
                                    Console.WriteLine("Verhindert das du gegen Asteroiden oder andere Objekte fliegst.\n");
                                    goto Start;
                                default:
                                    Console.WriteLine("Ungültiges System! Vorgang wird abgebrochen");
                                    continue;
                            }
                        case "kurs":
                            if (inventory.itemIndex[1] == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Du hast kein Treibstoff! Kauf dir neues bei Händlern oder Rufe GFI an.");
                                Console.ResetColor();
                                continue;
                            }
                            if (Energy < 20)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                                Console.WriteLine("Beachte das das Anti Crash System auch 10 Energie benötigt und es daher insgesamt 20 Energie sind.");
                                Console.ResetColor();
                                continue;
                            }
                            int x;
                            int y;
                        x:
                            Console.WriteLine("Bitte den Kurs für koordinate X ein");
                            message = Console.ReadLine().Trim();
                            try
                            {
                                x = Convert.ToInt32(message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Die X Koordinate hat das Falsche Format, versuche es nochmal.");
                                goto x;
                            }
                        y:
                            Console.WriteLine("Bitte den Kurs für koordinate Y ein");
                            message = Console.ReadLine().Trim();
                            try
                            {
                                y = Convert.ToInt32(message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Die Y Koordinate hat das Falsche Format, versuche es nochmal.");
                                goto y;
                            }
                            Energy -= 10;
                            world.Course(x, y, antiCrashSystem);
                            goto Start;
                        case "standort":
                            if (Energy < 5)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                                Console.ResetColor();
                                continue;
                            }
                            Energy -= 5;
                            world.ShowPosition();
                            goto Start;
                        case "sonar":
                            Console.Write("Bitte wähle aus ob du das Sonar Aktivieren oder Deaktiveren willst [Es ist gerade: ");
                            if (sonarActive)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Aktiviert");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Deaktivert");
                            }
                            Console.ResetColor();
                            Console.WriteLine("]");
                        sonarTemp:
                            Console.Write("Eingabe: ");
                            message = Console.ReadLine().Trim().ToLower();
                            switch (message)
                            {
                                case "aktivieren":
                                    Console.WriteLine("Sonar wurde aktiviert!");
                                    sonarActive = true;
                                    goto Start;
                                case "deaktivieren":
                                    Console.WriteLine("Sonar wurde deaktiviert!");
                                    sonarActive = false;
                                    goto Start;
                                default:
                                    Console.WriteLine("Bitte nutze einer der beiden Möglichkeiten");
                                    goto sonarTemp;
                            }
                        case "scanner":
                            if (Energy < 10)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine("Du hast zu Wenig Energie um eine Aktion durchzuführen!");
                                Console.ResetColor();
                                continue;
                            }
                            currentTime++;
                        Scanner:
                            Console.WriteLine("Willst du den Nahbereich Scanner nutzen oder ein entferntes Objekt scannen?(nah/fern)");
                            Console.Write("Eingabe: ");
                            message = Console.ReadLine().Trim().ToLower();
                            switch (message)
                            {
                                case "nah":
                                    Energy -= 10;
                                    world.ScannerNah();
                                    goto Start;
                                case "fern":
                                    int xCord;
                                    int yCord;
                                XScan:
                                    Console.WriteLine("Bitte die koordinate für X eingeben");
                                    message = Console.ReadLine().Trim();
                                    try
                                    {
                                        xCord = Convert.ToInt32(message);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                        Console.WriteLine("Die X Koordinate hat das Falsche Format, versuche es nochmal.");
                                        goto XScan;
                                    }
                                YScan:
                                    Console.WriteLine("Bitte die koordinate für Y eingeben");
                                    message = Console.ReadLine().Trim();
                                    try
                                    {
                                        yCord = Convert.ToInt32(message);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                        Console.WriteLine("Die Y Koordinate hat das Falsche Format, versuche es nochmal.");
                                        goto YScan;
                                    }
                                    Energy -= 10;
                                    world.ScannerFern(xCord, yCord);
                                    goto Start;
                                default:
                                    Console.WriteLine("Bitte nutze einer der beiden eingaben! ");
                                    goto Scanner;
                            }
                        case "psystems":
                            Console.WriteLine("Passiv Systems:");
                            //AntiCrash
                            Console.Write("AntiCrash: [Info: Kann derzeit NICHT deaktiviert werden]");
                            if (antiCrashSystem)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Aktiviert");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Deaktiviert");
                            }
                            Console.ResetColor();
                            Console.WriteLine(", Braucht: 10 Energie [Nur wenn der Kurs befehl genutzt wird]");
                            //Sonar
                            Console.Write("Sonar: ");
                            if (sonarActive)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Aktiviert");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Deaktiviert");
                            }
                            Console.ResetColor();
                            Console.WriteLine(", Braucht: 5 Energie");
                            goto Start;
                        case "quests":
                            questSystem.Quest();
                            goto Start;
                        case "mine":
                            if(Energy < 20)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine("Du hast zu Wenig Energie um eine Aktion durchzuführen!");
                                Console.ResetColor();
                                continue;
                            }
                            world.Mining();
                            goto Start;
                        case "call":
                            if(Energy < 5)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine("Du hast zu Wenig Energie um eine Aktion durchzuführen!");
                                Console.ResetColor();
                                continue;
                            }
                            Energy -= 5;
                            world.CallNPC();
                            goto Start;
                        case "logbuch":
                            world.Logbuch();
                            goto Start;
                        default:
                            Console.WriteLine("Wähle einer der (implementierten) Befehle aus!");
                            continue;              
                    }
                Start:
                    //Überprüft ob es bereits 23 Uhr ist
                    game.NewDayStart(currentTime);
                    Console.WriteLine("Die Konsole ist offen. [Uhrzeit: " + currentTime + ":00] | [Restliche Energie: " + Energy + "]");
                    Console.WriteLine("Welchen Befehl willst du machen? [Nutze help wenn du dich nicht mehr erinnerst]");
                }
            }
        }
    }
}

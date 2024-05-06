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
        private Math math;
        private Quest quest;
        private QuestSystem questSystem;
        string message;
        int currentRoom = 0;
        int day;
        public int currentTime;
        //shuttle
        public int Health = 100;
        public int Energy = 100;
        //Passiv System bool flags
        bool antiCrashSystem = true;
        public bool sonarActive = false;
        public void SpaceShip(Space space, Activities action, Game games, World world, PassivSystem systems, Inventory inv, Math math, Quest quest, QuestSystem questS)
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
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[SpaceShuttle]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public void Bedroom(int currentTime1, int day1)
        {
            //Setzt die Zeit und tag auf den aktuellen wert. (Und weil es im ersten Raum startet wird current Room auf 1 gesetzt)
            currentTime = currentTime1;
            day = day1;
            currentRoom = 1;
            //Überprüft ob es bereits 23 Uhr ist
            if (currentTime == 23)
            {
                game.NewDayStart(currentTime, day);
            }
            //Zeigt das "UI" an
            Console.WriteLine("Du bist im Schlafzimmer, was willst du machen? [Uhrzeit: "+ currentTime + ":00]");
            Console.WriteLine("In die Kommando Zentrale gehen.[1]");
            Console.WriteLine("Zur Luftschleuse gehen.[2]");
            Console.WriteLine("In die Küche gehen.[3]");
            Console.WriteLine("Zum Generator gehen.[4]");
            Console.WriteLine("Dich ausruhen (Regeniert bissen leben).[5]");
            Console.WriteLine("Schlafen (Tag beenden).[6]");
            Console.WriteLine("Hauptmenü öffnen (ES SPEICHERT NUR WENN EIN NEUER TAG STARTET) [7]");
        WrongInput:
            Console.Write("Eingabe:");
            message = Console.ReadLine();
            //Auswahlverfahren von was der User ausgewählt hat
            switch(message){
                case "1":
                    currentTime = currentTime1;
                    currentTime++;
                    Commandcenter();
                break;
                case "2":
                    Console.WriteLine("Noch nicht Implementiert!");
                    goto WrongInput;
                case "3":
                    Console.WriteLine("Noch nicht Implementiert!");
                    goto WrongInput;
                case "4":
                    Console.WriteLine("Noch nicht Implementiert!");
                    goto WrongInput;
                case "5":
                    Console.WriteLine("Noch nicht Implementiert!");
                    goto WrongInput;
                case "6":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Es verging viel Zeit.");
                    Console.ResetColor();
                    game.NewDayStart(23, day);
                    break;
                case "7":
                    game.Start();
                    break;
                default:
                    Console.WriteLine("Wähle einer der Nummern aus!");
                    goto WrongInput;
            }
            return;
        }
        public void Commandcenter()
        {
            currentRoom = 2;
            //Überprüft ob es bereits 23 Uhr ist
            if (currentTime == 23)
            {
                game.NewDayStart(currentTime, day);
            }
            Console.WriteLine("Du bist in der Kommando Zentrale, was willst du machen? [Uhrzeit: " + currentTime + ":00]");
            Console.WriteLine("Die Konsole öffnen[1]");
            Console.WriteLine("Ins Schlafzimmer gehen[2]");
            Console.WriteLine("Zur Luftschleuse gehen.[2]");
            Console.WriteLine("In die Küche gehen.[3]");
            Console.WriteLine("Zum Generator gehen.[4]");
        WrongInput:
            Console.Write("Eingabe:");
            message = Console.ReadLine();
            switch (message)
            {
                case "1":;
                    currentTime++;
                    math.EnergyController(10, Energy);
                    if (!math.result)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Nicht genügend Energie!");
                        Console.ResetColor();
                        goto WrongInput;
                    }
                    Energy = math.x;
                    Konsole();
                    break;
                case "2":;
                    currentTime++;
                    Bedroom(currentTime, day);
                    break;
                case "3":
                    Console.WriteLine("Noch nicht Implementiert!");
                    goto WrongInput;
                case "4":
                    Console.WriteLine("Noch nicht Implementiert!");
                    goto WrongInput;
                default:
                    Console.WriteLine("Wähle einer der Nummern aus!");
                    goto WrongInput;
            }
        }
        public void Konsole()
        {
            //Überprüft ob es bereits 23 Uhr ist
            if (currentTime == 23)
            {
                game.NewDayStart(currentTime, day);
            }
            Console.WriteLine("Du hast die Konsole geöffnet. [Uhrzeit: " + currentTime + ":00] | [Restliche Energie: " + Energy +"]");
            Console.WriteLine("Welchen Befehl willst du machen? [Nutze help wenn du dich nicht mehr erinnerst]");
        WrongInput:
            Console.Write("Eingabe:");
            message = Console.ReadLine();
            //überprüft ob Shuttle Energie Größer als 5 ist (5 Ist die Kleinst Mögliche Energy benötigung für eine Aktion)
            if (Energy < 5 && message != "exit")
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Du hast zu Wenig Energie um eine Aktion durchzuführen!");
                Console.ResetColor();
                goto WrongInput;
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
                        Console.WriteLine("info = gib den namen des Passiven Systems an um mehr darüber zu erfahren [Kostet keine Energie]");
                        Console.WriteLine("kurs X Y = Steuert dein Schiff zu diesen Koordinaten [Nutzt X Liter Benzin proportional zu der entfernung]");
                        Console.WriteLine("standort = Zeigt deine aktuelle X und Y Koordinate an [Nutzt 5 Energie aktiv]");
                        Console.WriteLine("sonar = Lässt dich das Sonar aktivieren / Deaktivieren [Nutzt 5 Energie Passiv]");
                        Console.WriteLine("scanner = Lässt dich entweder Objekte in deiner Nähe Scannen oder ein Objekt in der Ferne [10 Energie Aktiv]");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Mehr ist noch nicht Implementiert!");
                        Console.ResetColor();
                        goto Start;
                    case "info":
                        Console.WriteLine("Welches System willst du dir ansehen?");
                        Console.Write("Eingabe:");
                        message = Console.ReadLine();
                        switch (message)
                        {
                            case "sonar":
                                Console.WriteLine("Zeigt bei jeder Aktiven Aktion die Koordinaten umgebener Objekte an\n");
                                goto Start;
                            default:
                                goto WrongInput;
                        }
                    case "logbuch":
                        Console.WriteLine("Hier sind deine Missionen:");
                        goto Start;
                    case "kurs":
                        math.EnergyController(10, Energy);
                        Energy = math.x;
                        if (!math.result)
                        {
                            Console.WriteLine("Nicht genügend Energie!");
                            goto WrongInput;
                        }
                        int x;
                        int y;
                    x:
                        Console.WriteLine("Bitte den Kurs für koordinate X ein");
                        message = Console.ReadLine();
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
                        message = Console.ReadLine();
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
                        world.Course(x, y, antiCrashSystem);
                        goto Start;
                    case "standort":
                        math.EnergyController(5, Energy);
                        Energy = math.x;
                        if (!math.result)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                            Console.ResetColor();
                            goto WrongInput;
                        }
                        world.ShowPosition();
                        goto Start;
                    case "sonar":
                        Console.Write("Bitte wähle aus ob du das Sonar Aktivieren oder Deaktiveren willst [Es ist gerade: ");
                        if (sonarActive)
                        {
                            Console.WriteLine("Aktiviert]");
                        }
                        else
                        {
                            Console.WriteLine("Deaktivert]");
                        }
                        Console.Write("Eingabe: ");
                        message = Console.ReadLine();
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
                                goto WrongInput;
                        }
                    case "scanner":
                        math.EnergyController(10, Energy);
                        Energy = math.x;
                        if (!math.result)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("Du hast zu Wenig Energie um eine Aktion durchzuführen!");
                            Console.ResetColor();
                            goto WrongInput;
                        }
                        currentTime++;
                    Scanner:
                        Console.WriteLine("Willst du den Nahbereich Scanner nutzen oder ein entferntes Objekt scannen?(nah/fern)");
                        Console.Write("Eingabe: ");
                        message = Console.ReadLine();
                        switch (message)
                        {
                            case "nah":
                                world.ScannerNah();
                                goto Start;
                            case "fern":
                                int xCord;
                                int yCord;
                            XScan:
                                Console.WriteLine("Bitte die koordinate für X eingeben");
                                message = Console.ReadLine();
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
                                message = Console.ReadLine();
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
                    //PSystems = Passiv, Systems
                    case "pSystems":
                        Console.WriteLine("Passiv Systems:");
                        //Sonar
                        Console.Write("Sonar: "); 
                        if(sonarActive)
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
                    default:
                        Console.WriteLine("Wähle einer der (implementierten) Befehle aus!");
                        goto WrongInput;
                }
                Start:
                //Überprüft ob es bereits 23 Uhr ist
                if (currentTime == 23)
                {
                    game.NewDayStart(currentTime, day);
                }
                    Console.WriteLine("Die Konsole ist offen. [Uhrzeit: " + currentTime + ":00] | [Restliche Energie: " + Energy + "]");
                    Console.WriteLine("Welchen Befehl willst du machen? [Nutze help wenn du dich nicht mehr erinnerst]");
                    goto WrongInput;
            }
        }
    }
}

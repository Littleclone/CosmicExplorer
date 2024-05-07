using System;

namespace Cosmic_Explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
            game.LoadSaveFile();
            game.introduction();
            game.NewDayStart(23, 0);
            Console.WriteLine("Debug-Error[Main Function]"); //Wird ausgelöst wenn aus irgeindein grund das program wieder zur Main Function kommt (Sollte im normalfall nicht passieren)
        }
    }
    public class Game
    {
        World world = new World();
        SpaceShuttle shuttle = new SpaceShuttle();
        Space space = new Space();
        Activities activities = new Activities();
        PassivSystem system = new PassivSystem();
        Inventory inventory = new Inventory();
        Math math = new Math();
        Quest quest = new Quest();
        QuestSystem questSystem = new QuestSystem();
        int CurrentDay = 0; //Start Tag
        public int[,] _save = new int[30,30]; //Space um werte zu speichern (Für abfragen)
        public bool debug = false;
        bool init_Objects = false;
        int isWorld = 0;
        public void Start()
        {
        Start:
            string message = string.Empty;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cosmic Explorer");
            Console.WriteLine("Guten Tag, das ist Cosmic Explorer, ein Text Basiertes Weltraum Spiel\nDieses Text Game befindet sich in der Testphase und wird noch Entwickelt von mir um mehr C# zu lernen.");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Game Version: 0.1.3 Dev Phase Deutsch / German");
            Console.ResetColor();
            Console.WriteLine("Willst du starten? [start = Starte das spiel, exit = Verlasse das Spiel, new game = löscht deine Save File]");
            Console.Write("Eingabe:");
            message = Console.ReadLine();
            switch (message)
            {
                case "start":
                    Console.WriteLine("Starte das Game");
                    break;
                case "exit":
                    Console.WriteLine("Verlasse das Game");
                    Environment.Exit(0);
                    break;
                case "new game":
                    deleteFile:
                    Console.WriteLine("Bist du dir sicher das du sie löschen willst?(yes/no)");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sie lässt sich NICHT mehr wiederherstellen und ist dann für IMMER weg");
                    Console.ResetColor();
                    message = Console.ReadLine();
                    switch (message)
                    {
                        case "no":
                            goto Start;
                        case "yes":
                            Saver.DeleteSaveFile();
                            goto Start;
                        default:
                            Console.WriteLine("Bitte wähle eines der beiden optionen!");
                            goto deleteFile;
                    }
                case "start skip":
                    Console.WriteLine("Starte das game und skippe Introduction");
                    _save[0, 0] = 1;
                    NewDayStart(23, 0);
                    break;
                case "start debug":
                    debug = true;
                    goto Start;
                case "exit debug":
                    debug = false;
                    goto Start;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entscheide dich für eines der zwei möglichkeiten!(Groß und klein schreibung wird beachtet)\n");
                    goto Start;
            }
        }
        public void LoadSaveFile()
        {
            //Für Später wenn man den Spielstand lädt
        }
        public void introduction()
        {
            if (_save[0, 0] == 0)
            {
                _save[0,0] = 1;
                string message = string.Empty;
                introduction:
                Console.WriteLine("\nBevor es Losgeht, in diesem Spiel bist du eine Forscherin namens Charlotte, du befindest dich auf einem Raumschiff und \nlebst da allein. Dieses Spiel ist rein auf Text basiert und der Kreativität von dir. Es wird für die nächste Aktion \nimmer auf Input gewartet (von der Tastatur), auch damit Texte weiter gehen.");
                Console.ReadKey();
                Console.WriteLine("\nWillkommen Charlotte, dies ist dein Erster Tag hier auf dem Schiff stimmts?\nKeine Sorge, ich werde dir eine kurze Einführung geben in die Systeme des schiffs");
                Console.ReadKey();
                Console.WriteLine("Wichtigste Info Vorab, es gibt ein Tag System, fast alle aktionen verbrauchen Energie (Darauf später zurück)\nund Zeit (Darauf später zurück)");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Es SPEICHERT IMMER ERST wenn ein NEUER Tag STARTET.");
                Console.ResetColor();
                Console.ReadKey();
                Console.WriteLine("Das Energie System:\nDas Raumschiff hat Energie, diese wird durch ein Generator und Sonnar Panelle bereit gestellt.\nBei vielen aktionen im Schiff wird Aktiv Energie Verwendet, heißt eine Aktion wo das Schiff etwas aktives tut \nverbraucht Energie.");
                Console.ReadKey();
                Console.WriteLine("Es gibt aber auch Passive aktionen die die Energie des schiffes verbrauchen, beispiel das das Licht an ist oder das \nLebenserhaltungs System funktioniert.\nBei weiteren erklärungen wird immer gesagt ob dies Passiv (also immer energie verbraucht solange es aktiv ist und \neine aktive Aktion ausgeführt wird) oder eine aktive aktion ist.");
                Console.ReadKey();
                Console.WriteLine("Aber nochmal im grobem gesagt:\nImmer wenn eine Aktive aktion ausgeführt wird, werden auch die Passiven ausgeführt\nund damit werden die Passiven System auch energie verwenden.");
                Console.ReadKey();
                Console.WriteLine("Dein Raumanzug hat auch Energie, diese wird bei jeder aktiven aktion verbraucht, außerdem hat diese auch ein \npaar Passive aktionen wie das bereitstellen von Atemluft (oder kurz gesagt, das Lebenserhaltungs System).");
                Console.ReadKey();
                Console.WriteLine("Aber keine sorge, es wird recht offensichtlich sein welche aktion aktiv und welche passiv sind (Bei den help\nCommands (In der konsole) wird auch immer angezeigt wieviel energie etwas verwendet und ob es aktiv oder passiv ist.)");
                Console.ReadKey();
            //Weiteres kommt bei größerem Entwicklungs Fortschritt des Games
            verstanden:
                Console.WriteLine("Hast du soweit alles verstanden?(ja, nein)");
                Console.Write("Eingabe:");
                message = Console.ReadLine();
                switch (message)
                {
                    case "ja":
                        Console.WriteLine("Super, dann beginnen wir nun dein Ersten Tag, viel Erfolg :D\n");
                        break;
                    case "nein":
                        Console.WriteLine("Na dann wiederhole ich es nochmal");
                        goto introduction;
                    default:
                        goto verstanden;
                }
            }
        }
        public void SaveFileInit()
        {
            world.SaveWorld();
            Saver.SaveArray<int>("Save", _save);
            Saver.Save("SaveFile", "Day", CurrentDay);
        }
        public void NewDayStart(int time, int day) //Funktion um den neuen Tag zu starten
        {
            const int BREAKER = 1;
            CurrentDay = day;
            if (time >= 23.0)
            {
                //Erstmalige generierung der Welt und zuweisung von Objekten
                if (init_Objects == false)
                {
                    init_Objects = true;
                    shuttle.SpaceShip(space, activities, this, world, system, inventory, math, quest, questSystem);
                    world.Worlds(shuttle, space, activities, this, system, inventory, questSystem);
                    activities.Actions(shuttle, space, this, world, system, inventory);
                    system.Passiv(shuttle, space, activities, this, world, inventory, math, questSystem);
                    space.Space_(shuttle, activities, this, world, system, inventory, math);
                    inventory.InvInit(shuttle, space, activities, this, world, system);
                    questSystem.QuestSystemInit(quest,space, activities, this, world, system, inventory, math);
                    quest.QuestInit(questSystem, this);
                    //Sollte bereits ein spielstand existieren muss die Welt nicht nochmal generiert werden.
                    if (BREAKER != isWorld)
                    {
                        world.WorldGenerator();
                        isWorld = BREAKER;
                    }
                }
                CurrentDay++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Es ist Spät und du gingst ins Bett");
                Console.WriteLine("Tag " + CurrentDay + " Startet");
                Console.ResetColor();
                //automatische Speicherung wenn der Nächste Tag Startet (erst wenn das Spiel Tag 2 Startet)
                if (_save[0, 2] == BREAKER)
                {
                    Saver.DeleteSaveFile();
                    SaveFileInit();
                    if (debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Spielstand Gespeichert! [NUR WÄHREND DES DEBUGS VISIBLE]");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Spiel hat gespeichert.");
                    Console.ResetColor();
                }
                else
                {
                    _save[0, 2] = BREAKER;
                }
                shuttle.Bedroom(6, CurrentDay);
                return;
            }
        }
    }
}
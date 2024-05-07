using CosmicExplorer;
using System;

namespace Cosmic_Explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.initObjekts();
            game.Start();
            game.LoadSaveFile();
            game.introduction();
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
        ulong CurrentDay = 0; //Start Tag
        public int[] _save = new int[60]; //Space um werte zu speichern (Für abfragen)
        public bool debug = false;
        bool init_Objects = false;
        bool isWorld = false;
        const int BREAKER = 1;
        public void Start()
        {
            while (true)
            {
                string message = string.Empty;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Cosmic Explorer");
                Console.WriteLine("Guten Tag, das ist Cosmic Explorer, ein Text Basiertes Weltraum Spiel\nDieses Text Game befindet sich in der Testphase und wird noch Entwickelt von mir um mehr C# zu lernen.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Game Version: 0.1.4 Dev Phase Deutsch / German");
                Console.ResetColor();
                Console.WriteLine("Willst du starten? [start = Starte das spiel, exit = Verlasse das Spiel, new game = löscht deine Save File]");
                Console.Write("Eingabe:");
                message = Console.ReadLine();
                switch (message)
                {
                    case "start":
                        Console.WriteLine("Starte das Game");
                        return;
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
                                continue;
                            case "yes":
                                DataManager.DeleteSaveFiles();
                                continue;
                            default:
                                Console.WriteLine("Bitte wähle eines der beiden optionen!");
                                goto deleteFile;
                        }
                    case "start skip":
                        Console.WriteLine("Starte das game und skippe Introduction");
                        _save[0] = 1;
                        LoadSaveFile();
                        NewDayStart(23);
                        break;
                    case "start debug":
                        debug = true;
                        continue;
                    case "exit debug":
                        debug = false;
                        continue;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Entscheide dich für eines der zwei möglichkeiten!(Groß und klein schreibung wird beachtet)\n");
                        continue;
                }
            }
        }
        public void LoadSaveFile()
        {
            try
            {
                //game
                CurrentDay = DataManager.LoadData<ulong>("saveFile", "day");
                CurrentDay -= 1;
                isWorld = DataManager.LoadData<bool>("saveFile", "isWorld");
                //shuttle
                shuttle.Energy = DataManager.LoadData<int>("shuttle", "energy");
                shuttle.Health = DataManager.LoadData<int>("shuttle", "health");
                shuttle.sonarActive = DataManager.LoadData<bool>("shuttle", "sonar");
                //space
                space.spaceSuitEnergy = DataManager.LoadData<int>("space", "energy");
                space.spaceSuitHealth = DataManager.LoadData<int>("space", "health");
                space.SolarPanelHealth = DataManager.LoadData<int>("space", "solarHealth");
                space.AntennenHealth = DataManager.LoadData<int>("space", "antennasHealth");
                //arrays
                int[] loadedSave = DataManager.LoadData<int[]>("saveFile", "save");
                _save = loadedSave;
                world.LoadWorld();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Spielstand geladen!");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Spielstand konnte nicht geladen werden!");
                Console.ResetColor();
            }
        }
        public void SaveFileInit()
        {
            try
            {
                //game
                DataManager.SaveData("saveFile", "day", CurrentDay);
                DataManager.SaveData("saveFile", "isWorld", isWorld);
                //shuttle
                DataManager.SaveData("shuttle", "energy", shuttle.Energy);
                DataManager.SaveData("shuttle", "health", shuttle.Health);
                DataManager.SaveData("shuttle", "sonar", shuttle.sonarActive);
                //space
                DataManager.SaveData("space", "energy", space.spaceSuitEnergy);
                DataManager.SaveData("space", "health", space.spaceSuitHealth);
                DataManager.SaveData("space", "solarHealth", space.SolarPanelHealth);
                DataManager.SaveData("space", "antennasHealth", space.AntennenHealth);
                //arrays
                DataManager.SaveData("saveFile","save", _save);
                world.SaveWorld();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Spiel hat gespeichert.");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Spielstand konnte nicht gespeichert werden!");
                Console.ResetColor();
            }
        }
        public void initObjekts()
        {
            //Erstmalige generierung der Welt und zuweisung von Objekten
            if (init_Objects == false)
            {
                Console.WriteLine("Test");
                init_Objects = true;
                shuttle.SpaceShip(space, activities, this, world, system, inventory, math, quest, questSystem);
                world.Worlds(shuttle, space, activities, this, system, inventory, questSystem);
                activities.Actions(shuttle, space, this, world, system, inventory);
                system.Passiv(shuttle, space, activities, this, world, inventory, math, questSystem);
                space.Space_(shuttle, activities, this, world, system, inventory, math);
                inventory.InvInit(shuttle, space, activities, this, world, system);
                questSystem.QuestSystemInit(quest, space, activities, this, world, system, inventory, math);
                quest.QuestInit(questSystem, this);
            }
        }
        public void introduction()
        {
            if (_save[0] == 0)
            {
                _save[0] = 1;
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
                        NewDayStart(23);
                        break;
                    case "nein":
                        Console.WriteLine("Na dann wiederhole ich es nochmal");
                        goto introduction;
                    default:
                        goto verstanden;
                }
            }
        }
        public void NewDayStart(int time) //Funktion um den neuen Tag zu starten
        {
            const bool WorldBREAKER = true;
            if (time >= 23.0)
            {
                CurrentDay++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Es ist Spät und du gingst ins Bett");
                Console.WriteLine("Tag " + CurrentDay + " Startet");
                Console.ResetColor();
                //automatische Speicherung wenn der Nächste Tag Startet (erst wenn das Spiel Tag 2 Startet)
                if (_save[1] == BREAKER)
                {
                    DataManager.DeleteSaveFiles();
                    SaveFileInit();
                    if (debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Spielstand Gespeichert! [NUR WÄHREND DES DEBUGS VISIBLE]");
                        Console.ResetColor();
                    }
                }
                //Sollte bereits ein spielstand existieren muss die Welt nicht nochmal generiert werden.
                if (isWorld != WorldBREAKER)
                {
                    world.WorldGenerator();
                    isWorld = WorldBREAKER;
                    _save[1] = BREAKER;
                }
                shuttle.Bedroom(6);
            }
            else
            {
                return;
            }
        }
    }
}
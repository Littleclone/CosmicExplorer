using CosmicExplorer;
using System;

namespace Cosmic_Explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            while (true)
            {
                game.Start();
                Console.WriteLine("Debug-Error[Main Function]"); //Wird ausgelöst wenn aus irgeindein grund das program wieder zur Main Function kommt (Sollte im normalfall nicht passieren)
            }
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
        OwnMath math = new OwnMath();
        Quest quest = new Quest();
        QuestSystem questSystem = new QuestSystem();
        Player player = new Player();
        Science science = new Science();
        Trade trade = new Trade();
        NPC hanna = new NPC("Hanna", -1, "0", "0");
        NPC gfi = new NPC("GFI", 1, "0", "0");
        NPC lea = new NPC("Lea", 2, "0102030405060708", "02030405060708");
        NPC supplier = new NPC("The Supplier", 3, "0", "0");
        ulong CurrentDay = 0; //Start Tag
        public ulong DayCounter = 0; //Zählt die Tage bis 30
        public int[] _save = new int[300]; //Space um werte zu speichern (Für abfragen)
        string message = string.Empty;
        //Flags
        public bool debug = false; //Flag for Debug Mode, this show some information and processes happened in the background (for public)
        public bool dev = true; //Flag for Dev Mode (not for Public)
        public bool hardCore = false; //Flag if the mode is harcore
        //Kontroll Variablen
        bool init_Objects = false;
        bool isWorld = false;
        bool needSave = false;
        public bool newGame = true;
        public bool currentSession = false;
        const int BREAKER = 1;
        //Error Variablen
        public bool fileManipulation = false;
        bool failedToSave = false;
        public bool failedToSaveWorld = false;
        bool failedToLoad = false;
        public bool failedToLoadWorld = false;
        public bool spaceShip = false;
        public bool spaces = false;
        public bool worlds = false;
        public bool actions = false;
        public bool passivs = false;
        public bool inv = false;
        public bool qsystem = false;
        public bool quests = false;
        public bool mainPlayer = false;
        public bool sciences = false;
        public bool worldInit = false;
        public bool npcHanna = false;
        public bool npcLea = false;
        public void Start()
        {
            while (true)
            {
                if (needSave)
                {
                    CurrentDay--;
                }
                DataManager.InitializeSaveFileFirst();
                needSave = false;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Cosmic Explorer");
                Console.WriteLine("Guten Tag, das ist Cosmic Explorer, ein Text Basiertes Weltraum Spiel\nDieses Text Game befindet sich in der Testphase und wird noch Entwickelt von mir um mehr C# zu lernen.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Game Version: 0.2.1 Dev Phase, Deutsch / German");
                Console.ResetColor();
                Console.WriteLine("Willst du starten? [start = Starte das spiel, exit = Verlasse das Spiel, new game = löscht deine Save File]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine().Trim().ToLower();
                switch (message)
                {
                    case "start":
                        Console.WriteLine("Starte das Game");
                        initObjekts();
                        LoadSaveFile();
                        currentSession = true;
                        ErrorHandling();
                        introduction();
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
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Eingabe:");
                        Console.ResetColor();
                        message = Console.ReadLine().Trim().ToLower();
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
                        initObjekts();
                        LoadSaveFile();
                        currentSession = true;
                        ErrorHandling();
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
                        Console.WriteLine("Entscheide dich für eines der drei möglichkeiten!\n");
                        continue;
                }
            }
        }
        public void LoadSaveFile()
        {
            if (!currentSession)
            {
                try
                {
                    newGame = DataManager.LoadData<bool>("saveFile", "newGame");
                    //Detects for manipulation in the Save File
                }
                catch (FileNotFoundException ex)
                {
                    // Behandlung der FileNotFoundException
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Fehler beim Laden der Daten: {ex.Message}");
                    Console.ResetColor();
                    failedToLoad = true;
                }
                catch (InvalidDataException ex)
                {
                    // Behandlung der InvalidDataException
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Fehler beim Laden der Daten: {ex.Message}");
                    Console.ResetColor();
                    failedToLoad = true;
                }
                catch (Exception ex)
                {
                    // Allgemeine Exception-Behandlung für alle anderen Fälle
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                    Console.ResetColor();
                    failedToLoad = true;
                }
            }
            if (!newGame && !currentSession)
            {
                try
                {
                    //game
                    CurrentDay = DataManager.LoadData<ulong>("saveFile", "day");
                    DayCounter = DataManager.LoadData<ulong>("saveFile", "dayCounter");
                    isWorld = DataManager.LoadData<bool>("saveFile", "isWorld");
                    hardCore = DataManager.LoadData<bool>("saveFile", "hardcoreFlag");
                    //shuttle
                    //shuttle.Energy = DataManager.LoadData<int>("shuttle", "energy");
                    shuttle.Health = DataManager.LoadData<int>("shuttle", "health");
                    shuttle.sonarActive = DataManager.LoadData<bool>("shuttle", "sonar");
                    //space
                    //space.spaceSuitEnergy = DataManager.LoadData<int>("space", "energy");
                    space.spaceSuitHealth = DataManager.LoadData<int>("space", "health");
                    space.SolarPanelHealth = DataManager.LoadData<int>("space", "solarHealth");
                    space.AntennenHealth = DataManager.LoadData<int>("space", "antennasHealth");
                    //science
                    science.progress = DataManager.LoadData<sbyte[]>("science", "progress");
                    //quests
                    questSystem.QState = DataManager.LoadData<sbyte[]>("quests", "questSave");
                    //inventory
                    inventory.itemIndex = DataManager.LoadData<int[]>("inventory", "inv");
                    //player
                    player.gold = DataManager.LoadData<int>("player", "gold");
                    //NPC
                    hanna.state = DataManager.LoadData<sbyte>("npc", "hanna");
                    gfi.state = DataManager.LoadData<sbyte>("npc", "gfi");
                    lea.state = DataManager.LoadData<sbyte>("npc", "lea");
                    supplier.state = DataManager.LoadData<sbyte>("npc", "supplier");
                    //arrays
                    int[] loadedSave = DataManager.LoadData<int[]>("saveFile", "save");
                    _save = loadedSave;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Spielstand geladen!");
                    Console.ResetColor();
                    failedToLoad = false;
                    world.LoadWorld();
                }
                catch (FileNotFoundException ex)
                {
                    // Behandlung der FileNotFoundException
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Fehler beim Laden der Daten: {ex.Message}");
                    Console.ResetColor();
                    failedToLoad = true;
                }
                catch (InvalidDataException ex)
                {
                    // Behandlung der InvalidDataException
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Fehler beim Laden der Daten: {ex.Message}");
                    Console.ResetColor();
                    failedToLoad = true;
                }
                catch (Exception ex)
                {
                    // Allgemeine Exception-Behandlung für alle anderen Fälle
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                    Console.ResetColor();
                    failedToLoad = true;
                }
            }
        }
        public void SaveData()
        {
            try
            {
                //game
                DataManager.SaveData("saveFile", "newGame", false);
                DataManager.SaveData("saveFile", "day", CurrentDay - 1);
                DataManager.SaveData("saveFile", "dayCounter", DayCounter - 1);
                DataManager.SaveData("saveFile", "isWorld", isWorld);
                DataManager.SaveData("saveFile", "hardcoreFlag", hardCore);
                //shuttle
                //DataManager.SaveData("shuttle", "energy", shuttle.Energy);
                DataManager.SaveData("shuttle", "health", shuttle.Health);
                DataManager.SaveData("shuttle", "sonar", shuttle.sonarActive);
                //space
                //DataManager.SaveData("space", "energy", space.spaceSuitEnergy);
                DataManager.SaveData("space", "health", space.spaceSuitHealth);
                DataManager.SaveData("space", "solarHealth", space.SolarPanelHealth);
                DataManager.SaveData("space", "antennasHealth", space.AntennenHealth);
                //science
                DataManager.SaveData("science", "progress", science.progress);
                //quests
                DataManager.SaveData("quests", "questSave", questSystem.QState);
                //inventory
                DataManager.SaveData("inventory", "inv", inventory.itemIndex);
                //player
                DataManager.SaveData("player", "gold", player.gold);
                //NPC
                DataManager.SaveData("npc", "hanna", hanna.state);
                DataManager.SaveData("npc", "gfi", gfi.state);
                DataManager.SaveData("npc", "lea", lea.state);
                DataManager.SaveData("npc", "supplier", supplier.state);
                //arrays
                DataManager.SaveData("saveFile", "save", _save);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Spiel hat gespeichert.");
                Console.ResetColor();
                failedToSave = false;
                world.SaveWorld();
            }
            catch (FileNotFoundException ex)
            {
                // Behandlung der FileNotFoundException
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Fehler beim Speichern der Daten: {ex.Message}");
                Console.ResetColor();
                failedToSave = true;
            }
            catch (InvalidDataException ex)
            {
                // Behandlung der InvalidDataException
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Fehler beim Speichern der Daten: {ex.Message}");
                Console.ResetColor();
                failedToSave = true;
            }
            catch (Exception ex)
            {
                // Allgemeine Exception-Behandlung für alle anderen Fälle
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                Console.ResetColor();
                failedToSave = true;
            }
        }
        public void initObjekts()
        {
            //Erstmalige generierung der Welt und zuweisung von Objekten
            if (init_Objects == false)
            {
                init_Objects = true;
                shuttle.SpaceShip(space, activities, this, world, system, inventory, math, quest, questSystem, player, science, gfi);
                world.Worlds(shuttle, space, activities, this, system, inventory, questSystem, science);
                activities.Actions(shuttle, space, this, world, system, inventory);
                system.Passiv(shuttle, space, activities, this, world, inventory, math, questSystem);
                space.Space_(shuttle, activities, this, world, system, inventory, math);
                inventory.InvInit(shuttle, space, activities, this, world, system, science);
                questSystem.QuestSystemInit(quest, space, activities, this, world, system, inventory, math, science);
                quest.QuestInit(questSystem, this);
                player.ObjInit(this);
                science.Init(inventory, this, shuttle, system, questSystem);
                //NPC init
                world.InitNPCData(hanna, lea, supplier);
                //NPC
                hanna.initObj(questSystem, this, trade, science, quest, inventory, player);
                gfi.initObj(questSystem, this, trade, science, quest, inventory, player);
                lea.initObj(questSystem, this, trade, science, quest, inventory, player);
                supplier.initObj(questSystem, this, trade, science, quest, inventory, player);
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
                Console.WriteLine("\nCharlotte arbeitet beim Galaktische Forschungsinstitut [GFI], dies ist eine Staatliche Organisation. " +
                    "\n[Mehr infos in Kommenden Updates].");
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
                message = Console.ReadLine().Trim().ToLower(); ;
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
            NewDayStart(23);
        }
        public void NewDayStart(int time) //Funktion um den neuen Tag zu starten
        {
            const bool WorldBREAKER = true;
            if (time >= 23.0)
            {
                CurrentDay++;
                DayCounter++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Es ist Spät und du gingst ins Bett");
                Console.WriteLine("Tag " + CurrentDay + " Startet");
                Console.ResetColor();
                //automatische Speicherung wenn der Nächste Tag Startet (erst wenn das Spiel Tag 2 Startet)
                if (_save[1] == BREAKER && needSave)
                {
                    DataManager.BackupSaveFiles();
                    SaveData();
                    ErrorHandling();
                }
                //Sollte bereits ein spielstand existieren muss die Welt nicht nochmal generiert werden.
                if (isWorld != WorldBREAKER)
                {
                    world.WorldGenerator();
                    isWorld = WorldBREAKER;
                    _save[1] = BREAKER;
                    inventory.AddItem(1, 1000, false);
                    inventory.AddItem(8, 20, false);
                }
                needSave = true;
                shuttle.Energy = 1000;
                if(DayCounter == 30)
                {
                    DayCounter = 0;
                    player.AddGold(1500);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Du hast ein Gehalt von 1500 Gold bekommen.");
                    Console.ResetColor();
                }
                shuttle.Bedroom(6);
            }
            else
            {
                return;
            }
        }
        public void ErrorHandling()
        {
            if (failedToSave)
            {
                while (true && failedToSave)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Sicherheits Menü!");
                    Console.WriteLine("Dein Spielstand konnte NICHT gespeichert werden! Deshalb wurde das Sicherheits Menü geöffnet.");
                    Console.WriteLine("Du hast nun folgende Möglichkeiten die du machen kannst:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("HINWEIS: Schließe das Game NICHT außer keines der unten gennanten Möglichkeiten Funktioniert.");
                    Console.WriteLine("Lies dir die Fehlermeldung durch.");
                    Console.WriteLine("Solltest du an der Save File was verändert haben mach es Rückgängig!");
                    Console.WriteLine("Geh sicher das diese Anwendung in die files Schreiben kann (Schreib berechtigung).");
                    Console.WriteLine("Solltest du ein Antivirus haben der nicht Standart mäßig vom System kommt könnte dieser Falschen Alarm melden.");
                    Console.WriteLine("Sollte nichts funktionieren kannst du mich auf Discord anschreiben: littleclone (erwähne dein fehler beim anschreiben)");
                    Console.ResetColor();
                    Console.WriteLine("Versuch nochmal das Spiel zu speichern.[1]");
                    Console.WriteLine("Lade den letzten Spielstand.[2]");
                    Console.WriteLine("Starte ein neues Spiel.[3]");
                    Console.WriteLine("Verlasse das Game.[4]");
                    while (true && failedToSave)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Eingabe:");
                        Console.ResetColor();
                        message = Console.ReadLine().Trim();
                        switch (message)
                        {
                            case "1":
                                SaveData();
                                break;
                            case "2":
                                DataManager.RestoreBackupFiles();
                                currentSession = false;
                                LoadSaveFile();
                                NewDayStart(23);
                                break;
                            case "3":
                                DataManager.DeleteSaveFiles();
                                failedToSave = false;
                                Start();
                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Nutz einer der Möglichen Optionen!");
                                continue;
                        }
                        break;
                    }
                }
            }
            if (failedToSaveWorld)
            {
                while (true && failedToSaveWorld)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Sicherheits Menü!");
                    Console.WriteLine("Deine Welt konnte NICHT gespeichert werden! Deshalb wurde das Sicherheits Menü geöffnet.");
                    Console.WriteLine("Du hast nun folgende Möglichkeiten die du machen kannst:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("HINWEIS: Schließe das Game NICHT außer keines der unten gennanten Möglichkeiten Funktioniert.");
                    Console.WriteLine("Lies dir die Fehlermeldung durch.");
                    Console.WriteLine("Solltest du an der World File was verändert haben mach es Rückgängig!");
                    Console.WriteLine("Geh sicher das diese Anwendung in die files Schreiben kann (Schreib berechtigung).");
                    Console.WriteLine("Solltest du ein Antivirus haben der nicht Standart mäßig vom System kommt könnte dieser Falschen Alarm melden.");
                    Console.WriteLine("Sollte nichts funktionieren kannst du mich auf Discord anschreiben: littleclone (erwähne dein fehler beim anschreiben)");
                    Console.ResetColor();
                    Console.WriteLine("Versuch nochmal das Spiel zu speichern.[1]");
                    Console.WriteLine("Lade den letzten Spielstand.[2]");
                    Console.WriteLine("Starte ein neues Spiel.[3]");
                    Console.WriteLine("Verlasse das Game.[4]");
                    while (true && failedToSaveWorld)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Eingabe:");
                        Console.ResetColor();
                        message = Console.ReadLine().Trim();
                        switch (message)
                        {
                            case "1":
                                SaveData();
                                break;
                            case "2":
                                DataManager.RestoreBackupFiles();
                                currentSession = false;
                                LoadSaveFile();
                                NewDayStart(23);
                                break;
                            case "3":
                                DataManager.DeleteSaveFiles();
                                failedToSaveWorld = false;
                                Start();
                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Nutz einer der Möglichen Optionen!");
                                continue;
                        }
                        break;
                    }
                }
            }
            if (failedToLoad)
            {
                while (true && failedToLoad)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Sicherheits Menü!");
                    Console.WriteLine("Dein Spielstand konnte NICHT geladen werden! Deshalb wurde das Sicherheits Menü geöffnet.");
                    Console.WriteLine("Du hast nun folgende Möglichkeiten die du machen kannst:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("HINWEIS: Du kannst das Spiel normal Schließen und neustarten.");
                    Console.WriteLine("Lies dir die Fehlermeldung durch.");
                    Console.WriteLine("Solltest du an der Save File was verändert haben mach es Rückgängig!");
                    Console.WriteLine("Geh sicher das diese Anwendung in die files Lesen kann (Lese berechtigung).");
                    Console.WriteLine("Solltest du ein Antivirus haben der nicht Standart mäßig vom System kommt könnte dieser Falschen Alarm melden.");
                    Console.WriteLine("Sollte nichts funktionieren kannst du mich auf Discord anschreiben: littleclone (erwähne dein fehler beim anschreiben)");
                    Console.ResetColor();
                    Console.WriteLine("Versuch nochmal das Spiel zu laden.[1]");
                    Console.WriteLine("Lade die Backups.[2]");
                    Console.WriteLine("Starte ein neues Spiel.[3]");
                    Console.WriteLine("Verlasse das Game.[4]");
                    while (true && failedToLoad)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Eingabe:");
                        Console.ResetColor();
                        message = Console.ReadLine().Trim();
                        switch (message)
                        {
                            case "1":
                                LoadSaveFile();
                                break;
                            case "2":
                                DataManager.RestoreBackupFiles();
                                currentSession = false;
                                LoadSaveFile();
                                NewDayStart(23);
                                break;
                            case "3":
                                DataManager.DeleteSaveFiles();
                                failedToLoad = false;
                                Start();
                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Nutz einer der Möglichen Optionen!");
                                continue;
                        }
                        break;
                    }
                }
            }
            if (failedToLoadWorld)
            {
                while (true && failedToLoadWorld)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Sicherheits Menü!");
                    Console.WriteLine("Deine Welt konnte NICHT geladen werden! Deshalb wurde das Sicherheits Menü geöffnet.");
                    Console.WriteLine("Du hast nun folgende Möglichkeiten die du machen kannst:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("HINWEIS: Du kannst das Spiel normal Schließen und neustarten.");
                    Console.WriteLine("Lies dir die Fehlermeldung durch.");
                    Console.WriteLine("Solltest du an der World File was verändert haben mach es Rückgängig!");
                    Console.WriteLine("Geh sicher das diese Anwendung in die files Lesen kann (Lese berechtigung).");
                    Console.WriteLine("Solltest du ein Antivirus haben der nicht Standart mäßig vom System kommt könnte dieser Falschen Alarm melden.");
                    Console.WriteLine("Sollte nichts funktionieren kannst du mich auf Discord anschreiben: littleclone (erwähne dein fehler beim anschreiben)");
                    Console.ResetColor();
                    Console.WriteLine("Versuch nochmal das Spiel zu laden.[1]");
                    Console.WriteLine("Lade die Backups.[2]");
                    Console.WriteLine("Starte ein neues Spiel.[3]");
                    Console.WriteLine("Verlasse das Game.[4]");
                    while (true && failedToLoadWorld)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Eingabe:");
                        Console.ResetColor();
                        message = Console.ReadLine().Trim();
                        switch (message)
                        {
                            case "1":
                                LoadSaveFile();
                                break;
                            case "2":
                                DataManager.RestoreBackupFiles();
                                currentSession = false;
                                LoadSaveFile();
                                NewDayStart(23);
                                break;
                            case "3":
                                DataManager.DeleteSaveFiles();
                                failedToLoadWorld = false;
                                Start();
                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Nutz einer der Möglichen Optionen!");
                                continue;
                        }
                        break;
                    }
                }
            }
            if (!spaceShip || !spaces || !worlds || !actions || !passivs || !inv || !qsystem || !quests || !mainPlayer || !sciences || !worldInit || !npcHanna || !npcLea)
            {
                while (true || !spaceShip || !spaces || !worlds || !actions || !passivs || !inv || !qsystem || !quests || !mainPlayer || !sciences || !worldInit || !npcHanna || !npcLea)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Sicherheits Menü!");
                    Console.WriteLine("Objekte konnten NICHT Initialisiert werden! Deshalb wurde das Sicherheits Menü geöffnet.");
                    Console.WriteLine("Du hast nun folgende Möglichkeiten die du machen kannst:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("HINWEIS: Du kannst das Spiel normal Schließen und neustarten.");
                    Console.WriteLine("Lies dir die Fehlermeldung durch.");
                    Console.WriteLine("Solltest du an der World File was verändert haben mach es Rückgängig!");
                    Console.WriteLine("Geh sicher das diese Anwendung in die files Schreiben kann (Schreibe berechtigung).");
                    Console.WriteLine("Solltest du ein Antivirus haben der nicht Standart mäßig vom System kommt könnte dieser Falschen Alarm melden.");
                    Console.WriteLine("Sollte nichts funktionieren kannst du mich auf Discord anschreiben: littleclone (erwähne dein fehler beim anschreiben)");
                    Console.ResetColor();
                    Console.WriteLine("Versuch nochmal die Objekte zu initialisieren.[1]");
                    Console.WriteLine("Verlasse das Game.[2] (Du musst dass Game neustarten)");
                    while (true || !spaceShip || !spaces || !worlds || !actions || !passivs || !inv || !qsystem || !quests || !mainPlayer || !sciences || !worldInit || !npcHanna || !npcLea)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Eingabe:");
                        Console.ResetColor();
                        message = Console.ReadLine().Trim();
                        switch (message)
                        {
                            case "1":
                                init_Objects = false;
                                initObjekts();
                                break;
                            case "2":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Nutz einer der Möglichen Optionen!");
                                continue;
                        }
                        break;
                    }
                }
            }
            if (fileManipulation)
            {
                while (true && fileManipulation)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Sicherheits Menü! (Unfinished)");
                    Console.WriteLine("Deine Save File wurde Manipuliert! Deshalb wurde das Sicherheits Menü geöffnet.");
                    Console.WriteLine("Du hast nun folgende Möglichkeiten die du machen kannst:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("HINWEIS: Du kannst das Spiel normal Schließen und neustarten.");
                    Console.WriteLine("Dies wird angezeigt wenn der User die File Manipuliert hat.");
                    Console.WriteLine("Mach deine Änderungen an der Save File Rückgängig! Sonst kann das Spiel nicht Laden.");
                    Console.WriteLine("Solltest du es nicht gewesen sein dann schau mal wer diese Datei bearbeitet haben könnte.");
                    Console.WriteLine("Sollte nichts funktionieren kannst du mich auf Discord anschreiben: littleclone (erwähne dein fehler beim anschreiben)");
                    Console.ResetColor();
                    Console.WriteLine("Versuch nochmal das Spiel zu speichern.[1]");
                    Console.WriteLine("Lade die Backups.[2]");
                    Console.WriteLine("Starte ein neues Spiel.[3]");
                    Console.WriteLine("Verlasse das Game.[4]");
                    while (true && fileManipulation)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Eingabe:");
                        Console.ResetColor();
                        message = Console.ReadLine().Trim();
                        switch (message)
                        {
                            case "1":
                                SaveData();
                                break;
                            case "2":
                                DataManager.RestoreBackupFiles();
                                currentSession = false;
                                LoadSaveFile();
                                NewDayStart(23);
                                break;
                            case "3":
                                DataManager.DeleteSaveFiles();
                                failedToSave = false;
                                Start();
                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Nutz einer der Möglichen Optionen!");
                                continue;
                        }
                        break;
                    }
                }
            }
        }
    }
}
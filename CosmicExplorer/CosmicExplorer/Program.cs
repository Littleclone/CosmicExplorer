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
                Console.WriteLine("Game Version: 0.2.2 Dev Phase, Deutsch / German");
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
                                ResetVar();
                                inventory.ResetVar();
                                hanna.ResetVar();
                                gfi.ResetVar();
                                lea.ResetVar();
                                supplier.ResetVar();
                                player.ResetVar();
                                questSystem.ResetVar();
                                science.ResetVar();
                                space.ResetVar();
                                shuttle.ResetVar();
                                world.ResetVar();
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
        private void SaveData()
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
        private void initObjekts()
        {
            //Erstmalige generierung der Welt und zuweisung von Objekten
            if (init_Objects == false)
            {
                DataManager.Init(this);
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
        private void introduction()
        {
            if (_save[0] == 0)
            {
                _save[0] = 1;
                string message = string.Empty;
            introduction:
                Console.WriteLine("\nBevor es Losgeht, in diesem Spiel bist du eine Forscherin namens Charlotte, du befindest dich auf einem Raumschiff und \nlebst da allein. Dieses Spiel ist rein auf Text basiert und der Kreativität von dir. Es wird für die nächste Aktion \nimmer auf Input gewartet (von der Tastatur), auch damit Texte weiter gehen.");
                Console.ReadKey();
                Console.WriteLine("\nCharlotte arbeitet beim Galaktischen Forschungsinstitut [GFI], dies ist eine Staatliche Organisation. " +
                    "\n[Mehr infos in Kommenden Updates].");
                Console.ReadKey();
                Console.WriteLine("\nWillkommen Charlotte, dies ist dein Erster Tag hier auf dem Schiff stimmts?\nKeine Sorge, ich werde dir eine kurze Einführung geben in die Systeme des schiffs\n");
                Console.ReadKey();
                Console.WriteLine("Wichtigste Info Vorab, es gibt ein Tag System, fast alle aktionen verbrauchen Energie (Darauf später zurück)\nund Zeit (Darauf später zurück)\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Es SPEICHERT IMMER ERST wenn ein NEUER Tag STARTET.\n");
                Console.ResetColor();
                Console.ReadKey();
                Console.WriteLine("Es gibt ein Energie System sowie Zeit System das bestimmt wieviel du am Tag machen kannst. Wenn es zu" +
                    "\nspät wird (23 Uhr) dann gehtst ins Bett und beendest dein Tag, aber keine sorge, die tage haben" +
                    "\nkeine große bedeutung bis auf den Realismus. Sollte dir die Energie ausgehen musst du ins Bett gehen.\n");
                Console.ReadKey();
                Console.WriteLine("In der Kommando Zentrale kannst du an die Konsole gehen wo du mit 'help' schauen kannst was für befehle du zur" +
                    "\nverfügung hast und wieviel energie diese verbrauchen. Außerdem gibt es auch ein Aktions System. Bei Aktiven Aktionen" +
                    "\nwerden immer passive aktionen ausgeführt, diese können auch Energie verbrauchen, diese kann man aber deaktivieren" +
                    "\n(derzeit gibt es nur ein passives system das du einstellen kannst und insgesamt gibt es zwei passive Systeme," +
                    "\nmehr dazu kannst du in der Konsole einsehen.)\n");
                Console.ReadKey();
                Console.WriteLine("Das System funktioniert so das du die Zahlen eingibst wo du hinwillst. So einfach ist das." +
                    "\nBeispiel: In die Kommando Zentrale gehen.[1]" +
                    "\nDann kommt dazu noch ein eingabe Feld wo du die Zahl eintippen kannst, hier wäre es 1 und wenn du dann" +
                    "\nenter drückts gehst du in die Kommando Zentrale.\n");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wichtige Info!!!:" +
                    "\nDie Welt in der du dich befindest ist ein 2D Array oder kurz gesagt sind Spalten und Zeilen, daher werden\n" +
                    "dir deine Position und die andere Objekte in X:1, Y:200 (Oder fürs Array [1,200])angezeigt aber keine sorge,\n" +
                    "du kannst einfach normale Koordiaten wie X:3500 und Y:6855 eingeben, diese werden automatisch übersetzt, aber\n" +
                    "hier die genauen infos: Die welt ist 1619x1619 Zellen groß, das heißt X:3500 und Y:6855 sind im array:\n" +
                    "X:3 und Y:379. Im übrigen, 1619x1619 sind insgesamt 2.621.440 Zellen." +
                    "\nDIESES SYSTEM KANN SICH ÄNDERN!!!\n");
                Console.ResetColor();
                Console.ReadKey();
                Console.WriteLine("Beachte: Dies ist ein Prototyp und die Story ist nicht so lang, für mich scheint alles\n" +
                    "selbst erklärend, solltest du aber probleme damit haben schreib mich auf Discord an:\n" +
                    "littleclone (Nenn dein grund direkt wenn du mir schreibst, ich gehe nicht auf Random nachrichten ein.)\n");
                Console.ReadKey();
                Console.WriteLine("Das erste was du gleich machen solltest wäre ins Kommando Zentrum gehen und die\n" +
                    "GFI zu kontaktieren. Du kannst dir aber auch natürlich alles vorher anschauen\n");
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
            if (time >= 23.0)
            {
                const bool WorldBREAKER = true;
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
                gfi.state = 2;
                shuttle.Bedroom(6);
            }
            else
            {
                return;
            }
        }
        private void ResetVar()
        {
            //Setz alle Variablen hier auf ihre standartwert wie oben initialisiert
            CurrentDay = 0;
            DayCounter = 0;
            Array.Fill(_save, 0);
            //Flags
            debug = false;
            dev = true;
            hardCore = false;
            //Kontroll Variablen
            //init_Objects = false;
            isWorld = false;
            needSave = false;
            newGame = true;
            currentSession = false;
            //Error Variablen
            fileManipulation = false;
            failedToSave = false;
            failedToSaveWorld = false;
            failedToLoad = false;
            failedToLoadWorld = false;
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
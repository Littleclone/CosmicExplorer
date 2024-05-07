using CosmicExplorer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class World
    {
        private Game game;
        private SpaceShuttle shuttle;
        private Space Space;
        private Activities actions;
        private PassivSystem passiv;
        private Inventory inventory;
        private QuestSystem questSystem;
        public int _playerX = 0;
        public int _playerY = 0;
        //players Old Position
        int d = 1;
        int c = 1;
        CellType[,] world = new CellType[1619, 1619]; //Eine 1619x1619 Welt oder 2.621.440 Zellen
        int[,] sepWorld = new int[1620, 1620]; // 2.624.400
        CourseType[,] arr1 = new CourseType[1620, 1620]; //Eine 1620x1620 oder 2.624.400 mögliche Zellen
        public enum CellType
        {
            Space,
            Player,
            Asteroid,
            Planet,
            Sun,
            Spaceship,
        }
        public enum CourseType
        {
            notWritten,
            Written,
        }
        public void Worlds(SpaceShuttle shuttle, Space space, Activities action, Game games, PassivSystem systems, Inventory inv, QuestSystem questS)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.Space = space;
            this.actions = action;
            this.passiv = systems;
            this.inventory = inv;
            this.questSystem = questS;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[World]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public void WorldGenerator()
        {
            //Counter for the World Generation Rules
            int sunCount = 0;
            int sunCount1 = 0;
            int planetCount = 0;
            int planetCount1 = 0;
            int asteroidCount = 0;
            int asteroidCount1 = 0;
            int spaceShipCount = 0;
            int spaceCount = 0;
            int spaceCount1 = 0;
            //Other things
            const int BREAKER = 25;
            int counter = 0;
            Random random = new Random();
            for (int x = 0; x < world.GetLength(0); x++)
            {
                for (int y = 0; y < world.GetLength(1); y++)
                {
                    int randomNumber = random.Next(1, 101); // Beispiel: Zufallszahl zwischen 1 und 100
                    if (x >= BREAKER && y >= BREAKER) //Lässt erst zu das die Regeln angewendet werden wenn genügend einträge vorhanden sind
                    {
                        //Quest Debug Thing
                        if (world[1,400] != CellType.Space || world[5, 475] != CellType.Space)
                        {
                            world[1, 400] = CellType.Space;
                            world[5, 475] = CellType.Space;
                        }
                        //Welten Generation Regeln
                        if (world[x - 1, y - 1] == CellType.Asteroid || world[x, y - 1] == CellType.Asteroid || world[x - 1, y] == CellType.Asteroid)
                        {
                            if (randomNumber <= 50) //Wenn neben dem Asteroid ein Asteroid ist, ist die Chance höher das ein weitere Spawnt.
                            {
                                world[x, y] = CellType.Asteroid;
                                asteroidCount++;
                                continue;
                            }
                        }
                        else if (world[x - 5, y - 5] == CellType.Planet || world[x, y - 5] == CellType.Planet || world[x - 5, y] == CellType.Planet)
                        {
                            if (randomNumber <= 40) //Wenn ein Planet in der Nähe ist, ist es wahrscheinlicher das dies ein Sonnsystem ist.
                            {
                                world[x, y] = CellType.Planet;
                                planetCount++;
                                continue;
                            }
                        }
                        else if (world[x - 2, y - 2] == CellType.Planet || world[x, y - 2] == CellType.Planet || world[x - 2, y] == CellType.Planet)
                        {
                            if (randomNumber <= 40) //Wenn ein Planet in der Nähe ist, ist es wahrscheinlicher das Raumschiffe in der Nähe sind.
                            {
                                world[x, y] = CellType.Spaceship;
                                spaceShipCount++;
                                continue;
                            }
                        }
                        else if (world[x - 5, y - 5] == CellType.Sun || world[x, y - 5] == CellType.Sun || world[x - 5, y] == CellType.Sun)
                        {
                            if (randomNumber <= 60) //Wenn ein Planet in der Nähe ist, ist es wahrscheinlicher das dies ein Sonnsystem ist.
                            {
                                world[x, y] = CellType.Planet;
                                planetCount++;
                                continue;
                            }
                        }
                        else if (world[x - 2, y - 2] == CellType.Sun || world[x, y - 2] == CellType.Sun || world[x - 2, y] == CellType.Sun)
                        {
                            if (randomNumber <= 10) //Wenn eine Sonne in der Nähe ist, ist es wahrscheinlicher das eine weitere Sonne dabei ist. (Ein Doppelstern System)
                            {
                                world[x, y] = CellType.Sun;
                                sunCount++;
                                continue;
                            }
                        }
                        else if (world[x - 1, y - 1] == CellType.Space || world[x, y - 1] == CellType.Space || world[x - 1, y] == CellType.Space)
                        {
                            if (randomNumber <= 60) //Wenn es leer ist, ist es wahrscheinlicher das dies Weltraum ist
                            {
                                world[x, y] = CellType.Space;
                                spaceCount++;
                                continue;
                            }
                        }
                    }
                    if (randomNumber <= 5)
                    {
                        world[x, y] = CellType.Sun;
                        sunCount1++;
                    }
                    else if (randomNumber <= 20) // Beispiel: 20% Wahrscheinlichkeit für Planeten
                    {
                        world[x, y] = CellType.Planet;
                        planetCount1++;
                    }
                    else if (randomNumber <= 40) // Beispiel: 40% Wahrscheinlichkeit für Asteroiden
                    {
                        world[x, y] = CellType.Asteroid;
                        asteroidCount1++;
                    }
                    else // Restliche Wahrscheinlichkeit für leere Zellen
                    {
                        world[x, y] = CellType.Space;
                        spaceCount1++;
                    }
                }
            }
            for (int e = 1; e < sepWorld.GetLength(0); e++)
            {
                for (int r = 0; r < sepWorld.GetLength(1); r++)
                {
                    counter++;
                    sepWorld[e, r] = counter;
                }
            }
            world[1, 1] = CellType.Player;
            FindCellsByType(CellType.Player, true);
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Welt generation: [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.WriteLine("Es gibt " + sunCount + " Sonnen[Regeln]");
                Console.WriteLine("Es gibt " + sunCount1 + " Sonnen");
                Console.WriteLine("Es gibt " + planetCount + " Planeten[Regeln]");
                Console.WriteLine("Es gibt " + planetCount1 + " Planeten");
                Console.WriteLine("Es gibt " + spaceShipCount + " Spaceships[Regeln]");
                Console.WriteLine("Es gibt " + asteroidCount + " Asteroid[Regeln]");
                Console.WriteLine("Es gibt " + asteroidCount1 + " Asteroid");
                Console.WriteLine("Es gibt " + spaceCount + " Space[Regeln]");
                Console.WriteLine("Es gibt " + spaceCount1 + " Space");
                Console.WriteLine("Welt generation abgeschlossen [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public void SaveWorld()
        {
            WorldDataManager.SaveWorldData(world);
        }
        public void LoadWorld()
        {
            try
            {
                CellType[,] loadedWorld = WorldDataManager.LoadWorldData<CellType>();
                world = loadedWorld;
                if (game.debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("World is Loaded! [NUR WÄHREND DES DEBUGS VISIBLE]");
                    Console.ResetColor();
                }
            }
            catch (InvalidDataException ex)
            {
                // Behandlung der InvalidDataException
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Fehler beim Laden der Welt-Daten: {ex.Message}");
                Console.ResetColor();
            }
            catch (FileNotFoundException ex)
            {
                // Behandlung der FileNotFoundException
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Fehler beim Laden der Welt-Daten: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                // Allgemeine Exception-Behandlung für alle anderen Fälle
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                Console.ResetColor();
            }
        }
        //Ab hier sind die Aktivitäten die irgendwas mit der Welt zu tun haben:
        public void Course(int X, int Y, bool antiCrash)
        {
            const int BREAKER = 1;
            //Kontroll Variablen
            int counter = 0;
            int xCord = -1;
            int yCord = -1;
            //Ist unnötig da nach der Weltengenierung der Player einmal gefunden wird, jedoch bleibt dies erstmal falls fehler entstehen sollten
            if (_playerX == 0 && _playerY == 0)
            {
                Console.WriteLine("Bitte schaue erst wo dein Standort ist[Error by Finding Player Position]");
                return;
            }
            //Übersetzt die Koordinaten (X und Y) in array einträge (Spalte und Zeile)
            for (int e = 0; e < 1620; e++)
            {
                for (int r = 0; r < 1620; r++)
                {
                    int p;
                    p = sepWorld[e, r];
                    if (p == X)
                    {
                        xCord = e;
                        if(game.debug)
                        {
                            Console.WriteLine(xCord + "xCord [Debug]");
                        }
                    }
                    int o;
                    o = sepWorld[e, r];
                    if (o == Y)
                    {
                        yCord = r;
                        if(game.debug)
                        {
                            Console.WriteLine(yCord + "yCord [Debug]");
                        }
                    }
                }
            }
            //Überprüft ob die Koordinaten umgerechnet werden konnten [Debug funktion immer aktiv]
            if (xCord == -1 || yCord == -1)
            {
                if (xCord == -1 && yCord == -1)
                {
                    Console.WriteLine("Koordinaten konnten nicht umgerechnet werden![Debug]");
                    return;
                }
                if (yCord == -1)
                {
                    Console.WriteLine("Y konnte nicht umgerechnet werden![Debug]");
                    return;
                }
                if (xCord == -1)
                {
                    Console.WriteLine("X konnte nicht umgerechnet werden![Debug]");
                    return;
                }
            }
            Console.WriteLine("Kurs wird berechnet");
            if (antiCrash)
            {
                int playerX;
                int playerY;
                if(xCord < _playerX)
                {
                    for (int k = _playerX; k >= xCord; k--)
                    {
                        if (yCord < _playerY)
                        {
                            for (int l = _playerY; l >= yCord; l--)
                            {
                                if (world[k, l] == CellType.Space)
                                {
                                    int z = k;
                                    int w = l;
                                    playerX = k;
                                    playerY = l;
                                    k -= playerX;
                                    l -= playerY;
                                    if (k <= BREAKER && l <= BREAKER)
                                    {
                                        arr1[z, w] = CourseType.Written;
                                        k = z;
                                        l = w;
                                    }
                                }
                                if (world[k, l] != CellType.Space)
                                {
                                    counter++;
                                }
                            }
                        }
                        else
                        {
                            for (int l = _playerY; l <= yCord; l++)
                            {
                                if (world[k, l] == CellType.Space)
                                {
                                    int z = k;
                                    int w = l;
                                    playerX = k;
                                    playerY = l;
                                    k -= playerX;
                                    l -= playerY;
                                    if (k <= BREAKER && l <= BREAKER)
                                    {
                                        arr1[z, w] = CourseType.Written;
                                        k = z;
                                        l = w;
                                    }
                                }
                                if (world[k, l] != CellType.Space)
                                {
                                    counter++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int k = _playerX; k <= xCord; k++)
                    {
                        if (yCord < _playerY)
                        {
                            for (int l = _playerY; l >= yCord; l--)
                            {
                                if (world[k, l] == CellType.Space)
                                {
                                    int z = k;
                                    int w = l;
                                    playerX = k;
                                    playerY = l;
                                    k -= playerX;
                                    l -= playerY;
                                    if (k <= BREAKER && l <= BREAKER)
                                    {
                                        arr1[z, w] = CourseType.Written;
                                        k = z;
                                        l = w;
                                    }
                                }
                                if (world[k, l] != CellType.Space)
                                {
                                    counter++;
                                }
                            }
                        }
                        else
                        {
                            for (int l = _playerY; l <= yCord; l++)
                            {
                                if (world[k, l] == CellType.Space)
                                {
                                    int z = k;
                                    int w = l;
                                    playerX = k;
                                    playerY = l;
                                    k -= playerX;
                                    l -= playerY;
                                    if (k <= BREAKER && l <= BREAKER)
                                    {
                                        arr1[z, w] = CourseType.Written;
                                        k = z;
                                        l = w;
                                    }
                                }
                                if (world[k, l] != CellType.Space)
                                {
                                    counter++;
                                }
                            }
                        }
                    }
                }
            }
            for (int p = 1; p <= xCord; p++)
            {
                for (int l = 1; l <= yCord; l++)
                {
                    if (arr1[p, l] == CourseType.Written)
                    {
                        world[p, l] = CellType.Player;
                        arr1[p, l] = CourseType.notWritten;
                        world[c, d] = CellType.Space;
                        //Console.WriteLine("c " + c + " d " + d + "[Debug]");
                        c = p;
                        d = l;
                    }
                }
            }
            FindCellsByType(CellType.Player, true);
            Console.WriteLine("Kurs wurde berechnet");
            if(game.debug)
            {
                Console.WriteLine("Kollisionen erkannt: " + counter + " [Debug]");
            }
            if (questSystem.QState[1] == 0)
            {
                if (world[1,400] == CellType.Player)
                {
                    questSystem.QState[1] = 1;
                }
            }
            passiv.ActionMaked();
        }
        public void ShowPosition()
        {
            FindCellsByType(CellType.Player, false);
        }
        public void Sonar()
        {
            const int BREAKER = 1;
            Console.WriteLine("Objekte gefunden bei: ");
            for (int i = _playerX-15; i <= _playerX+15; i++)
            {
                for (int j = _playerY-15; j <= _playerY+15; j++)
                {
                    if (i >= BREAKER && j >= BREAKER)
                    {
                        if (world[i, j] == CellType.Asteroid)
                        {
                            Console.Write(i + "|" + j + ", ");
                        }
                        else if (world[i, j] == CellType.Sun)
                        {
                            Console.Write(i + "|" + j + ", ");
                        }
                        else if (world[i, j] == CellType.Spaceship)
                        {
                            Console.Write(i + "|" + j + ", ");
                        }
                        else if (world[i, j] == CellType.Planet)
                        {
                            Console.Write(i + "|" + j + ", ");
                        }
                    }
                }
            }
            Console.WriteLine(" ");
        }
        public void ScannerNah()
        {
            const int BREAKER = 1;
            for (int i = _playerX - 5; i <= _playerX + 5; i++)
            {
                for (int j = _playerY - 5; j <= _playerY + 5; j++)
                {
                    if (i >= BREAKER && j >= BREAKER)
                    {
                        if (world[i, j] == CellType.Asteroid)
                        {
                            Console.WriteLine("Asteroid gefunden bei: " + i + ", " + j);
                        }
                        else if (world[i, j] == CellType.Sun)
                        {
                            Console.WriteLine("Sonne gefunden bei: " + i + ", " + j);
                        }
                        else if (world[i, j] == CellType.Spaceship)
                        {
                            Console.WriteLine("Raumschiff gefunden bei: " + i + ", " + j);
                        }
                        else if (world[i, j] == CellType.Planet)
                        {
                            Console.WriteLine("Planet gefunden bei: " + i + ", " + j);
                        }
                    }
                }
            }
            Console.WriteLine(" ");
        }
        public void ScannerFern(int X, int Y)
        {
            if(X >= 0 && X < world.GetLength(0) && Y >= 0 && Y < world.GetLength(1))
            {
                Console.WriteLine("Bei den angegeben Koordinaten befindet sich: " + world[X, Y]);
            }
            else
            {
                Console.WriteLine("Die eingegebenen Koordinaten liegen außerhalb des gültigen Bereichs. Der Vorgang wurde abgebrochen.\n[Die energie wird erstattet]");
                shuttle.Energy += 10;
            }
        }
        public void FindCellsByType(CellType targetType, bool silent)
        {
            for (int x = 0; x < world.GetLength(0); x++)
            {
                for (int y = 0; y < world.GetLength(1); y++)
                {
                    if(targetType == CellType.Player)
                    {
                        if (world[x, y] == targetType && silent == false)
                        {
                            //Braucht ein dekodierer um Spalte und Zeile in Koordinaten umzurechnen
                            if(game.debug)
                            {
                                Console.WriteLine("Deine Position: (" + x + ", " + y + ") [Debug]");
                            }
                            _playerX = x;
                            _playerY = y;
                            return;
                        }
                        else if (world[x, y] == targetType && silent)
                        {
                            _playerX = x;
                            _playerY = y;
                            return;
                        }
                    } else if(world[x, y] == targetType)
                    {
                        Console.WriteLine("Gefunden: Zelle mit Typ " + targetType + " an Position: (" + x + ", " + y + ")");
                        continue;
                    }
                }
            }
            Console.WriteLine("NotFound");
        }
        public void WorldQuest(int index, uint cordX, uint cordY, bool isPlayer)
        {
            if(isPlayer)
            {
                if (world[cordX, cordY] == CellType.Player)
                {
                    questSystem.QState[index] = 15;
                    Console.WriteLine("Quest abgeschlossen[Debug]");
                }
            }
        }
    }
}

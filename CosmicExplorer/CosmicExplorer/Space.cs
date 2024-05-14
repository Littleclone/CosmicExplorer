using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class Space
    {
        private World world;
        private Game game;
        private SpaceShuttle shuttle;
        private Activities actions;
        private PassivSystem passiv;
        private Inventory inventory;
        private OwnMath math;
        public void Space_(SpaceShuttle shuttle, Activities action, Game games, World world, PassivSystem systems, Inventory inv, OwnMath math)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            this.inventory = inv;
            this.math = math;
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Space]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public int currentTime;
        string message;
        //Space Suit
        public int spaceSuitHealth = 100;
        public int spaceSuitEnergy = 100;
        public float spaceSuitOxygen = 100f;
        //Shuttle Module
        public int SolarPanelHealth = 100;
        public int SolarPanelEfficiency = 20;//Percantage. Maybe moved later to a different thing
        public int AntennenHealth = 100;
        public void InSpace()
        {
            //Überprüft ob es bereits 23 Uhr ist
            game.NewDayStart(currentTime);
            Console.WriteLine("Du bist im Weltraum, was willst du machen? [Uhrzeit: " + currentTime + ":00] | [Restliche Energie im Weltraum Anzug: " + spaceSuitEnergy + "]");
            Console.WriteLine("Die Integrität des Raumschiffes ist bei: " + shuttle.Health + "%");
            Console.WriteLine("Repariere das Shuttle[1]");
            Console.WriteLine("Geh zu den Solar Panellen[2]");
            Console.WriteLine("Geh zu den Antennen[3]");
            Console.WriteLine("Geh zurück ins Shuttle[4]");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine();
                switch (message)
                {
                    case "1":
                        if (spaceSuitEnergy < 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                            Console.ResetColor();
                            continue;
                        }
                        if (shuttle.Health < 100)
                        {
                            currentTime++;
                            shuttle.Health += 5;
                            spaceSuitEnergy -= 10;
                            Console.WriteLine("Shuttle um 5 Lebenspunkte Repariert");
                        }
                        else
                        {
                            Console.WriteLine("Das Shuttle muss nicht Repariert werden");
                        }
                        continue;
                    case "2":
                        currentTime++;
                        Solar();
                        break;
                    case "3":
                        currentTime++;
                        Antennas();
                        break;
                    case "4":
                        currentTime++;
                        shuttle.currentTime = currentTime;
                        shuttle.Airlock();
                        break;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
            }
        }
        public void Solar()
        {
            game.NewDayStart(currentTime);
            Console.WriteLine("Du bist bei den Solar Panelle, was willst du machen? [Uhrzeit: " + currentTime + ":00] | [Restliche Energie im Weltraum Anzug: " + spaceSuitEnergy + "]");
            Console.WriteLine("Die Integrität der Solar Panelle ist bei: " + SolarPanelHealth + "%");
            Console.WriteLine("Zurück zur Tür[1]");
            Console.WriteLine("Zu den Antennen[2]");
            Console.WriteLine("Mehr ist noch nicht vorhanden");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine();
                switch (message)
                {
                    case "1":
                        currentTime++;
                        InSpace();
                        continue;
                    case "2":
                        currentTime++;
                        Antennas();
                        break;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
            }
        }
        public void Antennas()
        {
            game.NewDayStart(currentTime);
            Console.WriteLine("Du bist bei den Antennen, was willst du machen? [Uhrzeit: " + currentTime + ":00] | [Restliche Energie im Weltraum Anzug: " + spaceSuitEnergy + "]");
            Console.WriteLine("Die Integrität der Antennen ist bei: " + AntennenHealth + "%");
            Console.WriteLine("Zurück zur Tür[1]");
            Console.WriteLine("Zu den Solar Panellen[2]");
            Console.WriteLine("Mehr ist noch nicht vorhanden");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine();
                switch (message)
                {
                    case "1":
                        currentTime++;
                        InSpace();
                        continue;
                    case "2":
                        currentTime++;
                        Solar();
                        break;
                    default:
                        Console.WriteLine("Wähle einer der Nummern aus!");
                        continue;
                }
            }
        }
    }
}

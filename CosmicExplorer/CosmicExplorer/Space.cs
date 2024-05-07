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
        private Math math;
        public void Space_(SpaceShuttle shuttle, Activities action, Game games, World world, PassivSystem systems, Inventory inv, Math math)
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
        int day;
        string message;
        //Space Suit
        public int spaceSuitHealth = 100;
        public int spaceSuitEnergy = 100;
        public float spaceSuitOxygen = 100f;
        //Shuttle Module
        public int SolarPanelHealth = 100;
        public int SolarPanelEfficiency = 20;//Percantage
        public int AntennenHealth = 100;
        public void InSpace()
        {
            day = shuttle.day;
            //Überprüft ob es bereits 23 Uhr ist
            if (currentTime == 23)
            {
                game.NewDayStart(currentTime, day);
            }
            Console.WriteLine("Du bist im Weltraum, was willst du machen? [Uhrzeit: " + currentTime + ":00] | [Restliche Energie: " + spaceSuitEnergy + "]");
            Console.WriteLine("Repariere das Shuttle[1]");
            Console.WriteLine("Geh zu den Solar Panellen[2]");
            Console.WriteLine("Geh zu den Antennen[3]");
            Console.WriteLine("Geh zurück ins Shuttle[4]");
        WrongInput:
            Console.Write("Eingabe:");
            message = Console.ReadLine();
            switch (message)
            {
                case "1":
                    math.EnergyController(10, spaceSuitEnergy);
                    spaceSuitEnergy = math.x;
                    if (!math.result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                        Console.ResetColor();
                        goto WrongInput;
                    }
                    if (shuttle.Health < 100)
                    {
                        currentTime++;
                        shuttle.Health += 5;
                        Console.WriteLine("Shuttle um 5 Lebenspunkte Repariert");
                    }
                    else
                    {
                        Console.WriteLine("Das Shuttle muss nicht Repariert werden");
                    }
                    goto WrongInput;
                case "2":
                    math.EnergyController(5, spaceSuitEnergy);
                    spaceSuitEnergy = math.x;
                    if (!math.result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                        Console.ResetColor();
                        goto WrongInput;
                    }
                    currentTime++;
                    Solar();
                    break;
                case "3":
                    math.EnergyController(5, spaceSuitEnergy);
                    spaceSuitEnergy = math.x;
                    if (!math.result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                        Console.ResetColor();
                        goto WrongInput;
                    }
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
                    goto WrongInput;
            }
        }
        public void Solar()
        {
            Console.WriteLine("Du bist bei den Solar Panelle, was willst du machen? [Uhrzeit: " + currentTime + ":00] | [Restliche Energie: " + spaceSuitEnergy + "]");
            Console.WriteLine("Zurück zur Tür[1]");
            Console.WriteLine("Zu den Antennen[2]");
            Console.WriteLine("Mehr ist noch nicht vorhanden");
        WrongInput:
            Console.Write("Eingabe:");
            message = Console.ReadLine();
            switch (message)
            {
                case "1":
                    math.EnergyController(5, spaceSuitEnergy);
                    spaceSuitEnergy = math.x;
                    if (!math.result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                        Console.ResetColor();
                        goto WrongInput;
                    }
                    currentTime++;
                    InSpace();
                    goto WrongInput;
                case "2":
                    math.EnergyController(5, spaceSuitEnergy);
                    spaceSuitEnergy = math.x;
                    if (!math.result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                        Console.ResetColor();
                        goto WrongInput;
                    }
                    currentTime++;
                    Antennas();
                    break;
                default:
                    Console.WriteLine("Wähle einer der Nummern aus!");
                    goto WrongInput;
            }
        }
        public void Antennas()
        {
            Console.WriteLine("Du bist bei den Antennen, was willst du machen? [Uhrzeit: " + currentTime + ":00] | [Restliche Energie: " + spaceSuitEnergy + "]");
            Console.WriteLine("Zurück zur Tür[1]");
            Console.WriteLine("Zu den Solar Panellen[2]");
            Console.WriteLine("Mehr ist noch nicht vorhanden");
        WrongInput:
            Console.Write("Eingabe:");
            message = Console.ReadLine();
            switch (message)
            {
                case "1":
                    math.EnergyController(5, spaceSuitEnergy);
                    spaceSuitEnergy = math.x;
                    if (!math.result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                        Console.ResetColor();
                        goto WrongInput;
                    }
                    currentTime++;
                    InSpace();
                    goto WrongInput;
                case "2":
                    math.EnergyController(5, spaceSuitEnergy);
                    spaceSuitEnergy = math.x;
                    if (!math.result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du hast zu Wenig Energie um diese Aktion durchzuführen!");
                        Console.ResetColor();
                        goto WrongInput;
                    }
                    currentTime++;
                    Solar();
                    break;
                default:
                    Console.WriteLine("Wähle einer der Nummern aus!");
                    goto WrongInput;
            }
        }
    }
}

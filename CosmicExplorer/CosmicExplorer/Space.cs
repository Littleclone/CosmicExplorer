using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Copyright 2024 Littleclone

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

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
            game.spaces = true;
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
        public float SolarPanelEfficiency = 1.2f;//Multiplier for the Solar Panel Energy Add
        public float SolarPanelEnergyAdd = 10;
        int lastTime = 6; //Last Time the Solar Panels were used
        public int AntennenHealth = 100;
        public void ResetVar()
        {
            currentTime = 0;
            spaceSuitHealth = 100;
            spaceSuitEnergy = 100;
            spaceSuitOxygen = 100f;
            SolarPanelHealth = 100;
            SolarPanelEfficiency = 20;
            AntennenHealth = 100;
        }
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
                message = Console.ReadLine().Trim();
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
                            passiv.ActionMaked();
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
            Console.WriteLine("Du bist bei den Solar Panellen, was willst du machen? [Uhrzeit: " + currentTime + ":00] | [Restliche Energie im Weltraum Anzug: " + spaceSuitEnergy + "]");
            Console.WriteLine("Die Integrität der Solar Panelle ist bei: " + SolarPanelHealth + "%");
            Console.WriteLine("Zurück zur Tür[1]");
            Console.WriteLine("Zu den Antennen[2]");
            Console.WriteLine("Mehr ist noch nicht vorhanden");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Eingabe:");
                Console.ResetColor();
                message = Console.ReadLine().Trim();
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
                message = Console.ReadLine().Trim();
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

        // Specific Functions
        public void SolarPanel()
        {
            int tempTime = currentTime - lastTime;
            if (tempTime < 0)
            {
                Math.Abs(tempTime);
            }
            for (int i = 0; i <= tempTime; i++)
            {
                float tempEnergy = SolarPanelEnergyAdd;
                tempEnergy *= SolarPanelEfficiency;
                shuttle.Energy += Convert.ToInt32(tempEnergy);
            }
            lastTime = currentTime;
        }
    }
}

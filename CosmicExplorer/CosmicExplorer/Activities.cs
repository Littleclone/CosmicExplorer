using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmic_Explorer;

//Copyright 2024 Littleclone

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

namespace Cosmic_Explorer
{
    // Derzeit nicht genutzt, wird aber später eine Funktion kriegen.
    public class Activities
    {
        private World world;
        private Game game;
        private SpaceShuttle shuttle;
        private Space Space;
        private PassivSystem passiv;
        private Inventory inventory;
        public void Actions(SpaceShuttle shuttle, Space space, Game games, World world, PassivSystem systems, Inventory inv)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.Space = space;
            this.world = world;
            this.passiv = systems;
            this.inventory = inv;
            game.actions = true;
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Actions]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
    }
    public class PassivSystem
    {
        private World world;
        private Game game;
        private SpaceShuttle shuttle;
        private Space Space;
        private Activities actions;
        private Inventory inventory;
        private OwnMath math;
        private QuestSystem qSystem;
        public void Passiv(SpaceShuttle shuttle, Space space, Activities action, Game games, World world, Inventory inv, OwnMath math, QuestSystem qSystem)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.Space = space;
            this.actions = action;
            this.world = world;
            this.inventory = inv;
            this.math = math;
            this.qSystem = qSystem;
            game.passivs = true;
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[PassivSystems]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        //Wird aufgerufen wenn eine Aktion gemacht wird um die Passiven Systeme auszuführen
        public void ActionMaked()
        {
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Action Triggered [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
            //Triggered the Methode to update the Quests
            qSystem.QuestSystemUpdate();
            //Mindest Energy die ein Passiv System ausführen kann
            if (shuttle.Energy !< 5)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Es gibt zu wenig energie um die Passiven Systeme auszuführen!");
                Console.ResetColor();
                return;
            }
            SonarSystem();
        }
        public void SonarSystem()
        {
            if(shuttle.sonarActive)
            {
                if(shuttle.Energy !< 5)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Es gibt zu wenig energie um das Sonar auszuführen!");
                    Console.ResetColor();
                    return;
                }
                Console.WriteLine("Passiv System 'Sonar' is triggered");
                shuttle.Energy -= 5;
                world.Sonar();
            }
        }
    }
}

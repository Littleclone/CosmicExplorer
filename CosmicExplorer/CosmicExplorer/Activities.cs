using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmic_Explorer;


namespace Cosmic_Explorer
{
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
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[PassivSystems]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        //Wird aufgerufen wenn eine Aktion gemacht wird um die Passiv System auszuführen
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
                    Console.WriteLine("Test");
                    return;
                }
                Console.WriteLine("Passiv System 'Sonar' is triggered");
                shuttle.Energy -= 5;
                world.Sonar();
            }
        }
    }
}

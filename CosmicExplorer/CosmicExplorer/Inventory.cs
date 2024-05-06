using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class Inventory
    {
        private World world;
        private Game game;
        private SpaceShuttle shuttle;
        private Space space;
        private Activities actions;
        private PassivSystem passiv;
        public void InvInit(SpaceShuttle shuttle, Space space, Activities action, Game games, World world, PassivSystem systems)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.space = space;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            if (game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Inventory]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();

            }
        }
    }
}
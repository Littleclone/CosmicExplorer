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
        public void Space_(SpaceShuttle shuttle, Activities action, Game games, World world, PassivSystem systems, Inventory inv)
        {
            this.game = games;
            this.shuttle = shuttle;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            this.inventory = inv;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Space]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
    }
}

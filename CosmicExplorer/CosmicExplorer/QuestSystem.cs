using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class QuestSystem
    {
        private Quest quest;
        private World world;
        private Game game;
        private SpaceShuttle shuttle;
        private Space Space;
        private Activities actions;
        private PassivSystem passiv;
        private Inventory inventory;
        private Math math;
        public void QuestSystemInit(Quest quest, Space space, Activities action, Game games, World world, PassivSystem systems, Inventory inv, Math math) 
        {
            this.game = games;
            this.Space = space;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            this.inventory = inv;
            this.math = math;
            this.quest = quest;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[QuestSystem]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public byte[] QState = new byte[100]; //Saved the Progress / State from the Quest
        public string message = "error";
        public uint x;
        public uint y;
        public void Quest()
        {
            for (int i = 1; i < 69; i++)
            {
                if (QState[i] != 0 && QState[i] != 15)
                {
                    string message = quest.QuestInfos(i);
                    if (message != "QuestID not found")
                    {
                        Console.WriteLine(message);
                    }
                    else
                    {
                        //Here it will be Quest ID not Found and i need this info for Debugging in the future
                        Console.WriteLine(message);
                        break;
                    }
                }
            }
        }
        //Updated the Quest Progress / State
        public void QuestSystemUpdate()
        {
            for (int index = 0; index < 69; index++)
            {
                if (QState[index] != 0 && QState[index] != 15)
                {
                    quest.QuestGoal(index, QState[index]);
                    //World
                    if (index == 1)
                    {
                        world.WorldQuest(index, x, y, true);
                    }
                }
            }
        }
    }
}

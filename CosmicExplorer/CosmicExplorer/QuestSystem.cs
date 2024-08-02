using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private OwnMath math;
        private Science science;
        private Player player;
        public void QuestSystemInit(Quest quest, Space space, Activities action, Game games, World world, PassivSystem systems, Inventory inv, OwnMath math, Science science, Player player) 
        {
            this.game = games;
            this.Space = space;
            this.actions = action;
            this.world = world;
            this.passiv = systems;
            this.inventory = inv;
            this.math = math;
            this.quest = quest;
            this.science = science;
            this.player = player;
            game.qsystem = true;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[QuestSystem]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        public sbyte[] QState = new sbyte[100]; //Saved the Progress / State from the Quest
        public string message = "error";
        public uint x; // Player Position X
        public uint y; // Player Position Y
        public void ResetVar()
        {
            x = 0;
            y = 0;
            Array.Fill<sbyte>(QState, 0);
        }
        public void Quest() // Hier werden die Quests angezeigt bei der Kommando Console
        {
            for (int i = 1; i < 69; i++)
            {
                if (QState[i] != 0 && QState[i] != 15)
                {
                    string message = quest.QuestInfos(i, QState);
                    if (message != "QuestID not found")
                    {
                        Console.WriteLine("----------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(message);
                        Console.ResetColor();
                    }
                    else
                    {
                        //Here it will be Quest ID not Found and i need this info for Debugging in the future
                        Console.WriteLine(message);
                        break;
                    }
                }
            }
            Console.WriteLine("----------------");
        }
        //Updated the Quest Progress / State
        public void QuestSystemUpdate() // Hier wird die Quest überprüft und geupdatet
        {
            for (int index = 0; index < 69; index++) // Wird irgendwann verändert
            {
                if (QState[index] != 0 && QState[index] != 15) // Macht so das nur Quests angezeigt werden die schon begonnen sind
                {
                    quest.QuestGoal(index, QState[index]);
                    //World
                    if (index == 1)
                    {
                        if (QState[1] == 1)
                        {
                            world.WorldQuest(index, x, y, true);
                        }
                        if (QState[1] == 3)
                        {
                            if (science.progress[3] == 2 && science.progress[4] == 2)
                            {
                                QState[1] = 4;
                            }
                        }
                    }
                    //NPC Quests
                    if(index == 2)
                    {
                        if (science.progress[1] == 3)
                        {
                            QState[2] = 2;
                        }
                    }
                    if (index == 3)
                    {
                        if(QState[3] == 1)
                        {
                            if(player.gold >= 3000)
                            {
                                QState[3] = 2;
                            }
                        }
                        if (QState[3] == 3)
                        {
                            if (inventory.itemIndex[13] >= 5)
                            {
                                QState[3] = 4;
                            }
                        }
                    }
                }
            }
        }
    }
}

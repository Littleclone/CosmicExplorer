using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public class Quest
    {
        private QuestSystem QSystem;
        private Game game;
        public void QuestInit(QuestSystem quest, Game game)
        {
            this.QSystem = quest;
            this.game = game;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Quest]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        string message = "error";
        public string QuestInfos(int QuestID)
        {
            if(QuestID == 1)
            {
                message = "Name: Test Quest\nAufgabe: Gehe zu 5, 475 [DEBUG QUEST]";
                return message;
            }
            return message = "QuestID not found";
        }
        public void QuestGoal(int QuestID, byte counter)
        {
            if(QuestID == 1)
            {
                if(counter == 1)
                {
                    QSystem.x = 5;
                    QSystem.y = 475;
                }
            }
        }
    }
}

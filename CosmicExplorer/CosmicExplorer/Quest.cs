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
            game.quests = true;
            if(game.debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekte Initalisiert[Quest]. [NUR WÄHREND DES DEBUGS VISIBLE]");
                Console.ResetColor();
            }
        }
        string message = "error";
        public string QuestInfos(int QuestID, sbyte[] state)
        {
            if(QuestID == 1)
            {
                if (state[1] == 1)
                {
                    message = "Name: Die ersten Proben\nAufgabe: Gehe zu den Koordinaten X:2800, Y:3000." +
                    "\nBeschreibung: Du musst dir deine ersten Proben zum erforschen von einem Lieferanten des Galaktische Forschungsinstitut \n(GFI) holen.";
                    return message;
                }
                if (state[1] == 2)
                {
                    message = "Name: Die ersten Proben\nAufgabe: Sprich mit dem Lieferanten. (Call befehl in der Konsole)" +
                    "\nBeschreibung: Du musst dir deine ersten Proben zum erforschen von einem Lieferanten des Galaktische Forschungsinstitut \n(GFI) holen.";
                    return message;
                }
                if (state[1] == 3)
                {
                    message = "Name: Die ersten Proben\nAufgabe: Erforsche die Proben komplett. (Im Labor)" +
                    "\nBeschreibung: Du musst dir deine ersten Proben zum erforschen von einem Lieferanten des Galaktische Forschungsinstitut \n(GFI) holen.";
                    return message;
                }
                if (state[1] == 4)
                {
                    message = "Name: Die ersten Proben\nAufgabe: Nehme wieder Kontakt zur GFI auf." +
                    "\nBeschreibung: Du musst dir deine ersten Proben zum erforschen von einem Lieferanten des Galaktische Forschungsinstitut \n(GFI) holen.";
                    return message;
                }
            }
            if(QuestID == 2)
            {
                if (state[2] == 1)
                {
                    message = "Name: Helfe Lea\nAufgabe: Erforsche Treibstoff komplett." +
                     "\nBeschreibung: Lea hat dich gefragt ob du vielleicht herausfinden kannst wie sie mit Weniger Benzin die gleiche Strecke\nFliegen kann.";
                    return message;
                }
                if (state[2] == 2)
                {
                    message = "Name: Helfe Lea\nAufgabe: Rede mit Lea." +
                     "\nBeschreibung: Lea hat dich gefragt ob du vielleicht herausfinden kannst wie sie mit Weniger Benzin die gleiche Strecke\nFliegen kann.";
                    return message;
                }
            }
            if(QuestID == 3)
            {
                if (state[3] == 1)
                {

                }
            }
            return message = "QuestID not found";
        }
        public void QuestGoal(int QuestID, sbyte counter)
        {
            if(QuestID == 1)
            {
                if(counter == 1)
                {
                    QSystem.x = 2;
                    QSystem.y = 1381;
                }
            }
        }
    }
}

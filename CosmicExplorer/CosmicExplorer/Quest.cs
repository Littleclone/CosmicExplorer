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
        public string QuestInfos(int QuestID, sbyte[] state) // Hier wird die Quest beschreibung festgelegt
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
                    message = "Name: Besorg die mehr Gold\nAufgabe: Krieg 3000 Gold." +
                    "\nBeschreibung: Die GFI verlangt das du 3000 Gold besitzt, wahrscheinlich für eine neue mission, verkaufe dafür sachen" +
                    "\nbei Lea (in dem du Asteroiden abbaust) oder warte auf dein nächstes Gehalt.";
                    return message;
                }
                if (state[3] == 2)
                {
                    message = "Name: Du hast genügend Gold\nAufgabe: Sprich mit der GFI." +
                    "\nBeschreibung: Du hast nun genügend Gold und sollst mit der GFI sprechen nun für die nächste Mission";
                    return message;
                }
                if (state[3] == 3)
                {
                    message = "Name: Ein neues Experiment\nAufgabe: Stelle Experiemente Legierung Her." +
                    "\nBeschreibung: Die GFI will das du Experiemente Legierung herstellst, mehr haben sie dazu nicht gesagt.";
                    return message;
                }
                if (state[3] == 4)
                {
                    message = "Name: Du hast genügend Legierungen\nAufgabe: Sprich mit der GFI." +
                    "\nBeschreibung: Du hast nun genügend Legierungen und sollst mit der GFI sprechen nun für die nächste Mission";
                    return message;
                }
                if (state[3] == 5)
                {
                    message = "Name: Ende der Story Mission\nAufgabe: Hab Spaß." +
                    "\nBeschreibung: Dies ist das Ende, mehr gibts noch nicht.";
                    return message;
                }
            }
            return message = "QuestID not found";
        }
        public void QuestGoal(int QuestID, sbyte counter) // Hier wird das Ziel der Quest festgelegt... hin und wieder mal :O
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

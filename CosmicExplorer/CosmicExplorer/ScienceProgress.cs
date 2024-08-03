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
    public static class ScienceProgress // Hier werden die Texte für die Wissenschaftlichen Fortschritte gespeichert und ausgegeben.
    {
        private static string? message;
        public static string ScienceProg(int ID, sbyte progress)
        {
            if(ID > 0)
            {
                if(ID == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if(progress == 0)
                    {
                        return message = "\nDu hast Treibstoff etwas erforscht und interessante erkentnisse bekommen.\n";
                    }
                    if (progress == 1)
                    {
                        return message = "\nDu hast Treibstoff weiter erforscht und hast interessante erkentnisse gemacht.\n";
                    }
                    if (progress == 2)
                    {
                        return message = "\nDu hast Treibstoff weiter erforscht, du denkst das du vielleicht damit was interessantes machen kannst.\n";
                    }
                    if (progress == 3)
                    {
                        return message = "\nDu hast Treibstoff komplett erforscht, du hast herausgefunden wie du mit weniger verbrauch weiter mit deinem Shuttle kommst.\n";
                    }
                }
                if (ID == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (progress == 0)
                    {
                        return message = "\nAls du das Asteroiden Stück untersucht hast hast du mehr darüber herausgefunden.\n";
                    }
                    if (progress == 1)
                    {
                        return message = "\nDu hast das Asteroiden Stück weiter erforscht und hast interessante erkentnisse gemacht.\n";
                    }
                    if (progress == 2)
                    {
                        return message = "\nDu hast das Asteroiden Stück weiter erforscht, du denkst das du vielleicht damit mehr materalieren kriegen kannst.\n";
                    }
                    if (progress == 3)
                    {
                        return message = "\nDu hast das Asteroiden Stück komplett erforscht, du hast herausgefunden das du dies schmelzen kannst um erze zu bekommen.\n";
                    }
                }
                if (ID == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (progress == 0)
                    {
                        return message = "\nDu hast das Glas mit Algen genauer untersucht und dir die Tiere darin notiert.\n";
                    }
                    if (progress == 1)
                    {
                        return message = "\nDu hast herausgefunden wie sich diese Tiere im Algen Gals im Welt All verändern.\n";
                    }
                }
                if (ID == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (progress == 0)
                    {
                        return message = "\nDu hast die Erde genauer untersucht und vielleicht Organische Stoffe gefunden.\n";
                    }
                    if (progress == 1)
                    {
                        return message = "\nDu hast herausgefunden das sich in der Erde Tatsächlich Organische Stoffe befinden.\n";
                    }
                }
                if (ID == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (progress == 0)
                    {
                        return message = "\nDu hast die Legierung untersucht und erste erkenntnisse gewonnen.\n";
                    }
                    if (progress == 1)
                    {
                        return message = "\nDu hast herausgefunden das die Legierung ziemlich Hitze Resistenz ist.\n";
                    }
                    if (progress == 2)
                    {
                        return message = "\nWeitere Tests bestätigen die Hitze Resistenz, aber da ist noch was.\n";
                    }
                    if (progress == 3)
                    {
                        return message = "\nDu hast herausgefunden das die Legierung auch noch Wärme Isolierend ist." +
                            "\nDies könnte gut für den Generator in deinem Raumschiff sein.\n";
                    }
                }
                //if (ID == 5) Interresannte idee von CoPilot
                //{
                //    Console.ForegroundColor = ConsoleColor.Cyan;
                //    if (progress == 0)
                //    {
                //        return message = "\nDu hast die Sonne genauer untersucht und vielleicht etwas interessantes gefunden.\n";
                //    }
                //    if (progress == 1)
                //    {
                //        return message = "\nDu hast herausgefunden das die Sonne mehr als nur ein Stern ist.\n";
                //    }
                //}
            }
            return message = "error (Code)";
        }
    }
}

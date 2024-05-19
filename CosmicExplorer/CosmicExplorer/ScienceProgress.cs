using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public static class ScienceProgress
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
            }
            return message = "error (Code)";
        }
    }
}

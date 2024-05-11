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
                        return message = "\nDu hast Benzin etwas erforscht und interessante erkentnisse bekommen.\n";
                    }
                    if (progress == 1)
                    {
                        return message = "\nDu hast Benzin weiter erforscht und hast interessante erkentnisse gemacht.\n";
                    }
                    if (progress == 2)
                    {
                        return message = "\nDu hast Benzin weiter erforscht, du denkst das du vielleicht damit was interessantes machen kannst.\n";
                    }
                    if (progress == 3)
                    {
                        return message = "\nDu hast Benzin komplett erforscht, du hast herausgefunden wie du mit weniger verbrauch weiter mit deinem Shuttle kommst.\n";
                    }
                }
            }
            return message;
        }
    }
}

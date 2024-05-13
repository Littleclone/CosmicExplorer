using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmic_Explorer
{
    public static class StringHandler
    {
        private static string? message;
        public static string NPCMessages(string npcName, int npcID, int messageID)
        {
            if(npcID > 0)
            {
                //Hanna (me)
                if(npcID == 1)
                {
                    if (messageID == 1)
                    {
                        return message = npcName + ": \nDies ist ein Test NPC!";
                    }
                    if(messageID == 2)
                    {
                        return message = npcName + ": \nDieser NPC Representiert die Entwicklerin von diesem Spiel und dient zu Debug zwecken.\n" +
                            "Dieser NPC wird nicht im normalem spiel zu finden sein!";
                    }
                    return message = "Message ID not found";
                }
                //Lea
                if(npcID == 2)
                {
                    //Erste Quest startet
                    if (messageID == 1)
                    {
                        return message = npcName + ": \nOh, Hallo Charlotte, lange nicht gesehen. Bist du immer noch am Forschen?";
                    }
                    if (messageID == 2)
                    {
                        return message = "Charlotte: \nOh, Hi Lea. Ja, ich bin immer noch dabei zu forschen und kann seit kurzem auch Endlich im Welt All Forschen." +
                            "\nWas machst du eigentlich derzeit?";
                    }
                    if (messageID == 3)
                    {
                        return message = npcName + ": \nStimmt, du hast ja dein Raumschiff erst seit kurzem. Ja ich bin immer noch als Händlerin unterwegs\n" +
                            "und verkaufe meine sachen. Übrigens, du bist ja Wissenschaftlerin, könntest du für mich herausfinden wie man Benzin \nbesser " +
                            "bzw. länger nutzen könnte?";
                    }
                    if (messageID == 4)
                    {
                        return message = "Charlotte: \nMeinst du das du weniger Benzin für gleiche Strecke verbrauchst?";
                    }
                    if (messageID == 5)
                    {
                        return message = npcName + ": \nJa, genau das meine ich, wenn du dies schaffst werde ich dir auch Rabatte für mein Shop geben.";
                    }
                    if (messageID == 6)
                    {
                        return message = "Charlotte: \nKlar, mach ich.";
                    }
                    if (messageID == 7)
                    {
                        return message = npcName + ": \nSupi, dankeschön, übrigens kannst du auch jetzt schon bei mir kaufen wenn du was brauchst.";
                    }
                    if (messageID == 8)
                    {
                        return message = npcName + ": \nHey Charlotte, was gibts?";
                    }
                    if (messageID == 9)
                    {
                        return message = "Charlotte: \nIch habe herausgefunden wie du mit weniger Benzin verbrauch die Gleiche Strecke fliegen kannst.";
                    }
                    if (messageID == 10)
                    {
                        return message = npcName + ": \nVielen Dank Charlotte, du bist meine Retterin.";
                    }
                    if(messageID == 11)
                    {
                        return message = "Charlotte hat Lea mehr über ihre kentnisse erzählt, Lea hat sie sich Notiert";
                    }
                    if (messageID == 12)
                    {
                        return message = npcName + ": \nNochmals Vielen Dank Charlotte, aus dank gebe ich dir 20% Rabat für einkaufe und 10% mehr wenn du mir was verkaufst.";
                    }
                    if (messageID == 13)
                    {
                        return message = "Charlotte: \nGeht das wirklich in Ordnung? Schließlich brauchst du doch auch Gold.";
                    }
                    if (messageID == 14)
                    {
                        return message = npcName + ": \nKeine Sorge, das Gold was ich durchs den kleineren Benzin verbrauch habe gleicht das aus, außerdem habe ich eine" +
                            "\ngroße Kundschaft, also wird dies nicht das problem sein.";
                    }
                    if (messageID == 15)
                    {
                        return message = "Charlotte: \nDann bedanke ich mich für dein Angebot.";
                    }
                    if (messageID == 16)
                    {
                        return message = "Lea's Preise für dich sind nun 20% weniger und sie kauft von dir für 10% mehr Gold.";
                    }
                    return message = "Message ID not found";
                }
            }
            return message = "NPC ID Not found!";
        }
    }
}

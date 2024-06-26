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
            if(npcID > -2)
            {
                //Hanna (me)
                if(npcID == -1)
                {
                    if (messageID == 1)
                    {
                        return message = npcName + ": \nDies ist ein Test NPC!";
                    }
                    if (messageID == 2)
                    {
                        return message = npcName + ": \nDieser NPC Representiert die Entwicklerin von diesem Spiel und dient zu Debug zwecken.\n" +
                            "Dieser NPC wird nicht im normalem spiel zu finden sein!";
                    }
                    return message = "Message ID not found";
                }
                //Galaktische Forschungsinstitut [GFI]
                if(npcID == 1)
                {
                    if (messageID == 1)
                    {
                        return message = npcName + ": \nHallo Charlotte, gut das wohl auf bist. Bist du bereit für deine erste aufgabe?";
                    }
                    if (messageID == 2)
                    {
                        return message = "Charlotte: \nJa, ich bin bereit für meine erste aufgabe.";
                    }
                    if (messageID == 3)
                    {
                        return message = npcName + ": \nGut, deine erste aufgabe wird es sein dich mit einer unserer Lieferanten zu treffen für deine ersten Proben.";
                    }
                    if (messageID == 4)
                    {
                        return message = "Charlotte: \nHeißt ich kriege meine ersten Proben von einem Lieferanten?";
                    }
                    if (messageID == 5)
                    {
                        return message = npcName + ": \nJa, du bist relativ nah und da du neu im Forschen im Weltraum bist, wären diese Proben ganz gut für dich.";
                    }
                    if (messageID == 6)
                    {
                        return message = "Charlotte: \nOkay, wo finde ich ihn?";
                    }
                    if (messageID == 7)
                    {
                        return message = npcName + ": \nDie Koordinaten sind X:2800, Y:3000. Du findest sie auch in deinem Logbuch. (Quests in der Console)";
                    }
                    if (messageID == 8)
                    {
                        return message = "Charlotte: \nOkay, habe ich mir notiert, ich melde mich dann wenn ich die Proben habe.";
                    }
                    if (messageID == 9)
                    {
                        return message = npcName + ": \nSuper. Bis dann und viel spaß im Weltraum.";
                    }
                    if (messageID == 10)
                    {
                        return message = npcName + ": \nWas gibts Charlotte?";
                    }
                    if (messageID == 11)
                    {
                        return message = "Charlotte: \nIch habe die Proben erforscht und habe die ergebnisse bereits zu ihnen geschickt.";
                    }
                    if (messageID == 12)
                    {
                        return message = npcName + ": \nSuper, das freut mich zu hören, wir melden uns wenn wir eine neue Aufgabe für dich haben.";
                    }
                    if (messageID == 13)
                    {
                        return message = "Charlotte: \nGut, bis dann.";
                    }
                    if (messageID == -3)
                    {
                        return message = "Das war das ende der Story Misson fürs erste, mehr kommt in weiteren Updates. Vergiss bitte nicht das dies ein Prototyp ist." +
                            "Es kommt darauf an wie viel interesse das Spiel erzeugt für neue Updates. Versteh mich nicht falsch, ich habe die \nzeit genossen " +
                            "die ich an diesem Spiel verbracht habe. Jedoch ist meine Lebens Situation etwas schwierig weshalb ich dann \nwenn dies auf geringes interesse" +
                            "stößt mich neuen sachen widmen werde um auch mehr über C# zu \nlernen, dennoch werde ich dieses Projekt nicht aufgeben.";
                    }
                    if (messageID == -3)
                    {
                        return message = "Danke fürs Spielen, sollte es dir gefallen habe dann komm auf mein Discord und berichte mir davon." +
                            "\nWie gesagt selbst wenn dies auf kleines interesse stößt werde ich immer wieder mal daran arbeiten, das nächste update" +
                            "\nsollte also nicht all zu lang auf sich warten lassen. :)" +
                            "\nJetzt hab spaß mit dem Rest der Welt und den Funktionen die es gibt, es gibt beispielsweise noch den NPC 'Lea'" +
                            "\nLea ist ab frühestens X:25 zu finden und sollte nicht all zu weit davon entfernt sein. (Nutz das Sonar)";
                    }
                    return message = "Message ID not found";
                }
                //Lea
                if(npcID == 2)
                {
                    //Erste Quest startet [2]
                    if (messageID == 1)
                    {
                        return message = npcName + ": \nOh, Hallo Charlotte, lange nicht gesehen. Bist du immer noch am Forschen?";
                    }
                    if (messageID == 2)
                    {
                        return message = "Charlotte: \nOh, Hi Lea. Ja, ich bin immer noch dabei zu forschen und kann seit kurzem auch Endlich im Welt All Forschen." +
                            "\nDa ich nun für das Galaktische Forschungsinstitut [GFI] im All Forsche. Was machst du eigentlich derzeit?";
                    }
                    if (messageID == 3)
                    {
                        return message = npcName + ": \nStimmt, du hast ja dein Raumschiff erst seit kurzem, aber schon erstaunlich das du jetzt schon" +
                            "\nim all Forschen darfst, aber naja, auch egal. Ich bin immer noch als Händlerin unterwegs und verkaufe meine sachen. " +
                            "\nÜbrigens, du bist ja Wissenschaftlerin, könntest du für mich herausfinden wie man Treibstoff besser bzw. länger nutzen " +
                            "\nkönnte?";
                    }
                    if (messageID == 4)
                    {
                        return message = "Charlotte: \nMeinst du das du weniger Treibstoff für gleiche Strecke verbrauchst?";
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
                    if (messageID == 8) //Quest abgeschlossen [2]
                    {
                        return message = npcName + ": \nHey Charlotte, was gibts?";
                    }
                    if (messageID == 9)
                    {
                        return message = "Charlotte: \nIch habe herausgefunden wie du mit weniger Treibstoff verbrauch die Gleiche Strecke fliegen kannst.";
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
                        return message = npcName + ": \nKeine Sorge, das Gold was ich durchs den kleineren Treibstoff verbrauch habe gleicht das aus, außerdem habe ich eine" +
                            "\ngroße Kundschaft, also wird dies nicht das problem sein.";
                    }
                    if (messageID == 15)
                    {
                        return message = "Charlotte: \nDann bedanke ich mich für dein Angebot.";
                    }
                    if (messageID == 16)
                    {
                        return message = "Lea's Preise für dich sind nun 20% weniger Gold und sie kauft von dir für 10% mehr Gold.";
                    }
                    return message = "Message ID not found";
                }
                //Den Lieferanten
                if(npcID == 3)
                {
                    if (messageID == 1)
                    {
                        return message = npcName + ": \nHi, bist du Charlotte from GFI?";
                    }
                    if (messageID == 2)
                    {
                        return message = "Charlotte: \nJa, das bin ich, dann musst du der Lieferant sein mit den Proben.";
                    }
                    if (messageID == 3)
                    {
                        return message = npcName + ": \nJa das bin ich, könnte ich trotzdem dein GFI Ausweis sehen?";
                    }
                    if (messageID == 4)
                    {
                        return message = "Charlotte: \nJa, hier.";
                    }
                    if (messageID == 5)
                    {
                        return message = "Charlotte zeigte ihr GFI Ausweis.";
                    }
                    if (messageID == 6)
                    {
                        return message = npcName + ": \nGut, hier sind die Proben.";
                    }
                    if (messageID == 7)
                    {
                        return message = "Der Lieferant brachte die Proben in dein Raumschiff.\nEs sind 2x Gläser mit Algen und 2x Erde von einem Fremden Planeten.";
                    }
                    if (messageID == 8)
                    {
                        return message = npcName + ": \nWenn du mich entschuldigst, ich muss weiter, habe heute ein strengen Zeitplan.";
                    }
                    if (messageID == 9)
                    {
                        return message = "Charlotte: \nAlles gut, bis zum Nächsten mal.";
                    }
                    if (messageID == 10)
                    {
                        return message = "Sie verabschiedeten sich.";
                    }
                }
            }
            return message = "NPC ID Not found!";
        }
    }
}

using System.Collections.Generic;
using MorseChat.MorseChat.Model;

namespace MorseChat.MorseChat.Util
{
    public class StringToMorseTextTranlator
    {
        public static MorseText  Translate(string morseMessage)
        {
            List<MorseElement> elements= new List<MorseElement>();
            for (int i = 0; i < morseMessage.Length; i++)
            {
                var traslatedElement = "";
                while (i < morseMessage.Length && morseMessage[i] != ' ')
                {
                    traslatedElement += morseMessage[i];
                    i++;
                }
                elements.Add(new MorseElement(traslatedElement));
            }
            return  new MorseText(elements);
        }
    }
}
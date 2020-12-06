using System;
using System.Collections.Generic;
using System.IO;
using MorseChat.MorseChat.Internal;
using MorseChat.MorseChat.Model;

namespace MorseChat.MorseChat.Service
{
    public class MorseEncodeService : IEncodeService<MorseText>

    {
        private readonly MorseToEngTranslator _translator;

        public MorseEncodeService(MorseToEngTranslator translator)
        {
            _translator = translator;
        }
        
        public MorseText Encode(string message)
        {
            var morseMessage = new List<MorseElement>();
            message = message.ToUpper();
            foreach (var letter in message)
            {
                var element = _translator.GetMorseSymbol(letter);
                if (element==null)
                {
                    throw new FormatException("Invalid symbol");

                }
                morseMessage.Add(element);
            }
            if (morseMessage.Capacity==0)
            {
                throw new InvalidDataException("Message is empty");
            }
            return  new MorseText(morseMessage);
        }
    }
}
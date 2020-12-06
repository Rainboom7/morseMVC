using System;
using System.Collections.Generic;
using MorseChat.MorseChat.Internal;
using MorseChat.MorseChat.Model;

namespace MorseChat.MorseChat.Service
{
    public class MorseDecodeService : IDecodeService<MorseText>
    {
        private readonly MorseToEngTranslator _translator;

        public MorseDecodeService(MorseToEngTranslator translator)
        {
            _translator = translator;
        }

        public string Decode(MorseText message)
        {
            var decodedMessage = "";
            foreach (var element in message.symbols)
            {
                decodedMessage += _translator.GetEngSymbol(element);
            }

            if (!"".Equals(decodedMessage))
                return decodedMessage;
            throw new FormatException("Message is empty");
        }
    }
}
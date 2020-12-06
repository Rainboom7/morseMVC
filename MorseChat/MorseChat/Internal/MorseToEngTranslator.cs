using System;
using System.Collections.Generic;
using System.Linq;
using MorseChat.MorseChat.Model;

namespace MorseChat.MorseChat.Internal
{
    public class MorseToEngTranslator
    {
        private readonly Dictionary<MorseElement, char> _morseEngDictionary;

        public MorseToEngTranslator()
        {
            _morseEngDictionary = new Dictionary<MorseElement, char>()
            {
                {new MorseElement(".-"), 'A'},
                {new MorseElement("-..."), 'B'},
                {new MorseElement("-.-."), 'C'},
                {new MorseElement("-.."), 'D'},
                {new MorseElement("."), 'E'},
                {new MorseElement("..-."), 'F'},
                {new MorseElement("--."), 'G'},
                {new MorseElement("...."), 'H'},
                {new MorseElement(".."), 'I'},
                {new MorseElement(".---"), 'J'},
                {new MorseElement("-.-"), 'K'},
                {new MorseElement(".-.."), 'L'},
                {new MorseElement("--"), 'M'},
                {new MorseElement("-."), 'N'},
                {new MorseElement("---"), 'O'},
                {new MorseElement(".--."), 'P'},
                {new MorseElement("--.-"), 'Q'},
                {new MorseElement(".-."), 'R'},
                {new MorseElement("..."), 'S'},
                {new MorseElement("-"), 'T'},
                {new MorseElement("..-"), 'U'},
                {new MorseElement("...-"), 'V'},
                {new MorseElement(".--"), 'W'},
                {new MorseElement("-..-"), 'X'},
                {new MorseElement("-.--"), 'Y'},
                {new MorseElement("--.."), 'Z'},
                {new MorseElement("-...-"), ' '}
            };
        }


        public Char GetEngSymbol(MorseElement alphabetElement)
        {
            char value;
            if (_morseEngDictionary.TryGetValue(alphabetElement, out value))
            {
                return value;
            }

            throw new FormatException("Symbol is undecodable");
        }

        public MorseElement GetMorseSymbol(char engSymbol)
        {
            var key = _morseEngDictionary
                .FirstOrDefault(eng => eng.Value == engSymbol).Key;
            return key;
        }
    }
}
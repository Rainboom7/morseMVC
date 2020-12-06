using System;
using System.Collections.Generic;
using MorseChat.MorseChat.Internal;
using MorseChat.MorseChat.Model;
using MorseChat.MorseChat.Service;
using MorseChat.MorseChat.Util;

namespace MorseChat.MorseChat
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Test2();
           // 	Test();
        }

        
        private static void Test2()
        {
            var wavFileHandler = new WavFileHandler();
            var text = ".- ---. . -";
            var message = StringToMorseTextTranlator.Translate(text);
            wavFileHandler.CreateFile("a", message);
            var decodedMessage = wavFileHandler.ReadFromFile("a.wav");
            Console.Write(string.Join(",", decodedMessage.ToString()));
            Console.WriteLine();
        }
        

        private static void Test()
        {
            var translator = new MorseToEngTranslator();
            var morseDecodeService = new MorseDecodeService(translator);
            var morseEncodeService = new MorseEncodeService(translator);
            var element=new MorseElement(".");
            var text = ".-. -.- .- ."; 
            var message = StringToMorseTextTranlator.Translate(text);
            Console.WriteLine(morseDecodeService.Decode(message));
            var encoded = morseEncodeService.Encode("Hello world");
            Console.Write(encoded);
        }
    }
}

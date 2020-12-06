
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MorseChat.MorseChat.Model;
using MorseChat.MorseChat.Util;

namespace MorseChat.MorseChat.Internal
{
    public class WavFileHandler : IFileHandler<MorseText>
    {
        private const int SHORT_BEEP = 500;
        private const int LONG_BEEP = 1000;
        private const int SILENCE = 1000;
        private const short SAMPLE_BITS = 16;
        private const int SAMPLE_RATE = 10000;

        private const int FORMAT_CHUNK_SIZE = 16;
        private const int WAVE_SIZE = 4;
        private const int HEADER_SIZE = 8;
        private const short FORMAT_TYPE = 1;
        private const short NUM_CHANNELS = 1;
        private const int AMPLITUDE = 10000;

        public void CreateFile(string filePath, MorseText morseMessage)
        {
            var shortBeeps = CountBeeps(morseMessage,'.');
            var longBeeps =  CountBeeps(morseMessage,'-');
            var silences = longBeeps + shortBeeps;
            var numSamples = shortBeeps * SHORT_BEEP + longBeeps * LONG_BEEP + silences * SILENCE;

            var frameSize = (short)(NUM_CHANNELS * (SAMPLE_BITS + 7) / 8);
            var dataChunkSize = numSamples * frameSize;
            var fileSize = WAVE_SIZE + HEADER_SIZE + FORMAT_CHUNK_SIZE + HEADER_SIZE + dataChunkSize;
            var bytesPerSecond = SAMPLE_RATE * frameSize;


            var f = new FileStream(filePath + ".wav", FileMode.Create);
            var wr = new BinaryWriter(f);

            wr.Write("RIFF".ToArray());
            wr.Write(fileSize);
            wr.Write("WAVE".ToArray());
            wr.Write("fmt ".ToArray());
            wr.Write(FORMAT_CHUNK_SIZE);
            wr.Write(FORMAT_TYPE);
            wr.Write(NUM_CHANNELS);
            wr.Write(SAMPLE_RATE);
            wr.Write(bytesPerSecond);
            wr.Write(frameSize);
            wr.Write(SAMPLE_BITS);
            wr.Write("data ".ToArray());
            wr.Write(dataChunkSize);
            foreach (var morseElement in morseMessage.symbols)
            {
                foreach (var element in morseElement.Value)
                {
                    if ('.'.Equals(element))
                    {
                        MakeSound(SHORT_BEEP, wr, 2*AMPLITUDE);
                        MakeSound(SILENCE, wr, 0);
                    }
                    else if ('-'.Equals(element))
                    {
                        MakeSound(LONG_BEEP, wr, AMPLITUDE);
                        MakeSound(SILENCE, wr, 0);
                    }
                }
                MakeSound(SILENCE, wr, 0);
            }

            wr.Close();
        }

        public MorseText ReadFromFile(string filepath)
        {
            var wav = File.ReadAllBytes(filepath);
            var dataBeginIndex = GetSoundBeginningIndex(wav);
            var decodedMessage = "";
            var beenSilent = false;
            for (var i = dataBeginIndex; i < wav.Length;)
            {
                switch (wav[i])
                {
                    case (AMPLITUDE & 0xff):
                    {
                        decodedMessage += '-';
                        i += 4 * LONG_BEEP;
                        beenSilent = false;
                        break;
                    }
                    case (2 * AMPLITUDE & 0xff):
                    {
                        decodedMessage += '.';
                        i += 4 * SHORT_BEEP;
                        beenSilent = false;
                        break;
                    }
                    case 0:
                    {
                        i += 4 * SILENCE;
                        if (beenSilent)
                            decodedMessage += " ";
                        beenSilent = true;
                        break;
                    }
                    default:
                    {
                        throw new FormatException("Wrong wav format");
                        break;
                    }
                }
            }
            var text = StringToMorseTextTranlator.Translate(decodedMessage);
            return text;
        }

        private int GetSoundBeginningIndex(byte[] wav)
        {
            var length = wav.Length;
            var dataBeginning = Encoding.ASCII.GetBytes("data");
            var dataIndexes = wav.Select((value, index) => new {value, index})
                .Where(pair => dataBeginning.Any(target => pair.value == target))
                .Select(pair => pair.index)
                .ToArray();
            var dataBeginIndex = 0;
            for (var i = dataIndexes.Length - 1; i >= 3; i--)
            {
                if (((dataIndexes[i] - dataIndexes[i - 1]) == 1) &&
                    ((dataIndexes[i - 1] - dataIndexes[i - 2]) == 1) &&
                    ((dataIndexes[i - 2] - dataIndexes[i - 3]) == 1))
                {
                    dataBeginIndex = dataIndexes[i];
                }
            }

            dataBeginIndex += 6; //skipping blank space and dataChunk size
            return dataBeginIndex;
        }

       
        private int CountBeeps(MorseText morseMessage,char value)
        {
            var count = 0;
            foreach (var element in morseMessage.symbols)
            {
                foreach (var character in element.Value)
                {
                    if (value.Equals(character))
                        count++;
                    
                }
            }

            return count;
        }

        private static void MakeSound(int length, BinaryWriter wr, int amplitude)
        {
            for (var i = 0; i < length; i++)
            {
                wr.Write(amplitude & 0xff);
            }
        }
    }
}


using System.Collections.Generic;

namespace MorseChat.MorseChat.Model
{
    public class MorseText
    {
        public MorseText(List<MorseElement> symbols)
        {
            this.symbols = symbols;
        }

        public List<MorseElement> symbols { get;  }
        public override string ToString()
        {
            string value = "";
            foreach (var element in symbols)
            {
                value += element.Value+ " ";

            }

            return value;
        }

        public override bool Equals(object? obj)
        {
            return obj != null && this.ToString().Equals(obj.ToString());
        }
    }
}
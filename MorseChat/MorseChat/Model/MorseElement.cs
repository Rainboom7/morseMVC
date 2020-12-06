using System.Collections.Generic;
using System.Linq;

namespace MorseChat.MorseChat.Model
{
	public class MorseElement
	{
		public string Value { get; set; }

		

		public MorseElement(string element)
		{
			this.Value = element;
		}

	

		public override string ToString()
		{
			return this.Value;
		}

		public void Clear()
		{
			this.Value="";
		}

		public override bool Equals(object obj)
		{
			// ReSharper disable once CheckForReferenceEqualityInstead.1
			if ((obj == null) || (this.GetType() != obj.GetType()))
				return false;
			else
			{
				var element = (MorseElement) obj;
				return this.Value.Equals(element.Value);
			}
		}

		public override int GetHashCode()
		{
			var hash = 19;
			foreach (var element in Value)
			{
				hash += hash * 31 + element.GetHashCode();
			}

			return hash;
		}
	}
}

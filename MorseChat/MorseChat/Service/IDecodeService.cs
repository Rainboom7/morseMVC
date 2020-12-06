using MorseChat.MorseChat.Model;

namespace MorseChat.MorseChat.Service
{
	public interface IDecodeService<T>
	{
		string Decode(T message);
	}
}

using System.Collections.Generic;
using MorseChat.MorseChat.Model;

namespace MorseChat.MorseChat.Service
{
    public interface IEncodeService<T>
    {
        T Encode(string message);
    }
}

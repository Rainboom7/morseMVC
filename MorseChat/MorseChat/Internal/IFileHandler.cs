using MorseChat.MorseChat.Model;

namespace MorseChat.MorseChat.Internal
{
    public interface IFileHandler<T>
    {
       T ReadFromFile(string filepath);
        void CreateFile(string filePath, T message);
    }
}

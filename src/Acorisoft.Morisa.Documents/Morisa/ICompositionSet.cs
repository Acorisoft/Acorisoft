using LiteDB;

namespace Acorisoft.Morisa
{
    public interface ICompositionSet
    {
        string FileName { get; }
        string Directory { get; }
    }
}
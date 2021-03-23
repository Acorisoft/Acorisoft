namespace Acorisoft.Morisa
{
    public interface ILoadContext
    {
        string Name { get; set; }
        string FileName { get; set; }
        string Directory { get; set; }
    }
}
using LiteDB;

namespace Acorisoft.Morisa.Map
{
    public sealed class BrushSet : DataSet<BrushSetProperty>, IBrushSet
    {
        internal LiteCollection<IBrush> DB_Brush { get; set; }
        internal LiteCollection<IBrushGroup> DB_Group { get; set; }
    }
}
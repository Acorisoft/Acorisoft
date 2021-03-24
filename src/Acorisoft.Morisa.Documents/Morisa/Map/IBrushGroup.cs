using System;

namespace Acorisoft.Morisa.Map
{
    public interface IBrushGroup
    {
        Guid Id { get; set; }
        Guid ParentId { get; set; }
        string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    public class Brush : IBrush
    {
        public int Id { get; set; }
        public Guid ParentId  { get; set; }
        public BrushMode Mode  { get; set; }
        public int RefId  { get; set; }
        public FillMode Left  { get; set; }
        public FillMode Right  { get; set; }
        public FillMode Top  { get; set; }
        public FillMode Bottom  { get; set; }
    }
}

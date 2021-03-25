using Acorisoft.Morisa.Map;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tools.Models
{
    public class BrushesGenerateContext : GenerateContext<IEnumerable<IGenerateContext<Brush>>>, IGenerateContext<IEnumerable<IGenerateContext<Brush>>>
    {
        private readonly List<IGenerateContext<IBrush>> _Brushes;

        public BrushesGenerateContext() : base(new List<IGenerateContext<Brush>>())
        {
            _Brushes = (List<IGenerateContext<IBrush>>)Context;
        }

        internal List<IGenerateContext<Brush>> Brushes => _Brushes;
        public Rgba32 LandColor { get; set; }
    }
}

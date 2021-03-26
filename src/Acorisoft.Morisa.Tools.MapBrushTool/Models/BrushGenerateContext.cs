using Acorisoft.Morisa.Map;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tools.Models
{
    public class BrushGenerateContext : GenerateContext<Brush>
    {
        public BrushGenerateContext() : base(new Brush())
        {

        }
        public Rgba32 LandColor { get; set; }
    }
}

using Acorisoft.Morisa.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tools.Models
{
    public class BrushSetGenerateContext : SaveContext<BrushSetProperty>
    {
        public BrushSetGenerateContext() : base(new BrushSetProperty())
        {

        }
    }
}

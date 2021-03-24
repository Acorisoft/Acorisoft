using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class GenerateContext<T> : IGenerateContext<T> where T : class
    {
        public GenerateContext(T property) => Context = property;

        public string Id { get; set; }
        public string FileName { get; set; }
        public string Directory { get; set; }
        public T Context { get; set; }
    }
}

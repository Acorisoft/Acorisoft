using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Composition
{
    /// <summary>
    /// <see cref="CompositionSet"/> 类型表示一个创作集。
    /// </summary>
    public class CompositionSet : DataSet<CompositionSetProperty>, ICompositionSet
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"IsDisposed = {IsDisposed}";
        }
    }
}

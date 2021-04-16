using System;
using System.Diagnostics;

namespace Acorisoft.Morisa.Composition
{
#pragma warning disable CA1816 

    /// <summary>
    /// <see cref="CompositionSetContext"/> 类型表示一个创作集上下文。
    /// </summary>
    [DebuggerDisplay("{Name} - {FileName}")]
    public class CompositionSetContext : ActivatingContext<CompositionSet, CompositionSetProperty>, ICompositionSetContext, IDisposable, IEquatable<CompositionSetContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="context"></param>
        public CompositionSetContext(CompositionSet cs, ILoadContext context) : base(cs, context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="context"></param>
        public CompositionSetContext(CompositionSet cs, ISaveContext context) : base(cs, context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!Activating.IsDisposed)
            {
                ((IDisposable)Activating).Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override sealed bool Equals(object obj)
        {
            if(obj is CompositionSetContext y)
            {
                return Equals(y);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override sealed int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), FileName?.GetHashCode() ?? 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override sealed string ToString()
        {
            return FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(CompositionSetContext y)
        {
            return y.FileName == FileName;
        }
    }
}
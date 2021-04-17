using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Acorisoft.Morisa.Composition
{

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
        /// 比较与指定对象之间的值等价性。
        /// </summary>
        /// <param name="y">指定要比较的对象。</param>
        /// <returns>如果指定的对象值等价性一致，则返回<see cref="true"/>否则返回<see cref="false"/></returns>
        public bool Equals([AllowNull]CompositionSetContext y)
        {
            if(y == null)
            {
                return false;
            }

            return y.FileName == FileName;
        }
    }
}
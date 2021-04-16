using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="DataFilter{TEntity1}"/> 类型表示一个支持8个数据实体过滤的过滤器。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    public abstract class DataFilter<TEntity1> : IDataFilter<TEntity1>
    {
        public virtual bool FilterEntity(TEntity1 entity)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// <see cref="DataFilter{TEntity1, TEntity2}"/> 类型表示一个支持8个数据实体过滤的过滤器。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    public abstract class DataFilter<TEntity1, TEntity2> :
        DataFilter<TEntity1>,
        IDataFilter<TEntity1, TEntity2>
    {
        public bool FilterEntity2(TEntity2 entity)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// <see cref="DataFilter{TEntity1, TEntity2, TEntity3}"/> 类型表示一个支持8个数据实体过滤的过滤器。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    public abstract class DataFilter<TEntity1, TEntity2, TEntity3> :
        DataFilter<TEntity1, TEntity2>,
        IDataFilter<TEntity1, TEntity2, TEntity3>
    {
        public bool FilterEntity3(TEntity3 entity)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// <see cref="DataFilter{TEntity1, TEntity2, TEntity3, TEntity4}"/> 类型表示一个支持8个数据实体过滤的过滤器。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    public abstract class DataFilter<TEntity1, TEntity2, TEntity3, TEntity4> :
        DataFilter<TEntity1, TEntity2, TEntity3>,
        IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4>
    {
        public bool FilterEntity4(TEntity4 entity)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// <see cref="DataFilter{TEntity1, TEntity2, TEntity3, TEntity4, TEntity5}"/> 类型表示一个支持8个数据实体过滤的过滤器。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    /// <typeparam name="TEntity5">需要过滤的第五个实体类型。</typeparam>
    public abstract class DataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5> :
        DataFilter<TEntity1, TEntity2, TEntity3, TEntity4>,
        IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>
    {
        public bool FilterEntity5(TEntity5 entity)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// <see cref="DataFilter{TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6}"/> 类型表示一个支持8个数据实体过滤的过滤器。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    /// <typeparam name="TEntity5">需要过滤的第五个实体类型。</typeparam>
    /// <typeparam name="TEntity6">需要过滤的第六个实体类型。</typeparam>
    public abstract class DataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> :
        DataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>,
        IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>
    {
        public bool FilterEntity6(TEntity6 entity)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// <see cref="DataFilter{TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7}"/> 类型表示一个支持8个数据实体过滤的过滤器。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    /// <typeparam name="TEntity5">需要过滤的第五个实体类型。</typeparam>
    /// <typeparam name="TEntity6">需要过滤的第六个实体类型。</typeparam>
    /// <typeparam name="TEntity7">需要过滤的第七个实体类型。</typeparam>
    public abstract class DataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> :
        DataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>,
        IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>
    {
        public bool FilterEntity7(TEntity7 entity)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// <see cref="DataFilter{TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8}"/> 类型表示一个支持8个数据实体过滤的过滤器。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    /// <typeparam name="TEntity5">需要过滤的第五个实体类型。</typeparam>
    /// <typeparam name="TEntity6">需要过滤的第六个实体类型。</typeparam>
    /// <typeparam name="TEntity7">需要过滤的第七个实体类型。</typeparam>
    /// <typeparam name="TEntity8">需要过滤的第八个实体类型。</typeparam>
    public abstract class DataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8> : 
        DataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> , 
        IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8>
    {
        public virtual bool FilterEntity8(TEntity8 entity)
        {
            throw new NotImplementedException();
        }
    }
}

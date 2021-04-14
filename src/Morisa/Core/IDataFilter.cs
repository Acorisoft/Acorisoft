using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="IDataFilter{TEntity}"/> 接口表示一个抽象的数据过滤器接口，用于为数据集提供数据过滤支持。
    /// </summary>
    /// <typeparam name="TEntity">需要过滤的实体类型。</typeparam>
    public interface IDataFilter<TEntity>
    {
        /// <summary>
        /// 指示当前实体是否需要过滤。
        /// </summary>
        /// <param name="entity">指定要过滤的实体。</param>
        /// <returns>如果不过滤则返回<see cref="true"/>,否则返回<see cref="false"/></returns>
        bool FilterEntity(TEntity entity);
    }

    /// <summary>
    /// <see cref="IDataFilter{TEntity1, TEntity2}"/> 接口表示一个抽象的数据过滤器接口，用于为数据集提供数据过滤支持。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    public interface IDataFilter<TEntity1, TEntity2> : IDataFilter<TEntity1>
    {
        /// <summary>
        /// 指示当前实体是否需要过滤。
        /// </summary>
        /// <param name="entity">指定要过滤的实体。</param>
        /// <returns>如果不过滤则返回<see cref="true"/>,否则返回<see cref="false"/></returns>
        bool FilterEntity2(TEntity2 entity);
    }

    /// <summary>
    /// <see cref="IDataFilter{TEntity1, TEntity2, TEntity3}"/> 接口表示一个抽象的数据过滤器接口，用于为数据集提供数据过滤支持。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    public interface IDataFilter<TEntity1, TEntity2, TEntity3> : IDataFilter<TEntity1, TEntity2>
    {
        /// <summary>
        /// 指示当前实体是否需要过滤。
        /// </summary>
        /// <param name="entity">指定要过滤的实体。</param>
        /// <returns>如果不过滤则返回<see cref="true"/>,否则返回<see cref="false"/></returns>
        bool FilterEntity3(TEntity3 entity);
    }

    /// <summary>
    /// <see cref="IDataFilter{TEntity1, TEntity2, TEntity3, TEntity4}"/> 接口表示一个抽象的数据过滤器接口，用于为数据集提供数据过滤支持。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    public interface IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4> : IDataFilter<TEntity1, TEntity2, TEntity3>
    {
        /// <summary>
        /// 指示当前实体是否需要过滤。
        /// </summary>
        /// <param name="entity">指定要过滤的实体。</param>
        /// <returns>如果不过滤则返回<see cref="true"/>,否则返回<see cref="false"/></returns>
        bool FilterEntity4(TEntity4 entity);
    }

    /// <summary>
    /// <see cref="IDataFilter{TEntity1, TEntity2, TEntity3, TEntity4, TEntity5}"/> 接口表示一个抽象的数据过滤器接口，用于为数据集提供数据过滤支持。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    /// <typeparam name="TEntity5">需要过滤的第五个实体类型。</typeparam>
    public interface IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5> : IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4>
    {
        /// <summary>
        /// 指示当前实体是否需要过滤。
        /// </summary>
        /// <param name="entity">指定要过滤的实体。</param>
        /// <returns>如果不过滤则返回<see cref="true"/>,否则返回<see cref="false"/></returns>
        bool FilterEntity5(TEntity5 entity);
    }

    /// <summary>
    /// <see cref="IDataFilter{TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6}"/> 接口表示一个抽象的数据过滤器接口，用于为数据集提供数据过滤支持。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    /// <typeparam name="TEntity5">需要过滤的第五个实体类型。</typeparam>
    public interface IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> : IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>
    {
        /// <summary>
        /// 指示当前实体是否需要过滤。
        /// </summary>
        /// <param name="entity">指定要过滤的实体。</param>
        /// <returns>如果不过滤则返回<see cref="true"/>,否则返回<see cref="false"/></returns>
        bool FilterEntity6(TEntity6 entity);
    }

    /// <summary>
    /// <see cref="IDataFilter{TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7}"/> 接口表示一个抽象的数据过滤器接口，用于为数据集提供数据过滤支持。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    /// <typeparam name="TEntity5">需要过滤的第五个实体类型。</typeparam>
    /// <typeparam name="TEntity6">需要过滤的第六个实体类型。</typeparam>
    /// <typeparam name="TEntity7">需要过滤的第七个实体类型。</typeparam>
    public interface IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> : IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>
    {
        /// <summary>
        /// 指示当前实体是否需要过滤。
        /// </summary>
        /// <param name="entity">指定要过滤的实体。</param>
        /// <returns>如果不过滤则返回<see cref="true"/>,否则返回<see cref="false"/></returns>
        bool FilterEntity7(TEntity7 entity);
    }

    /// <summary>
    /// <see cref="IDataFilter{TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8}"/> 接口表示一个抽象的数据过滤器接口，用于为数据集提供数据过滤支持。
    /// </summary>
    /// <typeparam name="TEntity1">需要过滤的第一个实体类型。</typeparam>
    /// <typeparam name="TEntity2">需要过滤的第二个实体类型。</typeparam>
    /// <typeparam name="TEntity3">需要过滤的第三个实体类型。</typeparam>
    /// <typeparam name="TEntity4">需要过滤的第四个实体类型。</typeparam>
    /// <typeparam name="TEntity5">需要过滤的第五个实体类型。</typeparam>
    /// <typeparam name="TEntity6">需要过滤的第六个实体类型。</typeparam>
    /// <typeparam name="TEntity7">需要过滤的第七个实体类型。</typeparam>
    /// <typeparam name="TEntity8">需要过滤的第八个实体类型。</typeparam>
    public interface IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8> : IDataFilter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>
    {
        /// <summary>
        /// 指示当前实体是否需要过滤。
        /// </summary>
        /// <param name="entity">指定要过滤的实体。</param>
        /// <returns>如果不过滤则返回<see cref="true"/>,否则返回<see cref="false"/></returns>
        bool FilterEntity8(TEntity8 entity);
    }
}

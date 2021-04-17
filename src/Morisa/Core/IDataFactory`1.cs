using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="IDataFactory{TEntity}"/> 接口表示一个抽象的数据工厂接口，用于为用户提供数据实体的操作支持。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataFactory<TEntity> : IDataFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        void Clear();
    }
}

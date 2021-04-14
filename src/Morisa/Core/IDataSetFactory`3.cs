using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="IDataSetFactory{TEntity, TDataSet, TProperty}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDataSet"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public interface IDataSetFactory<TEntity, TDataSet, TProperty> : IDataSetFactory<TDataSet, TProperty>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataProperty, IDataProperty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void Remove(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取当前所有实体实例。
        /// </summary>
        ReadOnlyObservableCollection<TEntity> Collection { get; }



        /// <summary>
        /// 获取或设置应用于当前 <see cref="IDataSetFactory{TEntity, TDataSet, TProperty}"/> 的分页器。
        /// </summary>
        IPageRequest Page { get; set; }

        /// <summary>
        /// 获取或设置应用于当前 <see cref="IDataSetFactory{TEntity, TDataSet, TProperty}"/> 的过滤器。
        /// </summary>
        IDataFilter<TEntity> Filter { get; set; }
    }
}

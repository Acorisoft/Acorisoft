using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    public interface IDataSetFactory<TEntity1, TEntity2, TDataSet, TProperty> : IDataSetFactory<TDataSet, TProperty>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataProperty, IDataProperty
    {
        void Add(TEntity1 entity);
        void Add(TEntity2 entity);
        void Add(IEnumerable<TEntity1> entities);
        void Add(IEnumerable<TEntity2> entities);
        void Update(TEntity1 entity);
        void Update(TEntity2 entity);
        void Update(IEnumerable<TEntity1> entities);
        void Update(IEnumerable<TEntity2> entities);
        void Remove(TEntity1 entity);
        void Remove(TEntity2 entity);
        void Remove(IEnumerable<TEntity1> entities);
        void Remove(IEnumerable<TEntity2> entities);
        void ClearEntity1();
        void ClearEntity2();


        /// <summary>
        /// 获取当前所有实体实例。
        /// </summary>
        ReadOnlyObservableCollection<TEntity1> Entity1 { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<TEntity2> Entity2 { get; }
    }
}

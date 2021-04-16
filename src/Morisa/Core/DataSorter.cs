using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class DataSorter<TEntity> : IDataSorter<TEntity>
    {
        public IComparer<TEntity> EntityComparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    public abstract class DataSorter<TEntity1, TEntity2> :
        DataSorter<TEntity1>,
        IDataSorter<TEntity1, TEntity2>
    {
        public IComparer<TEntity2> Entity2Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    public abstract class DataSorter<TEntity1, TEntity2, TEntity3> :
        DataSorter<TEntity1, TEntity2>,
        IDataSorter<TEntity1, TEntity2, TEntity3>
    {
        public IComparer<TEntity3> Entity3Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    /// <typeparam name="TEntity4"></typeparam>
    public abstract class DataSorter<TEntity1, TEntity2, TEntity3, TEntity4> :
        DataSorter<TEntity1, TEntity2, TEntity3>,
        IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4>
    {
        public IComparer<TEntity4> Entity4Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    /// <typeparam name="TEntity4"></typeparam>
    /// <typeparam name="TEntity5"></typeparam>
    public abstract class DataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5> :
        DataSorter<TEntity1, TEntity2, TEntity3, TEntity4>,
        IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>
    {
        public IComparer<TEntity5> Entity5Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    /// <typeparam name="TEntity4"></typeparam>
    /// <typeparam name="TEntity5"></typeparam>
    /// <typeparam name="TEntity6"></typeparam>
    public abstract class DataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> :
        DataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>,
        IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>
    {
        public IComparer<TEntity6> Entity6Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    /// <typeparam name="TEntity4"></typeparam>
    /// <typeparam name="TEntity5"></typeparam>
    /// <typeparam name="TEntity6"></typeparam>
    /// <typeparam name="TEntity7"></typeparam>
    public abstract class DataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> :
    DataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>,
    IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>
    {
        public IComparer<TEntity7> Entity7Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    /// <typeparam name="TEntity4"></typeparam>
    /// <typeparam name="TEntity5"></typeparam>
    /// <typeparam name="TEntity6"></typeparam>
    /// <typeparam name="TEntity7"></typeparam>
    /// <typeparam name="TEntity8"></typeparam>
    public abstract class DataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8> :
        DataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>,
        IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8>
    {
        public IComparer<TEntity8> Entity8Comparer { get; set; }
    }
}

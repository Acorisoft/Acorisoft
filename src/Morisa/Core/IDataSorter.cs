﻿using System;
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
    public interface IDataSorter<TEntity> 
    {
        IComparer<TEntity> EntityComparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    public interface IDataSorter<TEntity1, TEntity2> : IDataSorter<TEntity1>
    {
        IComparer<TEntity2> Entity2Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    public interface IDataSorter<TEntity1, TEntity2, TEntity3> : IDataSorter<TEntity1, TEntity2>
    {
        IComparer<TEntity3> Entity3Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    /// <typeparam name="TEntity4"></typeparam>
    public interface IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4> : IDataSorter<TEntity1, TEntity2, TEntity3>
    {
        IComparer<TEntity4> Entity4Comparer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity1"></typeparam>
    /// <typeparam name="TEntity2"></typeparam>
    /// <typeparam name="TEntity3"></typeparam>
    /// <typeparam name="TEntity4"></typeparam>
    /// <typeparam name="TEntity5"></typeparam>
    public interface IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5> : IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4>
    {
        IComparer<TEntity5> Entity5Comparer { get; set; }
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
    public interface IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> : IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>
    {
        IComparer<TEntity6> Entity6Comparer { get; set; }
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
    public interface IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> : IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>
    {
        IComparer<TEntity7> Entity7Comparer { get; set; }
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
    public interface IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8> : IDataSorter<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>
    {
        IComparer<TEntity8> Entity8Comparer { get; set; }
    }
}

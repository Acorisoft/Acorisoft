using LiteDB;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Acorisoft.Views
{
    /// <summary>
    /// 
    /// </summary>
    public interface INavigateParameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, object value);

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<string,object> Dictionary { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; }
    }
}
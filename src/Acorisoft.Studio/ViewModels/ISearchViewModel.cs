using System.Collections.Generic;

namespace Acorisoft.Studio.ViewModels
{
    public interface ISearchViewModel<TResult> where  TResult : notnull
    {
        /// <summary>
        /// 
        /// </summary>
        string Keywords { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<TResult> Results { get; }
    }
}
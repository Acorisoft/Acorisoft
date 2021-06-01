using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents;
using DynamicData;

namespace Acorisoft.Studio.Engines
{
    /// <summary>
    /// <see cref="IDocumentGalleryEngine{TIndex,TIndexWrapper,TDocument}"/> 类型表示一个抽象的画廊引擎接口，用于为应用程序提供添加、删除、清空文档支持。
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    /// <typeparam name="TIndexWrapper"></typeparam>
    /// <typeparam name="TDocument"></typeparam>
    public interface IDocumentGalleryEngine<TIndex, TIndexWrapper, TDocument>
        where TIndex : Acorisoft.Studio.Documents.DocumentIndexVersion1
        where TIndexWrapper : Acorisoft.Studio.Documents.DocumentIndexWrapperVersion1<TIndex>
        where TDocument : Acorisoft.Studio.Documents.DocumentVersion1
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task UpdateAsync(TDocument document);
        
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task FindAsync(string keyword);
        
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task NewAsync(INewDocumentInfo<TDocument> info);
        
        /// <summary>
        /// 打开文档
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<TDocument> OpenAsync(TIndex index);
        
        /// <summary>
        /// 保存文档更改
        /// </summary>
        /// <param name="wrapper"></param>
        /// <param name="index"></param>
        /// <param name="document"></param>
        Task UpdateAsync(TIndexWrapper wrapper, TIndex index, TDocument document);

        /// <summary>
        /// 删除这个文档
        /// </summary>
        /// <param name="index"></param>
        Task DeleteThisAsync(TIndex index);

        /// <summary>
        /// 删除这个文档
        /// </summary>
        /// <param name="document"></param>
        Task DeleteThisAsync(TDocument document);

        /// <summary>
        /// 删除这个页面
        /// </summary>
        Task DeleteThisPageAsync();

        /// <summary>
        /// 删除全部数据
        /// </summary>
        Task DeleteAllAsync();

        /// <summary>
        /// 获取当前画廊的页面数量
        /// </summary>
        /// <remarks>
        /// <para>这个属性值必须在[1,65536]之间</para>
        /// </remarks>
        IObservable<int> PageCount { get; }
        
        /// <summary>
        /// 获取当前画廊的操作状态
        /// </summary>
        IObservable<bool> IsOpen { get; }

        /// <summary>
        /// 获取或设置当前画廊中每个页面中元素的数量。
        /// </summary>
        /// <remarks>
        /// <para>这个属性值必须在[1,255]之间</para>
        /// </remarks>
        int PerPageCount { get; set; }

        /// <summary>
        /// 获取或设置当前画廊的页面位置。
        /// </summary>
        /// <remarks>
        /// <para>这个属性值必须在[1,65536]之间</para>
        /// </remarks>
        int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置当前画廊的过滤器。
        /// </summary>
        Func<TIndexWrapper, bool> Filter { get; set; }
        
        /// <summary>
        /// 获取或设置当前画廊的排序器。
        /// </summary>
        IComparer<TIndexWrapper> Sorter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<TIndexWrapper> Collection { get; }
    }
}
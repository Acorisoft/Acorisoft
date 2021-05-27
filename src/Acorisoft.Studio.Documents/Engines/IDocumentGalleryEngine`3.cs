using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents;

namespace Acorisoft.Studio.Engines
{
    /// <summary>
    /// <see cref="IDocumentGalleryEngine{TIndex,TIndexWrapper,TDocument}"/> 类型表示一个抽象的画廊引擎接口，用于为应用程序提供添加、删除、清空文档支持。
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    /// <typeparam name="TIndexWrapper"></typeparam>
    /// <typeparam name="TDocument"></typeparam>
    public interface IDocumentGalleryEngine<TIndex, TIndexWrapper, TDocument>
        where TIndex : DocumentIndex
        where TIndexWrapper : DocumentIndexWrapper<TIndex>
        where TDocument : Document
    {
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
        /// 
        /// </summary>
        Task DeleteThisPageAsync();

        /// <summary>
        /// 
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
        Func<TIndex, bool> Filter { get; set; }
        
        

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<TIndexWrapper> Collection { get; }
    }
}
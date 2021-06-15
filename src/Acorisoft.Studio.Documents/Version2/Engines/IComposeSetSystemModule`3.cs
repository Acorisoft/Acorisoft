using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Acorisoft.Studio.Engines
{
    public interface IComposeSetSystemModule<TIndex, TIndexWrapper, TComposition>
        where TIndex : DocumentIndex
        where TIndexWrapper : DocumentIndexWrapper<TIndex>
        where TComposition : Document
    {
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="keyword">要匹配的关键字</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task FindAsync(string keyword);

        /// <summary>
        /// 重置搜索
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task ResetAsync();

        /// <summary>
        /// 刷新
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task RefershAsync();

        /// <summary>
        /// 在一个异步操作中创建一个新的项目。
        /// </summary>
        /// <param name="info">指定要创建的操作。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task NewAsync(INewItemInfo<TComposition, TIndex> info);

        /// <summary>
        /// 打开文档
        /// </summary>
        /// <param name="index">要打开的索引。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task<TComposition> OpenAsync(TIndex index);

        /// <summary>
        /// 保存文档更改
        /// </summary>
        /// <param name="wrapper"></param>
        /// <param name="index"></param>
        /// <param name="document"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task UpdateAsync(TIndexWrapper wrapper, TIndex index, TComposition document);

        /// <summary>
        /// 保存文档更改
        /// </summary>
        /// <param name="document"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task UpdateAsync(TComposition document);

        /// <summary>
        /// 删除这个文档
        /// </summary>
        /// <param name="index"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task DeleteThisAsync(TIndex index);

        /// <summary>
        /// 删除这个文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task DeleteThisAsync(TComposition document);

        /// <summary>
        /// 删除这个页面
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        Task DeleteThisPageAsync();

        /// <summary>
        /// 删除全部数据
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
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
        int PerPageItemCount { get; }

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
        /// 获取当前 <see cref="IComposeSetSystemModule{TIndex,TIndexWrapper,TComposition}"/> 的可绑定集合。
        /// </summary>
        ReadOnlyObservableCollection<TIndexWrapper> Collection { get; }
    }
}
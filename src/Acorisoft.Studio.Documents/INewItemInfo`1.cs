﻿using System;

namespace Acorisoft.Studio
{
    public interface INewItemInfo<TItem>
    {
        /// <summary>
        /// 获取或设置当前 <see cref="INewItemInfo{TItem}"/> 的唯一标识符。
        /// </summary>
        /// <remarks>
        /// 该属性亦用于设置创建的项目的唯一标识符。
        /// </remarks>
        Guid Id { get; set; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="INewItemInfo{TItem}"/> 的名称。
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="INewItemInfo{TItem}"/> 的项目。
        /// </summary>
        TItem Item { get; set; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="INewItemInfo{TItem}"/> 的项目路径。
        /// </summary>
        string Path { get; set; }
    }
}
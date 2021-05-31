using System;

namespace Acorisoft.Studio
{
    public interface INewItemInfo<TItem, TFeedBack1, TFeedBack2> : INewItemInfo<TItem>
    {
        TFeedBack1 FeedBackValue1 { get; set; } 
        TFeedBack2 FeedBackValue2 { get; set; }
    }
    
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

    public class NewItemInfo<TItem> : INewItemInfo<TItem>
    {
        public NewItemInfo()
        {
            Id = Guid.NewGuid();
        }
        
        public NewItemInfo(TItem item)
        {
            Id = Guid.NewGuid();
            Item = item;
        }
        /// <summary>
        /// 获取或设置当前 <see cref="INewItemInfo{TItem}"/> 的唯一标识符。
        /// </summary>
        /// <remarks>
        /// 该属性亦用于设置创建的项目的唯一标识符。
        /// </remarks>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="INewItemInfo{TItem}"/> 的名称。
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="INewItemInfo{TItem}"/> 的项目。
        /// </summary>
        public TItem Item { get; set; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="INewItemInfo{TItem}"/> 的项目路径。
        /// </summary>
        public string Path { get; set; }
    }

    public class NewItemInfo<TItem, TFeedBack1, TFeedBack2> : NewItemInfo<TItem>, INewItemInfo<TItem, TFeedBack1, TFeedBack2>
    {
        public TFeedBack1 FeedBackValue1 { get; set; } 
        public TFeedBack2 FeedBackValue2 { get; set; }
    }
}
using LiteDB;
using System;
using System.IO;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="Resource"/>表示一种资源类型，用于描述所需的资源所在的位置。
    /// </summary>

    public abstract class Resource
    {
        /// <summary>
        /// 获取当前资源的唯一标识符。
        /// </summary>
        [BsonId]
        public string Id { get; set; }
    }

    /// <summary>
    /// <see cref="InDatabaseResource"/> 表示一种存储于数据库的资源，用于描述当前资源存储于数据库中。
    /// </summary>
    public sealed class InDatabaseResource : Resource
    {
        /// <summary>
        /// 获取当前资源的文件路径。只在读取的时候有效。
        /// </summary>
        [BsonIgnore]
        public string FileName { get; set; }

        /// <summary>
        /// 获取当前资源的统一资源路径。
        /// </summary>
        [BsonIgnore]
        public string Url { get; set; }
    }

    /// <summary>
    /// <see cref="OutsideResource"/> 表示一种旁侧资源。用于描述当前的资源实际存储在指定数据的旁侧。
    /// </summary>
    public sealed class OutsideResource : Resource
    {
        /// <summary>
        /// 获取当前资源的文件路径。只在读取的时候有效。
        /// </summary>
        public string FileName { get; set; }
    }

    /// <summary>
    /// <see cref="OnlineResource"/> 表示一种在线资源。用于描述当前的资源实际存储在互联网上。
    /// </summary>
    public sealed class OnlineResource : Resource
    {
        /// <summary>
        /// 获取当前资源的统一资源路径。
        /// </summary>
        public string Url { get; set; }
    }
}

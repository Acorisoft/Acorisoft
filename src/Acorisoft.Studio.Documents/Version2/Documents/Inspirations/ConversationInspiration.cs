using System;
using System.Collections.Generic;
using System.Linq;

namespace Acorisoft.Studio.Documents.Inspirations
{
    public class Spokeman
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
    
    public class Conversation
    {
        public Guid Spokeman { get; set; }
        public string Color { get; set; }
        public string Content { get; set; }
    }
    
    /// <summary>
    /// <see cref="ConversationInspiration"/> 表示一个对话类型的灵感。
    /// </summary>
    /// <remarks>
    /// <para>通过对话的方式产生灵感，这个功能最主要指的是我们通过扮演当事人，通过角色扮演的办法来实现寻找灵感。</para>
    /// <para>对话灵感主要包括以下内容：</para>
    /// <para>1.对话发生前的剧情概要。</para>
    /// <para>2.参与对话的人物。</para>
    /// <para>3.对话内容。</para>
    /// </remarks>
    public class ConversationInspiration : InspirationDocument
    {
        public sealed override InspirationType Type => InspirationType.Conversation;

        /// <summary>
        /// 获取或设置当前对话灵感中参与对话的人物。
        /// </summary>
        public List<Spokeman> Spokemans { get; set; }
        
        /// <summary>
        /// 获取或设置当前对话灵感的实际对话内容。
        /// </summary>
        public List<Conversation> Conversations { get; set; }

        public sealed override string ToString()
        {
            // 一段对话，12条文字
            return $"{Name},{Conversations?.Count ?? 0}";
        }
    }
}
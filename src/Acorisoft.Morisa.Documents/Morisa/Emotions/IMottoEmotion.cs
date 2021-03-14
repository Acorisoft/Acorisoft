using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Emotions
{

    public enum MottoEmotionPresentation
    {
        Default,
    }

    /// <summary>
    /// <see cref="IMottoEmotion"/> 表示一种格言式的情绪文字。
    /// </summary>
    /// <remarks>
    /// 格言式的情绪，一般是一句不超过100字的短文。
    /// <para>例如：在爱情萌芽的时候，只要悄悄的有点希望就足够了。</para>
    /// <para>——绝望先生</para>
    /// </remarks>
    public interface IMottoEmotion : IEmotionElement
    {
        /// <summary>
        /// 格言的主要内容。
        /// </summary>
        string Motto { get; set; }

        /// <summary>
        /// 格言的署名
        /// </summary>
        string Signature { get; set; }

        /// <summary>
        /// 用户可选的表示形式。
        /// </summary>
        MottoEmotionPresentation Presentation { get; set; }
    }
}

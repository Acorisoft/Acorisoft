using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Inspirations
{
    /// <summary>
    /// <see cref="IInspirationTextElement"/> 接口定义了一个短文章灵感元素。短文章灵感元素用于为用户提供在短文字中捕捉灵感的功能。
    /// </summary>
    public interface IInspirationTextElement : IInspirationElement
    {
        string Color { get; set; }
        string Text { get; set; }
    }
}

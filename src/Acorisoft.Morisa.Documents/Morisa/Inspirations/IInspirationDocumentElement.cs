using Acorisoft.Morisa.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Inspirations
{
    /// <summary>
    /// <see cref="IInspirationDocumentElement"/> 接口定义了一个灵感文档元素。灵感文档元素是长文章，用于为用户提供在长文章中捕捉灵感的功能。
    /// </summary>
    /// <remarks>
    /// 灵感文档元素是长文章，用于为用户提供在长文章中捕捉灵感的功能。
    /// </remarks>
    public interface IInspirationDocumentElement : IInspirationElement, IDocument
    {
    }
}

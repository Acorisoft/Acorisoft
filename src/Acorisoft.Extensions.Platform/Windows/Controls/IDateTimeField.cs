using System;

namespace Acorisoft.Extensions.Windows.Controls
{
    /// <summary>
    /// <see cref="IDateTimeField"/> 表示一个输入日期的字段。
    /// </summary>
    public interface IDateTimeField
    {
        string Text { get; set; }
        DateTime DateTime { get; set; }
        string StringFormat { get; set; }
    }
}
using System;
using System.Text;

namespace Acorisoft.Extensions.Platforms
{
    /// <summary>
    /// <see cref="StringExtensions"/> 用于为字符串提供拓展功能支持。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 检查一个序列中字符串与指定字符出现不匹配的索引位置。
        /// </summary>
        /// <param name="sequence">原字符串序列</param>
        /// <param name="character">要匹配的字符</param>
        /// <param name="isReserved">是否逆序寻找</param>
        /// <returns>返回一个 <see cref="int"/>类型，表示匹配操作失败的索引位置。</returns>
        /// <remarks>
        /// <para>如果isReserved为false，则对于一个序列AAABCDAAA而言，分析它第一个与A字符不一样的索引位置为：3</para>
        /// <para>如果isReserved为true，则对于一个序列AAABCDAAA而言，分析它第一个与A字符不一样的索引位置为：6</para>
        /// </remarks>
        public static int FirstIndexOfDifference(this string sequence, char character, bool isReserved = false)
        {
            int index;
            
            if (!isReserved)
            {
                index = 0;
                for (; index < sequence.Length;index++)
                {
                    if (sequence[index] != character)
                    {
                        return index;
                    }
                }
            }
            else
            {
                index = sequence.Length - 1;
                for (; index > 0; index--)
                {
                    if (sequence[index] != character)
                    {
                        return index;
                    }
                }
            }

            return isReserved ? 0 : sequence.Length - 1;
        }

        /// <summary>
        /// 从一个子序列中返回一个只保留指定内容的字符串。
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="predication"></param>
        /// <returns></returns>
        public static string Reserve(this string sequence, Predicate<char> predication)
        {
            predication ??= x => char.IsDigit(x);
            var builder = new StringBuilder();
            for (var i = 0; i < sequence.Length; i++)
            {
                if (predication(sequence[i]))
                {
                    builder.Append(sequence[i]);
                }
            }

            return builder.ToString();
        }
    }
}
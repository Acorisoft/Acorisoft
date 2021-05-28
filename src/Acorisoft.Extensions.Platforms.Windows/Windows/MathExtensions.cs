using System.Linq;
using System.Text;

namespace Acorisoft.Extensions.Platforms.Windows
{
    public static class MathExtensions
    {
        public static int GetNumber(string rawString)
        {
            var builder = new StringBuilder();
            foreach (var character in rawString.Where(character => char.IsDigit(character)))
            {
                builder.Append(character);
            }

            return builder.Length == 0 ? 0 : int.Parse(builder.ToString());
        }
    }
}
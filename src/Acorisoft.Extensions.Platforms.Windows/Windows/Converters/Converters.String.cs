using System.Globalization;

namespace Acorisoft.Extensions.Platforms.Windows.Converters
{
   #region String2IntConverter

   public class String2IntConverter : TwoWayConverter<string, int>
   {
      public readonly object Zero = 0;
      public const string ZeroString = "0";
      protected sealed override object ConvertSourceToTarget(string source, object parameter, CultureInfo culture)
      {
         //
         // 将String类型的数据转化为Int
         return int.TryParse(source, out var value) ? value : Zero;
      }

      protected sealed override string ConvertTargetToSource(int target, object parameter, CultureInfo culture)
      {
         //
         // int 类型到 string 类型
         return target.ToString();
      }

      protected sealed override object ConvertSourceToTargetFallback(object source, object parameter, CultureInfo culture)
      {
         return Zero;
      }

      protected sealed override string ConvertTargetToSourceFallback(object target, object parameter, CultureInfo culture)
      {
         return ZeroString;
      }

      protected sealed override object ConvertSourceToTargetWhenNull(object parameter, CultureInfo culture) => Zero;
      protected sealed override object ConvertTargetToSourceWhenNull(object parameter, CultureInfo culture) => ZeroString;
   }

   #endregion
}
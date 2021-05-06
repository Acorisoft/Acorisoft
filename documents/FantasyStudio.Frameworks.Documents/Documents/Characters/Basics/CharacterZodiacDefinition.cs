using Acorisoft.FantasyStudio.Core;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    [DebuggerDisplay("{Zodiac}")]
    public class CharacterZodiacDefinition : Bindable, ICharacterZodiacDefinition
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private Zodiac _Zodiac;

        public int SortingIndex => 1;

        /// <summary>
        /// 获取或设置当前人物设定的星座。
        /// </summary>
        public Zodiac Zodiac { get => _Zodiac ; set => SetValue(ref _Zodiac,value); }

        #region Convert Methods

        public static string GetName(Zodiac rawValue, CultureInfo culture)
        {
            return culture.LCID switch
            {
                2052 => GetSimplifiledChinese(rawValue),
                1028 or 3076 => GetTraditionalChinese(rawValue),
                _ => GetEnglish(rawValue),
            };
        }

        static string GetEnglish(Zodiac zodiac)
        {
            return zodiac switch
            {
                Zodiac.Aquarius => "Aquarius",
                Zodiac.Cancer => "Cancer",
                Zodiac.Capricorn => "Capricorn",
                Zodiac.Leo => "Leo",
                Zodiac.Libra => "Libra",
                Zodiac.Gemini => "Gemini",
                Zodiac.Pisces => "Pisces",
                Zodiac.Taurus => "Taurus",
                Zodiac.Scorpio => "Scorpio",
                Zodiac.Sagittarius => "Sagittarius",
                Zodiac.Virgo => "Virgo",
                _ => "Aries",
            };
        }
        static string GetTraditionalChinese(Zodiac zodiac)
        {
            return zodiac switch
            {
                Zodiac.Aquarius => "水瓶座",
                Zodiac.Cancer => "巨蟹座",
                Zodiac.Capricorn => "摩羯座",
                Zodiac.Leo => "獅子座",
                Zodiac.Libra => "天秤座",
                Zodiac.Gemini => "雙子座",
                Zodiac.Pisces => "雙魚座",
                Zodiac.Taurus => "金牛座",
                Zodiac.Scorpio => "天蠍座",
                Zodiac.Sagittarius => "射手座",
                Zodiac.Virgo => "處女座",
                _ => "白羊座",
            };
        }

        static string GetSimplifiledChinese(Zodiac zodiac)
        {
            return zodiac switch
            {
                Zodiac.Aquarius => "水瓶座",
                Zodiac.Cancer => "巨蟹座",
                Zodiac.Capricorn => "摩羯座",
                Zodiac.Leo => "狮子座",
                Zodiac.Libra => "天秤座",
                Zodiac.Gemini => "双子座",
                Zodiac.Pisces => "双鱼座",
                Zodiac.Taurus => "金牛座",
                Zodiac.Scorpio => "天蝎座",
                Zodiac.Sagittarius => "射手座",
                Zodiac.Virgo => "处女座",
                _ => "白羊座",
            };
        }

        #endregion Convert Methods
    }
}

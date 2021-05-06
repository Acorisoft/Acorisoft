using System;
using System.Windows.Controls;
using System.Globalization;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    public partial class ApperanceLand : UserControl
    {
        public ApperanceLand()
        {
            InitializeComponent();
            PART_Title.Header = Initialize();
        }

        protected static string Initialize()
        {
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            return lcid switch
            {
                2052 => "外观",
                1028 or 3076 => "外觀",
                _ => "Apperance",
            };
        }
    }
}
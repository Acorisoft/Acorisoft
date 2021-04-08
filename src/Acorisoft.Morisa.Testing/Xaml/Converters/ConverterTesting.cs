using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Acorisoft.Morisa.Converters
{
    [TestClass]
    public class ConverterTesting
    {
        [TestMethod]
        public void Testing_Generic_Converter()
        {
            var t2b = new True2VisibilityConverter();
            Assert.AreEqual(t2b.Convert(false, null, null, null), Visibility.Collapsed);
            Assert.AreEqual(t2b.Convert(true, null, null, null), Visibility.Visible);

            var f2b = new False2VisibilityConverter();
            Assert.AreEqual(f2b.Convert(false, null, null, null), Visibility.Visible);
            Assert.AreEqual(f2b.Convert(true, null, null, null), Visibility.Collapsed);
        }
    }
}

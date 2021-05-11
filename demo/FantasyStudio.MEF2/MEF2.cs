using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FantasyStudio.MEF.Core;

namespace FantasyStudio.MEF2
{
    [Export(typeof(IModel))]
    public class MEF2 : UserControl, IModel
    {
        public MEF2()
        {
            Background = new SolidColorBrush(Colors.Coral);
        }
    }
}
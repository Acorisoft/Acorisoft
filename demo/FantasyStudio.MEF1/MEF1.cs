using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FantasyStudio.MEF.Core;

namespace FantasyStudio.MEF1
{
    [Export(typeof(IModel))]
    public class MEF1 : UserControl, IModel
    {
        public MEF1()
        {
            Background = new SolidColorBrush(Colors.CornflowerBlue);
        }
    }
}
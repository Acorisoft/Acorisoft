using Acorisoft.Morisa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Acorisoft.Morisa
{
    class ViewModelLocator
    {
        public static AppViewModel AppViewModel { get => ((App)Application.Current).AppViewModel; }
    }
}

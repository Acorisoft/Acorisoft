using Acorisoft.Morisa.ViewModels;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.ViewModels
{

    public partial class SettingViewModel : ViewModelBase, ISettingViewModel
    {
        private readonly AppViewModel _Root;

        public SettingViewModel(AppViewModel rootVM)
        {
            _Root = rootVM ?? throw new InvalidOperationException();
        }

        public bool IgnoreFileDuplicate
        {
            get=> _Root.IgnoreFileDuplicate;
            set
            {
                _Root.IgnoreFileDuplicate = value;
                RaiseUpdated(nameof(IgnoreFileDuplicate));
            }
        }

    }
}
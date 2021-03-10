using Acorisoft.Morisa.Dialogs;
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
    public partial class SelectProjectFolderViewModel : ViewModelBase, IResultable
    {

        private string _Folder;

        object IResultable.GetResult()
        {
            return _Folder;
        }

        bool IResultable.VerifyAccess()
        {
            return !string.IsNullOrEmpty(_Folder);
        }

        public string ProjectFolder
        {
            get => _Folder;
            set => Set(ref _Folder , value);
        }
    }
}
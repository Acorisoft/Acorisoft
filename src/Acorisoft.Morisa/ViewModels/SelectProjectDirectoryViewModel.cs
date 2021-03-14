using Acorisoft.Morisa.Dialogs;
using Acorisoft.Morisa.ViewModels;
using DynamicData.Binding;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Acorisoft.Morisa.ViewModels
{
    public partial class SelectProjectDirectoryViewModel : DialogFunction
    {
        public SelectProjectDirectoryViewModel()
        {
        }

        protected override object GetResultCore()
        {
            return Directory;
        }

        protected override bool VerifyModelCore()
        {
            return !string.IsNullOrEmpty(Directory) && System.IO.Directory.Exists(Directory);
        }

        [Reactive]public string Directory { get; set; }
    }
}